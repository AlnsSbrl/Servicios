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
            try
            {
                StreamReader sr = new StreamReader(Environment.GetEnvironmentVariable("programdata")+"/password.txt");
                password = sr.ReadLine();//duda, la contraseña se actualiza cada vez que el usuario la cambia en el textbox?
                //es decir, el usuario puede modificarla mientras en servidor esta lanzado y con ello cerrarlo?
                //ahora mismo coge la contraseña y hasta que se cierra y se vuelve a lanzar no la cambia, lo dejo asi o como?
            }
            catch (IOException)
            {
                password = "";
            }
        }
        //activa el servicio elegido en el puerto seleccionado, igual deberia hacerlo a prueba de que el puerto esté ocupado
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
                sw.WriteLine("Wellcome to the dating app");
                string opcion = sr.ReadLine();
                switch (opcion) //igual no es lo mejor hacer esto, por lo menos en el caso de la contraseña
                {
                    case string a when a.ToLower().StartsWith("time"):
                        sw.WriteLine(DateTime.UtcNow.TimeOfDay); //me da pereza ponerlo sin ms
                        break;
                    case string b when b.ToLower().StartsWith("date"):
                        sw.WriteLine($"{DateTime.UtcNow.Day}/{DateTime.UtcNow.Month}/{DateTime.UtcNow.Year}");
                        break;
                    case string c when c.ToLower().StartsWith("all"):
                        sw.WriteLine(DateTime.UtcNow);
                        break;
                    case string d when d.ToLower().StartsWith("help") || d.ToLower().StartsWith("?"):
                        sw.WriteLine("time\t shows the current time"+
                            "\ndate\t shows the current date format dd/MM/yyyy"+
                            "\nall\t shows the current date, including the time"+
                            "\nclose +pswrd\t attempts to close the server, only the right password will do so"
                            );
                        break;
                    case string c when c.ToLower() == "close":
                        sw.WriteLine("mi pana pon la password correctamente");
                        break;
                    case string c when c.ToLower().StartsWith("close "):
                        if (c.Substring(6,c.Length-6)==password)
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
                sw.WriteLine("venga chao");
            }
            socket.Close();
        }
    }
}
