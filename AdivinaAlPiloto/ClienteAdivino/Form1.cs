using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteAdivino
{
    public partial class Form1 : Form
    {
        int port = 42069;
        string IPServer = "127.0.0.1";
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonConnection_Click(object sender, EventArgs e)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IPServer), port);
                sock.Connect(ep);
            }
            catch (SocketException ex) when (ex.ErrorCode==(int)SocketError.AccessDenied)
            {
                Text = "petó for some reason";
            }
            if (sock.Connected)
            {
                using (NetworkStream ns = new NetworkStream(sock))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.WriteLine(((Button)sender).Tag.ToString());
                    sw.Flush();
                    Text = sr.ReadLine();
                }
            }
            sock.Close();
        }
    }
}
