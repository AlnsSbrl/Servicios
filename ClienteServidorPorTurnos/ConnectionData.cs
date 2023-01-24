using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServidorPorTurnos
{
    internal class ConnectionData
    {
        public string dni;
        public string ip;
        public int port;
        public ConnectionData(string dni, string ip, int port)
        {
            this.dni = dni;
            this.ip = ip;
            this.port = port;
        }
    }
}
