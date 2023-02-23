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
        string name="";
        public Form1()
        {
            InitializeComponent();
            btnPlay.Enabled = false;
        }

        private void ButtonConnection_Click(object sender, EventArgs e)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IPServer), port);
                sock.Connect(ep);
            }
            catch (SocketException ex) when (ex.ErrorCode == (int)SocketError.AccessDenied)
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
                    string palabro = sr.ReadLine();
                    if (palabro != null)
                    {
                        name = textBox1.Text;
                        FormJuego f = new FormJuego(palabro,name,port,IPServer);
                        f.ShowDialog();
                    }
                }
            }
            sock.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length!=3)
            {
                btnPlay.Enabled = false;
                textBox1.BackColor = Color.Red;
            }
            else
            {
                btnPlay.Enabled=true;
                textBox1.BackColor = Color.White;
            }
        }
    }
}
