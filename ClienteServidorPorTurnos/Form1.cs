using System.Net;
using System.Net.Sockets;

namespace ClienteServidorPorTurnos
{
    public partial class Form1 : Form
    {
        string ip = "198.168.20.100";
        int port = 5001;
        string respuestaServidor;
        public Form1()
        {
            InitializeComponent();
            //aqui inicializar los valores de IP y port leyendo. Si no se hace estarán deshabilitados el resto de botones
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(iPEndPoint);
            using (NetworkStream ns = new NetworkStream(sock))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                sw.AutoFlush=true;
                sw.WriteLine(((Button)sender).Tag.ToString().ToLower());
                respuestaServidor= sr.ReadLine(); //supongo que aunque sea una lista de cosas sigue siendo una string (larga but)
                //supongo que dependiendo de la respuesta (o del sender) coloco esto en un lado u otro
            }
            sock.Close();
        }

        private void btnCambiarConexion_Click(object sender, EventArgs e)
        {
            FormConexion f = new FormConexion(IPAddress.Parse(ip), port);
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                ip = f.ip.ToString();
                port = f.port;
            }
        }
    }
}