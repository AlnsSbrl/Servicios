using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ShiftServer
{
    internal class ShiftServer
    {
        string[] users;
        List<string> waitingList;
        Socket socket;
        int port = 31416;
        int backUpPort = 1024;
        IPEndPoint ie;

        public void ReadNames(string src)
        {
            try
            {
                using (StreamReader sr = new StreamReader(src))
                {
                    users = sr.ReadLine().Split(';');
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
                    ie = new IPEndPoint(IPAddress.Loopback, port);
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
                    ReadNames(Environment.GetEnvironmentVariable("userprofile") + "/usuarios.txt");
                    socket.Listen(10);
                    while (true)
                    {
                        Console.WriteLine($"Hallo Welt {port}");
                        Socket sClient = socket.Accept();
                        Thread hiloCliente = new Thread(ClientThread);
                        hiloCliente.Start(sClient);
                    }
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.Interrupted)
                {
                    Console.WriteLine("Aufwiedersehen Welt");
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
                    sw.WriteLine("HALT, schreib deinen Name bitte"); //el ptsd de tener que buscar declinaciones......
                    sw.Flush();
                    string? nombreUsuario = sr.ReadLine();
                    if (nombreUsuario != null)
                    {
                        if (nombreUsuario == "admin")
                        {
                            sw.WriteLine("Schreib den PIN (Nummer)");
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
                                    sw.WriteLine("Perfekt! Du kannst AdminBefehl schreiben");
                                }
                                else
                                {
                                    sw.WriteLine("Das ist nicht das PIN");
                                }
                            }
                            else
                            {
                                sw.WriteLine("Kannst du nicht lesen??");
                            }
                            sw.Flush();
                        }
                        else
                        {
                            foreach (string usuariosPermitidos in users)
                            {
                                if (nombreUsuario == usuariosPermitidos) isUser = true;
                            }
                            if (isUser) sw.WriteLine("o k");
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
                                        case string e when e == "del" && isAdmin:
                                            break;

                                        case string e when e == "chpin" && isAdmin:
                                            break;

                                        case string e when e == "exit" && isAdmin:
                                            isAdmin = false;
                                            break;

                                        case string e when e == "shutdown" && isAdmin:
                                            socket.Close();
                                            break;

                                        case "list":
                                            break;

                                        case "add":
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }while (isAdmin);
                        }
                        else
                        {
                            sw.WriteLine("Warnung! Du bist Scheisse für uns!");
                        }

                    }
                }
            }
            catch (IOException)
            {

            }
        }

        public void List()
        {

        }
        public void Add()
        {

        }
        public void Del(int pos)
        {

        }
        public void ChPin(int newPin)
        {

        }
        public void Exit()
        {
            //no hace falta
        }
        public void ShutDown()
        {
            //no hace falta (o igual sí, en plan, lo hace mas claro)
        }
    }
}
