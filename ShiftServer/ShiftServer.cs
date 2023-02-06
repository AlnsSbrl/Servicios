using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace ShiftServer
{
    internal class ShiftServer
    {
        string[] users;
        SortedList waitingQueue = new SortedList();
        Socket socket;
        int port = 31416;
        int backUpPort = 1024;
        IPEndPoint ie;

        public void ReadNames(string srcAlumnosPermitidos, string srcWaitingQueue)
        {
            try
            {
                using (StreamReader sr = new StreamReader(srcAlumnosPermitidos))
                {
                    users = sr.ReadLine().Split(';');
                }
                using (QueueReader sr = new QueueReader(new FileStream(srcWaitingQueue, FileMode.Open)))
                {
                    try
                    {
                        while (true)
                        {
                            Queue peñaEnCola = sr.ReadQueue();
                            waitingQueue.Add(waitingQueue.Count, peñaEnCola);
                        }
                    }
                    catch (EndOfStreamException)
                    {
                        sr.Dispose();
                    }
                }
            }
            catch (Exception ex) when (ex is IOException || ex is FileNotFoundException || ex is ObjectDisposedException || ex is UnauthorizedAccessException)
            {
                Console.WriteLine("no se pudo acceder al archivo");
            }
        }
        public int ReadPin(string src)
        {
            try
            {
                using (BinaryReader br = new BinaryReader(new FileStream(src, FileMode.Open)))
                {
                    return br.ReadInt32();
                }
            }
            catch (Exception ex) when (ex is IOException || ex is FileNotFoundException || ex is ObjectDisposedException || ex is UnauthorizedAccessException)
            {
                return -1;
            }
        }

        public void Init()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            do
            {
                try
                {
                    ie = new IPEndPoint(IPAddress.Any, port);
                    socket.Bind(ie);
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                {
                    port = backUpPort;
                    if (backUpPort < IPEndPoint.MaxPort) backUpPort++;
                    else break;
                }
            } while (!socket.IsBound);

            if (socket.IsBound)
            {
                try
                {
                    string env = Environment.GetEnvironmentVariable("userprofile");
                    ReadNames(env + "/usuarios.txt", env + "/waitingList.txt");
                    socket.Listen(10);
                    while (true)
                    {
                        Console.WriteLine($"Hallo Welt {port}"); //hello world
                        Socket sClient = socket.Accept();
                        Thread hiloCliente = new Thread(ClientThread);
                        hiloCliente.Start(sClient);
                    }
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.Interrupted)
                {
                    Console.WriteLine("Aufwiedersehen Welt");//goodbye world
                }
            }
        }

        public void ClientThread(object sclient)
        {
            Socket soc = (Socket)sclient;
            bool isAdmin = false;
            bool isUser = false;
            try
            {
                using (NetworkStream ns = new NetworkStream(soc))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.WriteLine("HALT, schreib deinen Name bitte"); //el ptsd de tener que buscar declinaciones......socorro        (ALTO, escribe tu nombre porfa)
                    sw.Flush();
                    string? nombreUsuario = sr.ReadLine();
                    if (nombreUsuario != null)
                    {
                        if (nombreUsuario == "admin")
                        {
                            sw.WriteLine("Schreib den PIN (Nummer)"); //escribe el pin
                            sw.Flush();
                            int pin;
                            bool istNummer = int.TryParse(sr.ReadLine(), out pin);
                            string src = Environment.GetEnvironmentVariable("userprofile") + "/pin.bin";
                            int pass = ReadPin(src) == -1 ? 1234 : ReadPin(src);
                            if (istNummer)
                            {
                                isAdmin = pass == pin;
                                if (isAdmin)
                                {
                                    //int 我 = 4; preparate curro...
                                    //int 我们 = 3;
                                    sw.WriteLine("Perfekt! Du kannst AdminBefehl schreiben"); //perfecto, puedes escribir comandos de admin
                                }
                                else
                                {
                                    sw.WriteLine("Das ist nicht das PIN");//ese no es el pin
                                }
                            }
                            else
                            {
                                sw.WriteLine("Kannst du nicht lesen??");//no sabes leer?
                            }
                            sw.Flush();
                        }
                        else
                        {
                            foreach (string usuariosPermitidos in users)
                            {
                                if (nombreUsuario == usuariosPermitidos) isUser = true;
                            }
                            if (isUser)
                            {
                                sw.WriteLine("ok");
                                sw.Flush();
                            }
                        }
                        if (isUser || isAdmin)
                        {
                            do
                            {
                                string? comando = sr.ReadLine();
                                if (comando != null)
                                {
                                    string[] input = comando.Split(' ');
                                    switch (input[0])
                                    {
                                        case string e when e == "del" && isAdmin && input.Length == 2:
                                            if (!Del(input[1]))
                                            {
                                                sw.WriteLine("Programmfehler");//fallo de programa
                                                sw.Flush();
                                            }
                                            break;

                                        case string e when e == "chpin" && isAdmin && input.Length == 2:
                                            if (ChPin(input[1]))
                                            {
                                                sw.WriteLine("Wir aktualisieren das PIN");//actualizamos el pin
                                                sw.Flush();
                                            }
                                            else
                                            {
                                                sw.WriteLine("Wir können nicht das PIN aktualisieren");//no podemos actualizar el pin
                                                sw.Flush();
                                            }
                                            break;

                                        case string e when e == "exit" && isAdmin && input.Length == 1:
                                            isAdmin = false;
                                            GuardaWaitingList();
                                            break;

                                        case string e when e == "shutdown" && isAdmin && input.Length == 1:
                                            isAdmin = false;
                                            GuardaWaitingList();
                                            socket.Close();
                                            break;

                                        case string e when e == "list" && input.Length == 1:
                                            if (waitingQueue.Count > 0)
                                            {
                                                for (int i = 0; i < waitingQueue.Count; i++)
                                                {
                                                    sw.WriteLine($"{i}\t{waitingQueue.GetByIndex(i)}");
                                                    sw.Flush();
                                                }
                                            }
                                            else
                                            {
                                                sw.WriteLine("Der Warteliste ist leer"); //la lista ta vacia
                                                sw.Flush();
                                            }
                                            break;

                                        case string e when e == "add" && input.Length == 1:
                                            
                                            if (waitingQueue.ContainsValue(nombreUsuario))//ª es porque el valor no solo es el nombre, sino tambien el indice y la hora zzzzzz
                                            {
                                                //todo crear una clase, entonces la coleccion no es de strings, y sobreescribo el equals para que solo compruebe el nombre de usuario, no solo la fecha
                                                sw.WriteLine("WARUM versuchst du zwei Mal im Warteliste aufschreiben??"); //por qué te intentas anotar dos veces en la lista???                                      
                                            }
                                            else
                                            {
                                                Queue nuevoUserEnCola = new Queue(nombreUsuario, DateTime.Now.Ticks);
                                                waitingQueue.Add(waitingQueue.Count, nuevoUserEnCola);
                                                sw.WriteLine("Fertig"); //hecho
                                            }
                                            sw.Flush();
                                            break;

                                        default:
                                            sw.WriteLine("Mein Brot kannt nicht schreiben"); //mi pan(a) no sabe escribir
                                            sw.Flush();
                                            break;
                                    }
                                }
                            } while (isAdmin);
                            sw.WriteLine("Tschüss Sternchen <3"); //adios corazón <3
                            sw.Flush();
                        }
                        else
                        {
                            sw.WriteLine("Wer, wer bist du?? WER? WER BIST\t DU"); //quien, quien eres? QUIEN      ERES    (referencia a la cancion: Wer bist du? - Megaherz)
                            sw.Flush();
                        }
                        soc.Close();
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("joer meu que fixeches");
            }
        }


        public void GuardaWaitingList()
        {
            try
            {
                string src = Environment.GetEnvironmentVariable("userprofile") + "/waitingList.txt";
                using (QueueWriter sw = new QueueWriter(new FileStream(src, FileMode.Create)))
                {
                    foreach (Queue queue in waitingQueue.Values)
                    {
                        sw.Write(queue);
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Du kannst nicht mir halten! (waiting queueuueue could not be updated)");
            }
        }
        public bool Del(string input)
        {
            bool passed;
            int pos;
            if (passed = int.TryParse(input, out pos))
            {
                try
                {
                    waitingQueue.Remove(pos);
                    passed = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    passed = false;
                }
            }
            return passed;

        }
        public bool ChPin(string lePin)
        {
            int newPin = 0;
            bool passed = int.TryParse(lePin, out newPin);
            string src = Environment.GetEnvironmentVariable("userprofile") + "/pin.bin";
            if (passed = newPin > 999) //no me gusta pero bueh
            {
                try
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(src, FileMode.OpenOrCreate)))
                    {
                        bw.Write(newPin);
                        passed = true;
                    }
                }
                catch (Exception ex) when (ex is IOException || ex is FileNotFoundException || ex is ObjectDisposedException || ex is UnauthorizedAccessException)
                {
                    passed = false;
                }
            }
            return passed;
        }

    }
}
