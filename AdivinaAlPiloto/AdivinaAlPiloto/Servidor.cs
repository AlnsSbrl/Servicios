using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaAlPiloto
{
    internal class Servidor
    {
        public Socket socket;
        int port = 42069;
        IPEndPoint ie;
        public List<int> years; //se escoge aleatoriamente un año de estos de la lista, y de ese año aleatoriamente un piloto de la lista que devuelve la api
        public void Init()
        {
            years = new List<int>();
            years.Add(2022);
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
                        ParametroCliente pam = new ParametroCliente(sclient);
                        Thread thread = new Thread(AdivinaAlPiloto);
                        thread.Start(pam);
                    }
                }
                catch (SocketException ex) when (ex.ErrorCode == (int)SocketError.Interrupted)
                {

                }
            }
        }

        public void AdivinaAlPiloto(object pam)
        {
            ParametroCliente datosCliente = (ParametroCliente)pam;
            using (NetworkStream ns = new NetworkStream(datosCliente.socket))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {            
                string? userOption = sr.ReadLine();
                if (userOption != null)
                {
                    string[] msg = userOption.Split(' ');
                    switch (msg[0])
                    {
                        case string e when e == "getdriver" && msg.Length == 1:
                            sw.WriteLine(Juega(datosCliente));
                            sw.Flush();
                            break;
                        case string e when e == "add" && msg.Length == 2:
                            if (AddDrivers(msg[1]))
                            {
                                sw.WriteLine("Done!, added drivers from the " + msg[1]+" season");
                            }
                            else
                            {
                                sw.WriteLine("I can't do that bro");
                            }
                            sw.Flush();
                            break;
                        case string e when e == "getrecords" && msg.Length == 1:
                            GetRecords(datosCliente);
                            break;
                        case string e when e == "closeserver" && msg.Length == 2:
                            compruebaPIN(msg[1]);
                            break;
                        case string e when e == "addrecord" && msg.Length == 2:

                            if (añadeRecord(GetRecords(datosCliente), msg[1]))
                            {
                                sw.Write("ole");
                            }
                            else
                            {
                                sw.Write("uffff");
                            }
                            sw.Flush();
                            break;
                        default:
                            break;
                    }
                }
            }
            datosCliente.socket.Close();

        }
        public bool añadeRecord(List<Record> records, string reccc)
        {
            bool seAñade = false;
            int newRecord = 0;
            int index;
            if (int.TryParse(reccc, out newRecord))
            {

                for (int i = 0; i < records.Count; i++)
                {
                    if (newRecord < records[i].time)
                    {
                        seAñade = true;
                        index = i;
                        break;
                    }
                }
                if (seAñade)
                {

                }
            };
            return seAñade;
        }
        public string Juega(ParametroCliente paramCliente)
        {
            ParametroCliente pam = (ParametroCliente)paramCliente;
            using (HttpClient httpclient = new HttpClient())
            {
                Random random = new Random();
                int r = random.Next(years.Count);
                string endPoint = String.Format($"http://ergast.com/api/f1/{years[r]}/drivers.json");
                var jason = httpclient.GetAsync(endPoint).Result.Content.ReadAsStringAsync().Result;
                JObject jsonRoot = (JObject)JsonConvert.DeserializeObject(jason);
                JArray drivers = (JArray)jsonRoot["MRData"]["DriverTable"]["Drivers"];
                //Random random = new Random();
                int choice = random.Next(drivers.Count);
                string driverName = drivers[choice]["givenName"].Value<string>();
                string driverFamilyName = drivers[choice]["familyName"].Value<string>();
                string driverNationality = drivers[choice]["nationality"].Value<string>();
                Piloto driverToGuess = new Piloto(driverName, driverFamilyName, driverNationality);
                //la idea es mandar al piloto entero y que, cuando vaya fallando, le de más informacion al usuario pa acertar
                return driverToGuess.name + " " + driverToGuess.familyName;
                //el trigger que me da que la api dé mal algunos nombres hispanos I FUCKING CANT
            }
        }

        public bool AddDrivers(string yearToAdd)
        {
            bool done = false;
            int year = 0;
            if (yearToAdd != null)
            {
                int.TryParse(yearToAdd, out year);
            }
            if (year >= 1950 && year < 2022 && !years.Contains(year))
            {
                years.Add(year);
                done = true;              
            }
            return done;
        }
    

    public List<Record> GetRecords(ParametroCliente param)
    {
        try
        {
            using (RecordReader rr = new RecordReader(new FileStream("a", FileMode.Open)))
            {
                List<Record> recs = new List<Record>();
                try
                {
                    while (true)
                    {
                        recs.Add(rr.ReadRecord());
                    }
                }
                catch (EndOfStreamException)
                {
                    return recs;
                }
            }
        }
        catch (IOException)
        {
            return null;
        }
    }

    public bool compruebaPIN(string pin)
    {


        return true;
    }
}
}
