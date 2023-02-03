using ChatRoom;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms.VisualStyles;

namespace ClienteChat
{
    public partial class Form1 : Form
    {
        Socket[] socket = new Socket[3];
        int[] port = new int[3] { 42069, 63000, 6969 };
        bool[] hardDisable = new bool[3] { true, true, true };

        string ip = "127.0.0.1";
        //string nombreUsuario;//="";

        public Form1()
        {
            InitializeComponent();
            Icon = Properties.Resources.telegram_icon_icons_com_72055;
            Text = "Tele miligram";
            permiteAccesoASalas(false);
            for (int i = 0; i < socket.Length; i++)
            {
                socket[i] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        private void btnSala1_Click(object sender, EventArgs e)
        {
            int i = 0;
            bool isConnected = false;
            do
            {
                try
                {
                    int port = 0;
                    int.TryParse(((Button)sender).Tag.ToString(), out port);
                    IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), port);
                    socket[i].Connect(ie); //claro, habrá que cerrarla no??
                    Usuario user = new Usuario(socket[i], textBox1.Text, i);
                    isConnected = true;
                    SalaChat formSala = new SalaChat(user);
                    formSala.Show();
                    hardDisable[i] = false;
                    permiteAccesoASalas(true);
                }
                catch (SocketException ex) when (ex.ErrorCode == (int)SocketError.AddressAlreadyInUse || (ex.ErrorCode == (int)SocketError.IsConnected))
                {
                    if (i < socket.Length) i++;
                }
            } while (!isConnected);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text.Contains("@"))
            {
                permiteAccesoASalas(false);
            }
            else
            {
                permiteAccesoASalas(true);
            }
        }
        private void permiteAccesoASalas(bool isPermitted)
        {
            btnSala0.Enabled = isPermitted && hardDisable[0];
            btnSala1.Enabled = isPermitted && hardDisable[1];
            btnSala2.Enabled = isPermitted && hardDisable[2];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < socket.Length; i++)
            {
                if (socket[i].IsBound)
                {
                    try
                    {

                        socket[i].Close();
                    }
                    catch (IOException)
                    {

                    }
                }
            }
        }
    }
}