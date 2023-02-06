using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftServer
{
    internal class Queue
    {
        public string nombre;
        public DateTime fecha;
        public Queue(string nombre, long fecha)
        {
            this.nombre = nombre;       
            this.fecha = new DateTime(fecha,DateTimeKind.Local);
        }

        public override bool Equals(object? obj)
        {        
            if (obj == null) return false;
            return ((string)obj) == this.nombre; 
        }

        public override string ToString()
        {
            return this.nombre+"\t"+fecha;
        }
    }
}
