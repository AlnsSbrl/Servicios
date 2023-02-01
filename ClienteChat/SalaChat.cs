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
        bool haceComando = false;
        string mensajeComando = "";
        public SalaChat(object user)
        {
            InitializeComponent();
            usuario = (Usuario)user;
            pictureBox1.Image = Properties.Resources.ic_send_128_28719;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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
        public void LeeMensajes()
        {
            while (usuario.isConnected)
            {
                //lock (l) WOW TENIA MAL PUESTO ESO ME MEO
                    using (usuario.ns = new NetworkStream(usuario.socket))
                    using (usuario.sr = new StreamReader(usuario.ns))
                    {
                        {
                            if (haceComando)
                            {
                                //todo list, exit
                                haceComando = false;
                            }
                            else
                            {
                                string? msg = usuario.sr.ReadLine();
                                if (msg != null)
                                {
                                    cambiaLosMensajes(msg, true);
                                }
                            }
                        }
                    }
            }
        }
        private void cambiaLosMensajes(string msg, bool posIzq)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listBoxMensajes.Items.Add(posIzq?""+msg:"\t"+msg);
                //esto "tira", pero no me displayea los mensajes como a mi me gustaría
                if (listBoxMensajes.Items.Count > 10) listBoxMensajes.Items.RemoveAt(0);

            }));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //lock (l)
            {
                if (textBox1.Text != "")
                {
                    using (usuario.ns = new NetworkStream(usuario.socket))
                    using (usuario.sw = new StreamWriter(usuario.ns))                
                    {
                        usuario.sw.WriteLine(textBox1.Text);
                        usuario.sw.Flush();
                        string msg = textBox1.Text;
                        cambiaLosMensajes(msg, false);
                        textBox1.Text = "";
                    }
                }
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            //lock (l)
            {
                haceComando = true;
                using (usuario.sw = new StreamWriter(usuario.ns))
                {
                    mensajeComando = ((Button)sender).Tag.ToString();
                    usuario.sw.WriteLine(((Button)sender).Tag.ToString()); //?
                    usuario.sw.Flush();
                }
                if ((Button)sender == btnList)
                {
                    //showDialog con la lista??
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox1_Click(pictureBox1, e); //este evento......funcionaria? lmao
            }
        }
    }
}
