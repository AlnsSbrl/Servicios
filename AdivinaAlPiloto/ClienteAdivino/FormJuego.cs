using System;
using System.Collections;
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
    public partial class FormJuego : Form
    {
        string guessWord;
        List<char> alreadyInputedChars;
        char letra;
        int fallosPermitidos = 5;
        int contador = 0;
        int tiempo = 0;
        Timer timer;
        string name;
        Socket socket;
        int port;// = 42069;
        string IPServer;// = "127.0.0.1";
        public FormJuego(string palabra, string name, int port, string IPServer)
        {
            InitializeComponent();
            this.IPServer = IPServer;
            this.port = port;
            this.name = name;
            string[] datos = palabra.Split('$');
            Icon = Properties.Resources.f1;
            guessWord = datos[0];
            alreadyInputedChars = new List<char>();
            Text = guessWord;
            textBox1.MaxLength = 1;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            for (int i = 0; i < guessWord.Length; i++)
            {
                Label lb = new Label();
                lb.Tag = guessWord[i];
                if (guessWord[i] == ' ')
                {
                    lb.Text = " ";
                    lb.Enabled = false;
                }
                else
                {
                    lb.Text = "_";
                    lb.Enabled = true;
                }
                lb.Location = new Point(i * 20, 50);
                lb.Size = new Size(20, 20);
                this.panel1.Controls.Add(lb);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tiempo++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (!alreadyInputedChars.Contains(textBox1.Text[0]))
                {
                    alreadyInputedChars.Add(textBox1.Text[0]);
                    letra = textBox1.Text.ToUpper()[0];
                    bool contieneLaLetra = false;
                    if (guessWord.Contains(letra))
                    {
                        contieneLaLetra = true;
                    }
                    foreach (Label lb in this.panel1.Controls)
                    {
                        if (((char)lb.Tag) == letra)
                        {
                            lb.Text = letra + "";
                        }
                    }
                    //foreach (Label lb in panel1.Controls)
                    //{
                    //    if(((char)lb.Tag) == letra)
                    //    {
                    //        lb.Text = letra.ToString();
                    //        contieneLaLetra = true;
                    //    }
                    //}
                    if (!contieneLaLetra)
                    {
                        contador++;
                        if (contador >= fallosPermitidos)
                        {
                            timer.Stop();
                            Text = "Dedicate a otra cosa macho";
                        }
                    }
                    if (WinCheck())
                    {
                        timer.Stop();
                        bool isRecord = enviaRecord();//que lo puedo poner directamente en un if, pues sí, pero así queda más claro



                        //Text = "OLE OLEEEE VIVA MI ANDALUCIA";
                    }
                }
            }
        }

        private bool WinCheck()
        {
            foreach (Label label in panel1.Controls)
            {
                if (label.Text == "_")
                {
                    return false;
                }
            }
            return true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.Text = e.KeyChar.ToString();

        }
        private bool enviaRecord()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IPServer), port);
                socket.Connect(ep);
            }
            catch (SocketException ex) when (ex.ErrorCode == (int)SocketError.AccessDenied)
            {
                Text = "petó for some reason";
            }
            if (socket.Connected)
            {

                using (NetworkStream ns = new NetworkStream(socket))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.WriteLine("add " + name + tiempo);
                    //Text = "add " + name + tiempo;
                    sw.Flush();
                    string res = sr.ReadLine();
                    Text = res;
                    if (res == "ACCEPT")
                    {
                        return true;
                    }
                }
                socket.Close();
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IPServer), port);
                socket.Connect(ep);
            }
            catch (SocketException ex) when (ex.ErrorCode == (int)SocketError.AccessDenied)
            {
                Text = "petó for some reason";
            }
            if (socket.Connected && txbAddWords.Text!="")
            {
                using (NetworkStream ns = new NetworkStream(socket))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.WriteLine("sendword " + txbAddWords.Text);
                    sw.Flush();
                    string resp= sr.ReadLine();
                    Text= resp;
                }
                socket.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
