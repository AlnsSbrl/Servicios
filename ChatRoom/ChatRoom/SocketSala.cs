using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom
{
    internal class SocketSala
    {
        public Socket socket;
        public int sala;
        public SocketSala(Socket socket, int sala)
        {
            this.socket = socket;
            this.sala = sala;
        }
    }
}
