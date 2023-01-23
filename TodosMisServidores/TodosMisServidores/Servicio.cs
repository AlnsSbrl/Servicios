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
        bool activeService;
        string password;

        public Servicio(int port)
        {
            ip = IPAddress.Loopback;
            this.port = port;
        }
        public void conexion(int opcionServicio)
        {          
            activeService = true;
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, port);
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                s.Bind(ie);
                s.Listen(10);
                while (activeService)
                {
                    Console.WriteLine("aber conectense");
                    Socket sClient = s.Accept();
                    Thread clientThread;                   
                    if (activeService)
                    {
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
                Console.WriteLine("Curro, I don't feel so good......");
            }
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
                //sw.WriteLine("Wellcome to the dating app");
                //string opcion = sr.ReadLine();
                //Console.WriteLine(opcion);
                string[] inputUser=sr.ReadLine().Split(" ");
                switch (inputUser[0]) //igual no es lo mejor hacer esto, por lo menos en el caso de la contraseña
                {
                    case "time":
                        sw.WriteLine(DateTime.UtcNow.TimeOfDay); //me da pereza ponerlo sin ms
                        break;
                    case "date":
                        sw.WriteLine($"{DateTime.UtcNow.Day}/{DateTime.UtcNow.Month}/{DateTime.UtcNow.Year}");
                        break;
                    case "all":
                        sw.WriteLine(DateTime.UtcNow);
                        break;
                    case string d when d=="help" || d=="?":
                        sw.WriteLine("time\t shows the current time"+
                            "\ndate\t shows the current date format dd/MM/yyyy"+
                            "\nall\t shows the current date, including the time"+
                            "\nclose +pswrd\t attempts to close the server, only the right password will do so"
                            );
                        break;
                    case "close":
                        Console.WriteLine("entra");
                        try
                        {
                            string texto = Environment.GetEnvironmentVariable("ProgramData") + "/password.txt";
                            Console.WriteLine("ruta: "+texto);
                            StreamReader srpass = new StreamReader(texto);
                            password = srpass.ReadLine();
                            srpass.Close();
                        }
                        catch (IOException)
                        {

                            password = "1";
                        }
                        if (inputUser[1].Length == 0)
                        {
                            sw.WriteLine("te crees que no tenemos contraseña bro?");
                        }else if(inputUser[1] == password)
                        {
                            sw.WriteLine("LONG  LIVE    THE KING");
                            activeService = false;
                        }
                        else
                        {
                            sw.WriteLine("You can't stop me");
                        }
                        break;
                    default:
                        sw.WriteLine("U got no bitches, connect and try again. Use the 'help' command to know the correct syntax to use next time u lonely fucker");
                        break;
                }
                Console.WriteLine("contraseña: "+password);
            }
            socket.Close();
        }
    }
}
