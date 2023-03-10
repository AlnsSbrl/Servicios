using System.Net;
using System.Net.Sockets;
namespace ClienteBase
{
    public partial class Form1 : Form
    {
        string IP_SERVER = "127.0.0.1";
        int port = 42069;
        public Form1()
        {
            InitializeComponent();
        }

        private void commandButtonClick(object sender, EventArgs e)
        {
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(IP_SERVER), port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ie);
                using (NetworkStream ns = new NetworkStream(server))
                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    sw.AutoFlush = true;
                    string envio = (string)((Button)sender).Tag;
                    string accion = (Button)sender == btnClose && txtPassword.Text != "" ? envio + " " + txtPassword.Text : envio;
                    sw.WriteLine(accion);
                    string? nuevoTexto = sr.ReadLine();
                    lblResult.Text = nuevoTexto;
                }
                server.Close();
            }
            catch (SocketException eo)
            {
                Console.WriteLine(eo.Message);
            }
            catch (IOException)
            {
                lblResult.Text = "Server disconnected. Please try again after paying us";
            }
        }

        private void changeConnectionClick(object sender, EventArgs e)
        {
            FormConexion f = new FormConexion(IPAddress.Parse(IP_SERVER), port); 
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                IP_SERVER = f.ip.ToString();
                port = f.port;
            }
        }
    }
}