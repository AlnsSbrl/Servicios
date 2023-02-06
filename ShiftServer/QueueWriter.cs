using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftServer
{
    internal class QueueWriter:BinaryWriter
    {
        public QueueWriter(Stream str) : base(str)
        {
        }

        public void Write(Queue queue)
        {
            base.Write(queue.nombre);
            base.Write(queue.fecha.Ticks);
        }
    }
}
