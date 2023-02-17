using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaAlPiloto
{
    internal class Servidor
    {
        private readonly object l = new object();
        public Socket socket;
        int port = 42069;
        IPEndPoint ie;
        public List<int> years; //se escoge aleatoriamente un año de estos de la lista, y de ese año aleatoriamente un piloto de la lista que devuelve la api
        string path = Environment.GetEnvironmentVariable("userprofile")+"/recordsF1.bin";
        public List<Record> records;
        int pin = 1234;
        public void LeeRecords()
        {
            try
            {
                lock (l)
                {
                    using (RecordReader rr = new RecordReader(new FileStream(path, FileMode.Open)))
                    {
                        while (true)
                        {
                            Record? record = rr.ReadRecord();
                            records.Add(record);
                        }
                    }
                }
            }
            catch (EndOfStreamException)
            {
                return;
            }
            catch (IOException)
            {

            }
        }
        public void EscribeRecords()
        {
            try
            {
                lock (l)
                {
                    using (RecordWriter rw = new RecordWriter(new FileStream(path, FileMode.OpenOrCreate)))
                    {
                        for (int i = 0; i < records.Count; i++)
                        {
                            rw.Write(records[i]);
                        }
                    }
                }
            }
            catch (IOException)
            {

            }
        }
        public void Init()
        {
            years = new List<int>();
            records = new List<Record>();
            records.Capacity = 3;
            years.Add(2022);
            LeeRecords();
            do
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    ie = new IPEndPoint(IPAddress.Any, port);
                    socket.Bind(ie);
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                {
                    port++;
                    if (port > IPEndPoint.MaxPort) break;
                }
            } while (!socket.IsBound);

            if (socket.IsBound)
            {
                try
                {
                    socket.Listen(10);
                    while (true)
                    {
                        Console.WriteLine("Im up at " + port);
                        Socket sclient = socket.Accept();
                        Thread thread = new Thread(AdivinaAlPiloto);
                        thread.Start(sclient);
                        thread.IsBackground = true;
                    }
                }
                catch (SocketException ex) when (ex.ErrorCode == (int)SocketError.Interrupted)
                {
                    Console.WriteLine("la FIA nos ha censurado =(");
                }
            }
        }

        public void AdivinaAlPiloto(object soc)
        {
            Socket socketCliente = (Socket)soc;
            using (NetworkStream ns = new NetworkStream(socketCliente))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                string? userOption = sr.ReadLine();
                if (userOption != null)
                {
                    string[] msg = userOption.Split(' ');
                    switch (msg[0])
                    {
                        case string e when e == "getword" && msg.Length == 1:
                            sw.WriteLine(Juega(false));//devuelve solo al piloto
                            sw.Flush();
                            break;
                        case string e when e == "getdata" && msg.Length == 1:
                            sw.WriteLine(Juega(true));//devuelve al piloto y datos relacionados con él
                            sw.Flush();
                            break;
                        case string e when e == "sendword" && msg.Length == 2:
                            if (AddDrivers(msg[1]))
                            {
                                sw.WriteLine("OK");
                            }
                            else
                            {
                                sw.WriteLine("ERROR");
                            }
                            sw.Flush();
                            break;
                        case string e when e == "getrecords" && msg.Length == 1:
                            sw.WriteLine(GetRecords());
                            sw.WriteLine(records.Capacity);
                            sw.Flush();
                            break;
                        case string e when e == "closeserver" && msg.Length == 2:
                            if (compruebaPIN(msg[1]))
                            {
                                sw.Write("CLOSED");
                                sw.Flush();
                                socket.Close();
                            }
                            else
                            {
                                sw.Write("QUE FAS CARALLO");
                                sw.Flush();
                            }
                            break;
                        case string e when e == "a" && msg.Length == 2:
                            if (añadeRecord(msg[1]))
                            {
                                sw.Write("ACCEPT");
                            }
                            else
                            {
                                sw.Write("REJECT");
                            }
                            sw.Flush();
                            break;
                        default:
                            break;
                    }
                }
            }
            socketCliente.Close();

        }
        public bool añadeRecord(string reccc)
        {
            bool isInserted = false;
            //comprobar si puedo hacer esto tbh, se le puede pasar el param vacio jjjjj
            string name;
            string recordTime;
            if (reccc.Length > 3)
            {
                name = reccc.Substring(0, 3);
                recordTime = reccc.Substring(3);
                int time;
                int.TryParse(recordTime, out time);
                Record newRcord = new Record(name, time);
                lock (l)
                {
                    int newElement = records.FindIndex(rec => rec.time > time);
                    if (newElement != -1)
                    {
                        if (records.Count == records.Capacity)
                        {
                            records.RemoveAt(records.Count - 1);
                        }
                        records.Insert(newElement, newRcord);
                        isInserted = true;
                    }
                    if (newElement == -1 && records.Count < records.Capacity)
                    {
                        records.Insert(records.Count, newRcord);
                        isInserted = true;
                    }
                }
            }
            if (isInserted)
            {
                lock (l)
                {
                    EscribeRecords();
                }
            }

            return isInserted;
        }
        public string Juega(bool pistas)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                string endPoint;
                Random random = new Random();
                int r;
                lock (l)
                {
                    r = random.Next(years.Count);
                    endPoint = String.Format($"http://ergast.com/api/f1/{years[r]}/drivers.json");
                }
                var jason = httpclient.GetAsync(endPoint).Result.Content.ReadAsStringAsync().Result;
                JObject jsonRoot = (JObject)JsonConvert.DeserializeObject(jason);
                JArray drivers = (JArray)jsonRoot["MRData"]["DriverTable"]["Drivers"];
                //Random random = new Random();
                int choice = random.Next(drivers.Count);
                string driverName = drivers[choice]["givenName"].Value<string>().ToUpper();
                string driverFamilyName = drivers[choice]["familyName"].Value<string>().ToUpper();
                string driverNationality = drivers[choice]["nationality"].Value<string>();
                Piloto driverToGuess = new Piloto(driverName, driverFamilyName, driverNationality);
                //la idea es mandar al piloto entero y que, cuando vaya fallando, le de más informacion al usuario pa acertar
                if (driverNationality.ToLower() == "japanese" || driverNationality.ToLower() == "chinese")
                {
                    string swap = driverName;
                    driverName = driverFamilyName;
                    driverFamilyName = swap;
                }

                if (pistas)
                {
                    lock (l)
                    {
                        return driverName + " " + driverFamilyName + "$" + driverNationality + "$" + years[r];
                    }
                }
                else
                {
                    return driverToGuess.name + " " + driverToGuess.familyName;
                }
                //el trigger que me da que la api dé mal algunos nombres hispanos I FUCKING CANT
            }
        }


        public bool AddDrivers(string yearToAdd)
        {
            bool done = false;
            int year = 0;
            int.TryParse(yearToAdd, out year);
            lock (l)
            {
                if (year >= 1950 && year < 2022 && !years.Contains(year))
                {
                    years.Add(year);
                    done = true;
                }
            }
            return done;
        }


        public string GetRecords()
        {
            string muestraRecords = "Lista de records:";
            lock (l)
            {
                for (int i = 0; i < records.Count; i++)
                {
                    muestraRecords += String.Format($"\r\n{records[i].name}\t{records[i].time}");
                }
                muestraRecords += "\r\nFin";
            }
            return muestraRecords;
        }

        public bool compruebaPIN(string input)
        {
            int intento = -1;
            int.TryParse(input, out intento);
            lock (l)
            {
                return (intento == pin);
            }
        }
    }
}
