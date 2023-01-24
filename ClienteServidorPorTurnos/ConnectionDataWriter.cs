using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServidorPorTurnos
{
    internal class ConnectionDataWriter : BinaryWriter
    {
       public void write(ConnectionData cd)
        {
            base.Write(cd.dni);
            base.Write(cd.ip);
            base.Write(cd.port);
        }
        public ConnectionDataWriter(Stream str):base(str)          
        {
        }
    }
}
