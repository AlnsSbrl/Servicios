using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom
{
    internal class Usuario
    {
        public string nombre;
        public int numSala;
        public IPEndPoint ie;
        public Socket socket;
        public NetworkStream ns;
        public StreamWriter sw;
        public StreamReader sr;
        public bool isConnected;
        //public Stream imageStream;

        public Usuario(Socket socket, string nombre, int numSala)
        {
            this.nombre = nombre;
            this.socket = socket;
            this.numSala = numSala;
            ie = (IPEndPoint)socket.RemoteEndPoint;
            ns = new NetworkStream(socket);
            sw = new StreamWriter(ns);
            sr = new StreamReader(ns);
            sw.AutoFlush = true;
            isConnected = true;
            //imageStream =null; //esto para el iconotm del modo grafico
        }
    }
}
