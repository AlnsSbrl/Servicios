using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom
{
    internal class Servidor
    {
        Socket[] socketSalaChat;
        int[] puertosSalaChat;
        List<Usuario>[] listaDeUsuarios;
        //realmente esas 3 propiedades son arrays del mismo tamaño, para cada sala......-> podria ponerlas en la misma clase I guess
        bool keepTrying = true;

        public Servidor()
        {
            socketSalaChat = new Socket[3];
            for (int i = 0; i < socketSalaChat.Length; i++)
            {
                socketSalaChat[i] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            puertosSalaChat = new int[] { 42069, 63000, 64000 };
            listaDeUsuarios = new List<Usuario>[3];
        }
        public void IniciarChatRoom()
        {
            int numSala = 0;
            do
            {
                try
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Any, puertosSalaChat[numSala]);
                    socketSalaChat[numSala].Bind(ep);
                    socketSalaChat[numSala].Listen(10);
                    listaDeUsuarios[numSala] = new List<Usuario>();
                    while (true)
                    {
                        Console.WriteLine("conectense a la sala " + numSala + " puerto " + puertosSalaChat[numSala]);
                        Socket sClient = socketSalaChat[numSala].Accept();
                        SocketSala soc = new SocketSala(sClient, numSala);
                        Thread thread = new Thread(UserInChat);
                        thread.Start(soc);
                    }
                }
                catch (SocketException e) when ((e.ErrorCode == (int)SocketError.AddressNotAvailable) || e.ErrorCode == (int)SocketError.InvalidArgument || e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                {
                    if (numSala < socketSalaChat.Length - 1) numSala++;
                    else keepTrying = false;
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.Interrupted)
                {
                    Console.WriteLine($"Sala {numSala} vacía, se procede a cerrarla...");
                    keepTrying = false;
                    //esto por poner algo para cerrarlo y no tener todo el tiempo un while true
                }
            } while (keepTrying);
        }

        public string MuestraLista(int numSala)
        {
            string res = "Lista de personas en la sala:";
            foreach (Usuario usuario in listaDeUsuarios[numSala])
            {
                res += "\n\r" + usuario.nombre + "@" + usuario.ie.Address;
            }
            return res;
        }

        public void UserInChat(object socketSala)
        {
            SocketSala socSala = (SocketSala)socketSala;
            string? nombre;
            using (NetworkStream ns = new NetworkStream(socSala.socket))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
                do
                {
                    sw.WriteLine("Introduce nombre usuario");
                    sw.Flush();
                    nombre = sr.ReadLine();
                    if (nombre != null)
                    {
                        if (nombre == "")
                        {
                            sw.WriteLine("Es necesario un nombre de usuario");
                            sw.Flush();
                        }
                        else if (nombre.Contains("@"))
                        {
                            sw.WriteLine("No se permiten '@' en el nombre de usuario");
                            sw.Flush();
                        }
                    }
                    else
                    {
                        break;
                    }
                } while (nombre == "" || nombre.Contains("@"));

            if (nombre != null)
            {
                Usuario cliente = new Usuario(socSala.socket, nombre, socSala.sala);
                listaDeUsuarios[socSala.sala].Add(cliente);
                Console.WriteLine(cliente.nombre + "@" + cliente.ie.Address);
                try
                {
                    using (cliente.ns = new NetworkStream(cliente.socket))//me petaba aqui porque el cliente CERRABA el socket
                    using (cliente.sw = new StreamWriter(cliente.ns))
                    using (cliente.sr = new StreamReader(cliente.ns))
                    {
                        cliente.sw.AutoFlush = true;
                        foreach (Usuario usuario in listaDeUsuarios[cliente.numSala])
                        {
                            usuario.sw.WriteLine($"Usuario {cliente.nombre}@{cliente.ie.Address} se ha unido a la sala {cliente.numSala}");
                        }
                        while (cliente.isConnected)
                        {
                            string? mensaje = cliente.sr.ReadLine();
                            if (mensaje == "#exit" || mensaje == null)
                            {
                                cliente.isConnected = false;
                                listaDeUsuarios[cliente.numSala].Remove(cliente);
                                foreach (Usuario usuario in listaDeUsuarios[cliente.numSala])
                                {
                                    usuario.sw.WriteLine($"{cliente.nombre}@{cliente.ie.Address} se ha desconectado");
                                }
                                if (listaDeUsuarios[cliente.numSala].Count == 0) socketSalaChat[cliente.numSala].Close();
                            }
                            else if (mensaje == "#lista")
                            {
                                cliente.sw.WriteLine(MuestraLista(cliente.numSala));
                                cliente.sw.WriteLine("Fin de la lista");
                            }
                            else
                            {
                                foreach (Usuario usuario in listaDeUsuarios[cliente.numSala])
                                {
                                    if (usuario != cliente) usuario.sw.WriteLine($"{cliente.nombre}@{cliente.ie.Address}: " + mensaje);
                                }
                            }
                        }
                        cliente.socket.Close();
                    }
                }
                catch (IOException)
                {
                    listaDeUsuarios[cliente.numSala].Remove(cliente);
                    foreach (Usuario usr in listaDeUsuarios[cliente.numSala])
                    {
                        usr.sw.WriteLine($"{cliente.nombre}@{cliente.ie.Address} tried to swim in lava");
                    }
                }
            }
        }
    }
}
