using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteAdivino
{
    public partial class FormJuego : Form
    {
        string guessWord;
        List<char> alreadyInputedChars;
        public FormJuego(string palabra)
        {
            InitializeComponent();
            string[] datos = palabra.Split('$');
            guessWord = datos[0];

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
                lb.Location=new Point(i*20,50);
                lb.Size = new Size(20, 20);
                this.Controls.Add(lb);
            }
        }
    }
}
