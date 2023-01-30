using ChatRoom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteChat
{
    public partial class SalaChat : Form
    {
        Usuario usuario;
        private readonly object l = new object();
        public SalaChat(object user)
        {
            InitializeComponent();
            usuario = (Usuario)user;
            pictureBox1.Image = Properties.Resources.ic_send_128_28719;
            using (usuario.ns = new NetworkStream(usuario.socket))
            using (usuario.sw = new StreamWriter(usuario.ns))
            using (usuario.sr = new StreamReader(usuario.ns))
            {
                usuario.sw.AutoFlush = true;
                usuario.sr.ReadLine();
                usuario.sw.WriteLine(usuario.nombre);
            }
            Thread hiloLeer = new Thread(LeeMensajes);
            hiloLeer.Start();
        }
        public void actualizaListBox(string msg)
        {

        }
        public void LeeMensajes()
        {
            while (usuario.isConnected)
            {
                using (usuario.ns = new NetworkStream(usuario.socket))
                using (usuario.sr = new StreamReader(usuario.ns))
                {
                    lock (l)
                    {
                        string? msg = usuario.sr.ReadLine();
                        if (msg != null)
                        {
                            this.Invoke(new MethodInvoker(delegate ()
                        {
                            listBoxMensajes.Items.Add(msg);
                            if (listBoxMensajes.Items.Count > 10) listBoxMensajes.Items.RemoveAt(0);
                        }));
                    }
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                using (usuario.ns = new NetworkStream(usuario.socket))
                using (usuario.sw = new StreamWriter(usuario.ns))
                {
                    usuario.sw.Write(textBox1.Text);
                }
            }
        }
    }
}
