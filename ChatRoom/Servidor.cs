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
        Socket sServer;
        string IP_SERVER;
        int port;
        int backUpPort;
        public void IniciaChatRoom()
        {
            sServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP_SERVER),port);

            }catch(SocketException e) when (e.ErrorCode == (int)SocketError.AddressNotAvailable){

            }
        }
    }
}
