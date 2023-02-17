using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaAlPiloto
{
    internal class RecordReader : BinaryReader
    {
        public RecordReader(Stream input) : base(input)
        {
        }

        public Record ReadRecord()
        {
            char[] name = base.ReadChars(3);
            int time = base.ReadInt32();
            return new Record(name, time);
        }
    }
}
