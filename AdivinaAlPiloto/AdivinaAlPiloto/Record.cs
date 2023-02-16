using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaAlPiloto
{
    internal class Record
    {
        public char[] name;
        public int time;

        public Record(string name, int time)
        {
            
            this.name = name.Substring(0,3).ToUpper().ToCharArray();
            this.time = time;
        }
    }
}
