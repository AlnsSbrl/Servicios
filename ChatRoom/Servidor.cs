using System;
using System.Collections.Generic;
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
        //realmente esas 3 propiedades son arrays del mismo tamaño, para cada sala......-> podria ponerlas en la misma clase i guess
        string IP_SERVER;
        bool keepTrying = true;

        public Servidor()
        {
            IP_SERVER = "127.0.0.1";
            socketSalaChat = new Socket[3];
            puertosSalaChat = new int[] {42069,63000,6969}; //aqui tendria que hacer un math random? le asigno los valores a pelo a las salas...?
            //todo igual hacer que el numero de salas sea fijo, con los puertos fijos.....pero asi garantizas que sean siempre los mismos...?idk
            //igual hacer un archivo con los puertos que yo le pongo...y si no va coge el siguiente...asi hasta fin del archivo?
            //y que el cliente tmb acceda a esos archivos para entrar en la sala de chats...?
            //port = 42069;
            //backUpPort = 64000;
            listaDeUsuarios = new List<Usuario>[3];          
        }
        public void IniciarChatRoom()
        {
            int numSala = 0;
            do
            {
                try
                {
                    socketSalaChat[numSala] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint ep = new IPEndPoint(IPAddress.Any, puertosSalaChat[numSala]);
                    //IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP_SERVER), port);
                    socketSalaChat[numSala].Bind(ep);
                    socketSalaChat[numSala].Listen(10);
                    listaDeUsuarios[numSala] = new List<Usuario>();
                    while (true)
                    {
                        Console.WriteLine("conectense a " + puertosSalaChat[numSala]);
                        Socket sClient = socketSalaChat[numSala].Accept();
                        using (NetworkStream ns = new NetworkStream(sClient))
                        using (StreamReader sr = new StreamReader(ns))
                        using (StreamWriter sw = new StreamWriter(ns))
                        {
                            sw.WriteLine("Introduce nombre usuario");
                            sw.Flush();
                            string nombre = sr.ReadLine();
                            Usuario user = new Usuario(sClient, nombre,numSala);
                            Thread thread = new Thread(UserInChat);
                            thread.Start(user);
                        }
                    }
                }
                catch (SocketException e) when ((e.ErrorCode == (int)SocketError.AddressNotAvailable) || e.ErrorCode == (int)SocketError.InvalidArgument || e.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                {
                    if (numSala<socketSalaChat.Length-1) numSala++;
                    else keepTrying = false;
                }
                catch (SocketException e) when (e.ErrorCode == (int)SocketError.Interrupted)
                {
                    Console.WriteLine("Sala vacía, apagando...");
                    keepTrying = false;
                    //esto por poner algo para cerrarlo y no tener todo el tiempo un while true
                }
            } while (keepTrying);
        }

        public string MuestraLista(int numSala)
        {
            string res = "Lista de usuarios conectados:";
            foreach (Usuario usuario in listaDeUsuarios[numSala])
            {
                res += "\n\r" + usuario.nombre + "@" + usuario.ie.Address;
            }
            return res;
        }

        public void UserInChat(object user)
        {
            Usuario cliente = (Usuario)user;
            listaDeUsuarios[cliente.numSala].Add(cliente);
            foreach (Usuario usuario in listaDeUsuarios[cliente.numSala])
            {
                usuario.sw.WriteLine($"Usuario {cliente.nombre}@{cliente.ie.Address} se ha unido a la sala");
            }
            using (cliente.ns = new NetworkStream(cliente.socket))
            using (cliente.sw = new StreamWriter(cliente.ns))
            using (cliente.sr = new StreamReader(cliente.ns))
            {
                while (cliente.isConnected)
                {
                    string? mensaje = cliente.sr.ReadLine();
                    if (mensaje == "#exit" || mensaje == null)
                    {
                        cliente.isConnected = false;
                        foreach (Usuario usuario in listaDeUsuarios[cliente.numSala])
                        {
                            usuario.sw.WriteLine($"{cliente.nombre}@{cliente.ie.Address} se ha desconectado");
                        }
                        listaDeUsuarios[cliente.numSala].Remove(cliente);
                        if (listaDeUsuarios[cliente.numSala].Count == 0) socketSalaChat[cliente.numSala].Close();
                    }
                    else if (mensaje == "#lista")
                    {
                        cliente.sw.WriteLine(MuestraLista(cliente.numSala));
                    }
                    else
                    {
                        foreach (Usuario usuario in listaDeUsuarios[cliente.numSala])
                        {
                            if (usuario != cliente) usuario.sw.WriteLine($"{cliente.nombre}@{cliente.ie.Address}: " + mensaje);
                        }
                    }
                }
            }
        }
    }
}
