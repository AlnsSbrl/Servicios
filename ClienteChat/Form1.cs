using ChatRoom;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms.VisualStyles;

namespace ClienteChat
{
    public partial class Form1 : Form
    {
        Socket[] socket = new Socket[3];
        int[] port = new int[3] {42069,63000,6969};
        string ip="127.0.0.1";
        string nombreUsuario="";

        public Form1()
        {
            InitializeComponent();
            Icon = Properties.Resources.telegram_icon_icons_com_72055;

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
                    int port=0;
                    int.TryParse( ((Button)sender).Tag.ToString(), out port);
                    IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip),port);
                    socket[i].Connect(ie); //en verdad NO dará problema de conexion......unlucky
                    Usuario user = new Usuario(socket[i],textBox1.Text,i);
                    isConnected = true;
                    SalaChat formSala = new SalaChat(user);
                    formSala.Show();
                }
                catch (SocketException ex) when (ex.ErrorCode == (int)SocketError.AddressAlreadyInUse)
                {
                    if (i < socket.Length) i++;
                }
            } while (!isConnected);

        }
    }
}