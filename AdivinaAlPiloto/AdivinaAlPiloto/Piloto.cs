using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaAlPiloto
{
	internal class Piloto
	{
		public string name;
		public string familyName;
		public string nationality;

		public Piloto(string name, string familyName, string nationality)
		{
			this.name = name;
			this.familyName = familyName;
			this.nationality = nationality;
		}
	}
}
