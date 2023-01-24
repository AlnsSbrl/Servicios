using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServidorPorTurnos
{
    internal class ConnectionDataReader : BinaryReader
    {
        public ConnectionDataReader(Stream input) : base(input)
        {
        }
        public ConnectionData read()
        {
            string dni = base.ReadString();
            string ip = base.ReadString();
            int port = base.ReadInt32();
            return new ConnectionData(dni, ip, port);
        }
    }
}
