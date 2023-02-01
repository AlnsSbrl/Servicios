using System.Net;
using System.Net.Sockets;

namespace ClienteServidorPorTurnos
{
    public partial class Form1 : Form
    {
        string ip;
        int port;
        string respuestaServidor;
        public Form1()
        {
            InitializeComponent();
            Icon = Properties.Resources.Twitter_icon_icons_com_66803;
            label2.Text = "";
            btnCambiarConexion.Text = txbDNIUsuario.Enabled ? "Cambiar conexión" : "Establecer conexión";
            try
            {
                using (ConnectionDataReader wr = new ConnectionDataReader(new FileStream(Environment.CurrentDirectory + "/data.bin", FileMode.Open)))
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
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Socket sock = null;
            bool hasConnected;
            try
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), (int)port);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(iPEndPoint);
                hasConnected = true;
            }
            catch (SocketException)
            {
                label2.Text = "mi pana no se pudo establecer conexión :(";
                setEnable(false);
                hasConnected = false;
            }
            if (hasConnected)
            {
                try
                {
                    using (NetworkStream ns = new NetworkStream(sock))
                    using (StreamReader sr = new StreamReader(ns))
                    using (StreamWriter sw = new StreamWriter(ns))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine("user " + txbDNIUsuario.Text);
                        string msg = sr.ReadLine();
                        label2.Text = msg;
                        if (msg != "ERROR02" && msg != "ERROR01")
                        {
                            sw.WriteLine(((Button)sender).Tag.ToString().ToLower());
                            respuestaServidor = sr.ReadLine();
                            label2.Text = respuestaServidor;
                            if (sender == btnList)
                            {
                                listAlumnos.Items.Clear();
                                do
                                {
                                    string? alumno = sr.ReadLine();

                                    if (alumno == null) break;

                                    listAlumnos.Items.Add(alumno);
                                } while (true);
                            }
                        }
                    }
                }
                catch (IOException)
                {
                    label2.Text = "Error";
                }
                sock.Close();
            }
        }
        private void setEnable(bool setEnable)
        {
            txbDNIUsuario.Enabled = setEnable;
            btnAdd.Enabled = setEnable;
            btnList.Enabled = setEnable;
            btnCambiarConexion.Text = setEnable ? "Cambiar conexión" : "Nueva conexión";
        }

        private void btnCambiarConexion_Click(object sender, EventArgs e)
        {
            FormConexion f = new FormConexion(ip, port);
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK && f.ip.ToString() == "0" && f.port != 0)
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
                Console.WriteLine("no sabe escribir");
            }
        }
    }
}