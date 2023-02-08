using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaAlPiloto
{
    internal class RecordWriter : BinaryWriter
    {
        public RecordWriter(Stream str):base(str) { }

        public void Write(Record record) {
            base.Write(record.name);
            base.Write(record.time);
        }    
    }
}
