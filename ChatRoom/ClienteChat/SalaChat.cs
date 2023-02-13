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
        bool muestraLista = false;
        bool salir = false;
        public SalaChat(object user)
        {
            InitializeComponent();
            usuario = (Usuario)user;
            Icon = Properties.Resources.telegram_icon_icons_com_72055;
            Text = "Sala " + usuario.numSala;
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
            hiloLeer.IsBackground = true;
        }
        public void LeeMensajes()
        {
            while (usuario.isConnected)
            {
                try
                {
                    using (usuario.ns = new NetworkStream(usuario.socket))
                    using (usuario.sr = new StreamReader(usuario.ns))
                    {
                        string msg = usuario.sr.ReadLine();
                        if (muestraLista)
                        {
                            borraLista();
                            while (muestraLista)
                            {
                                updateUsuarios(msg);
                                msg = usuario.sr.ReadLine();
                                if (msg == "Fin de la lista") muestraLista = false;
                            }
                        }
                        else if (!salir)
                        {
                            cambiaLosMensajes(msg, true);
                        }
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("ups");
                }
            }
        }


        private void borraLista()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                txbListaUsuarios.Text = "";
            }));
        }
        private void cambiaLosMensajes(string msg, bool posIzq)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                //ms bruhhh no me deja colocar mi propio componente T_T
                /*la idea era en un panel ir poniendo el componente mensaje, que separa el nombre del usuario del mensaje
                 y los coloca de manera más vistosa (mensajes del usuario a la derecha, mensajes del resto a la izquierda),
                igual añadir evento on click para responder a ese mensaje...endless ideas*/
                listBoxMensajes.Items.Add(posIzq ? "" + msg : "\t" + msg);
                if (listBoxMensajes.Items.Count > 10) listBoxMensajes.Items.RemoveAt(0);
            }));
        }

        private void updateUsuarios(string usuario)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                txbListaUsuarios.Text += usuario + "\n";
            }));
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
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
                catch (IOException)
                {
                    Console.WriteLine("woops");
                }
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            txbListaUsuarios.Visible = true;
            muestraLista = true;
            try
            {
                using (usuario.ns = new NetworkStream(usuario.socket))
                using (usuario.sw = new StreamWriter(usuario.ns))
                {
                    usuario.sw.WriteLine(((Button)sender).Tag.ToString());
                    usuario.sw.Flush();
                }
            }
            catch (IOException)
            {
                Console.WriteLine("algo pasa mm");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {            
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox1_Click(pictureBox1, e);
            }
        }

        private void SalaChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                salir = true;
                using (usuario.ns = new NetworkStream(usuario.socket))
                using (usuario.sw = new StreamWriter(usuario.ns))
                {
                    usuario.sw.WriteLine("#exit");
                    usuario.sw.Flush();
                }
                //usuario.socket.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("algo pasa mm");
            }
        }
    }
}
