using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftServer
{
    internal class QueueReader : BinaryReader
    {
        public QueueReader(Stream input) : base(input)
        {
        }

        public Queue ReadQueue()
        {
            string nombre = base.ReadString();
            long ticks = base.ReadInt64();
            return new Queue(nombre, ticks);
        }
    }
}
