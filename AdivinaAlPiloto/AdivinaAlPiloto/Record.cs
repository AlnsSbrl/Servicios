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

        public Record(char[] name, int time)
        {   
            this.name = name;
            this.time = time;
        }
        public Record(string name,int time)
        {
            if (name.Length > 3)
            {
                name=name.Substring(0,3);
            }
            this.name = name.ToCharArray();
            this.time = time;
        }
    }
}
