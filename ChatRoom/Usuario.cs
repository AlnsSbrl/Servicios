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

        public Usuario(Socket socket, string nombre, int numSala)
        {
            this.nombre = nombre;
            this.socket = socket;
            this.numSala = numSala;
            ie = (IPEndPoint)socket.RemoteEndPoint;
            sw.AutoFlush = true;
            isConnected = true;
        }
    }
}
