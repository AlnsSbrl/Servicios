using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaAlPiloto
{
	internal class ParametroCliente
	{
		public Socket socket;
		public List<int> years;
		public NetworkStream ns;
		public StreamReader sr;
		public StreamWriter sw;

		public ParametroCliente(Socket socket)
		{
			this.socket = socket;
			ns = new NetworkStream(socket);
			sr = new StreamReader(ns);
			sw = new StreamWriter(ns);
			years = new List<int>();
			years.Add(2022);
		}
	}
}
