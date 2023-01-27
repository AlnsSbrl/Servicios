using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TodosMisServidores
{
    internal class Servicio
    {
        IPAddress ip;
        int port;
        string password;
        Socket s;
        bool occupiedSocket;
        public Servicio(int port)
        {
            ip = IPAddress.Loopback;
            this.port = port;
        }
        public void conexion(int opcionServicio)
        {
            do
            {
                IPEndPoint ie = new IPEndPoint(IPAddress.Any, port);
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    s.Bind(ie);
                    s.Listen(10);
                    while (true)
                    {
                        Console.WriteLine("aber conectense a " + port);
                        Socket sClient = s.Accept();
                        Thread clientThread;
                        switch (opcionServicio)
                        {
                            case 1:
                                clientThread = new Thread(FechaHora);
                                break;
                            default:
                                clientThread = null;
                                break;
                        }
                        if (clientThread == null) break;
                        clientThread.Start(sClient);
                    }
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                {
                    Console.WriteLine("puerto ocupao");
                    port += 1000;
                    occupiedSocket = true;//AQUI HAY QUE HACER MAS COSAS, varios intentos, a gusto de programador
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.AddressNotAvailable)
                {
                    Console.WriteLine("no se ha encontrao un puerto libre");
                    occupiedSocket = false;
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.Interrupted)
                {
                    Console.WriteLine("imma head out");
                    occupiedSocket = false;
                }
            } while (occupiedSocket);
        }


        public void FechaHora(object socketClient)
        {
            Socket socket = (Socket)socketClient;
            IPEndPoint endPoint = (IPEndPoint)socket.RemoteEndPoint;
            Console.WriteLine($"Connected with client {endPoint.Address} at port {endPoint.Port}");
            using (NetworkStream ns = new NetworkStream(socket))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                sw.AutoFlush = true;
                string? input = sr.ReadLine();
                string[] inputUser;
                if (input != null)
                {
                    inputUser = input.Split(" ");
                    if (inputUser.Length == 1 && inputUser != null)
                    {
                        switch (inputUser[0])
                        {
                            case "time":
                                sw.WriteLine(DateTime.UtcNow.TimeOfDay); //me da pereza ponerlo sin milliseconds
                                break;
                            case "date":
                                sw.WriteLine($"{DateTime.UtcNow.Day}/{DateTime.UtcNow.Month}/{DateTime.UtcNow.Year}");
                                break;
                            case "all":
                                sw.WriteLine(DateTime.UtcNow);
                                break;
                            case string d when d == "help" || d == "?":
                                sw.WriteLine("time\t shows the current time" +
                                    "\r\ndate\t shows the current date format dd/MM/yyyy" +
                                    "\r\nall\t shows the current date, including the time" +
                                    "\r\nclose +pswrd\t attempts to close the server, only the right password will do so"
                                    );
                                break;
                            case "close":
                                sw.WriteLine("te crees que no tenemos contraseña bro?");
                                break;
                            default:
                                sw.WriteLine("Error, connect and try again. Use the 'help' command to know the correct syntax to use next time u lonely fucker");
                                break;
                        }
                    }
                    else if (inputUser[0] == "close" && inputUser != null)
                    {
                        try
                        {
                            string texto = Environment.GetEnvironmentVariable("ProgramData") + "/password.txt";
                            Console.WriteLine("ruta: " + texto);
                            using (StreamReader srpass = new StreamReader(texto))
                            {
                                password = srpass.ReadLine();
                            }
                        }
                        catch (IOException)
                        {
                            password = "1";
                        }
                        string laPass = "";
                        for (int i = 0; i < inputUser.Length - 1; i++)
                        {
                            laPass += inputUser[i + 1] + " ";
                        }
                        laPass = laPass.TrimEnd(' ');
                        if (laPass == password)
                        {
                            sw.WriteLine("LONG  LIVE    THE KING");
                            s.Close();
                        }
                        else
                        {
                            sw.WriteLine("You can't stop me");
                        }
                    }
                    else
                    {
                        sw.WriteLine("Error, connect and try again. Use the 'help' command to know the correct syntax to use next time u lonely fucker");
                    }
                }
                socket.Close();
            }
        }
    }
}
