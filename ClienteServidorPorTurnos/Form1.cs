using System.Net;
using System.Net.Sockets;

namespace ClienteServidorPorTurnos
{
    public partial class Form1 : Form
    {
        string ip;
        int port;// = 0;
        string respuestaServidor;
        public Form1()
        {
            InitializeComponent();

            btnCambiarConexion.Text = txbDNIUsuario.Enabled ? "Cambiar conexión" : "Establecer conexión";

            try
            {
                using (ConnectionDataReader wr = new ConnectionDataReader(new FileStream(Environment.CurrentDirectory+"/data.bin", FileMode.Open)))
                {
                    txbDNIUsuario.Text = wr.ReadString();
                    ip = wr.ReadString();
                    port = wr.ReadInt32();
                }
                setEnable(true);
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is IOException)
                {
                    ip = "0";
                    port = 0;
                    setEnable(false);
                }
                else
                {
                    throw;
                }
            }
            //aqui inicializar los valores de IP y port leyendo. Si no se hace estarán deshabilitados el resto de botones
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), (int)port);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(iPEndPoint);
            using (NetworkStream ns = new NetworkStream(sock))
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                sw.AutoFlush = true;
                sw.WriteLine(((Button)sender).Tag.ToString().ToLower());
                respuestaServidor = sr.ReadLine(); //supongo que aunque sea una lista de cosas sigue siendo una string (larga but)
                //supongo que dependiendo de la respuesta (o del sender) coloco esto en un lado u otro
                if ((Button)sender == btnList)
                {
                    listBox1.Items.AddRange(respuestaServidor.Split('\n'));
                }
                else
                {
                    label2.Text=respuestaServidor;
                }
            }
            sock.Close();
        }
        private void setEnable(bool setEnable)
        {
            txbDNIUsuario.Enabled = setEnable;
            btnAdd.Enabled = setEnable;
            btnList.Enabled = setEnable;
            btnCambiarConexion.Text = setEnable?"Cambiar conexión":"Nueva conexión";
        }

        private void btnCambiarConexion_Click(object sender, EventArgs e)
        {
            FormConexion f = new FormConexion(ip, port);
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK && !f.ip.Equals("0") && f.port != 0)
            {
                ip = f.ip.ToString();
                port = (int)f.port;
                setEnable(true);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                using (ConnectionDataWriter wr = new ConnectionDataWriter(new FileStream(Environment.CurrentDirectory + "/data.bin", FileMode.Create)))
                {
                    wr.write(new ConnectionData(txbDNIUsuario.Text, ip, port));
                }
            }
            catch (IOException)
            {

                throw;
            }

        }
    }
}