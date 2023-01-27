using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteServidorPorTurnos
{
    public partial class FormConexion : Form
    {
        public IPAddress? ip;
        public int? port;
        int numParser;
        IPAddress ipParser;
        public FormConexion(string ip, int puerto)
        {
            InitializeComponent();
            txbAntiguaIP.Text =ip.ToString();
            txbAntiguoPuerto.Text = puerto.ToString();
            this.ip=IPAddress.Parse(ip);          
            port = puerto;
        }

        private void txbNuevaIP_TextChanged(object sender, EventArgs e)
        {
            if ((TextBox)sender == txbNuevaIP)
            {
                txbNuevaIP.BackColor = (IPAddress.TryParse(txbNuevaIP.Text, out ipParser)||txbNuevaIP.Text=="") ? Color.White : Color.Red;
            }
            if ((TextBox)sender == txbNuevoPuerto)
            {
                bool pruebaAdiccional;
                txbNuevoPuerto.BackColor = (pruebaAdiccional = int.TryParse(txbNuevoPuerto.Text, out numParser)||txbNuevoPuerto.Text=="") ? Color.White : Color.Red;
                if (pruebaAdiccional)
                {
                    txbNuevoPuerto.BackColor = (numParser < IPEndPoint.MaxPort && numParser > IPEndPoint.MinPort) || txbNuevoPuerto.Text == "" ? Color.White : Color.Red;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txbNuevoPuerto.BackColor == Color.White && txbNuevoPuerto.Text != "")
            {
                port = numParser;
            }
            if (txbNuevaIP.BackColor == Color.White && txbNuevaIP.Text != "")
            {
                ip = ipParser;
            }
            if ((Button)sender == btnGuardar)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }

        }
    }
}
