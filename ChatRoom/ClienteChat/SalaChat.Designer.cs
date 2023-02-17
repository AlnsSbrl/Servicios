namespace ClienteChat
{
    partial class SalaChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaChat));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxMensajes = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnList = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txbListaUsuarios = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBoxMensajes);
            this.panel1.Location = new System.Drawing.Point(24, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 339);
            this.panel1.TabIndex = 0;
            // 
            // listBoxMensajes
            // 
            this.listBoxMensajes.FormattingEnabled = true;
            this.listBoxMensajes.ItemHeight = 15;
            this.listBoxMensajes.Location = new System.Drawing.Point(0, 14);
            this.listBoxMensajes.Name = "listBoxMensajes";
            this.listBoxMensajes.Size = new System.Drawing.Size(290, 319);
            this.listBoxMensajes.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 398);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(264, 23);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(383, 276);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 48);
            this.btnList.TabIndex = 2;
            this.btnList.Tag = "#lista";
            this.btnList.Text = "Personas en la sala";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(383, 346);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 46);
            this.btnExit.TabIndex = 3;
            this.btnExit.Tag = "#exit";
            this.btnExit.Text = "Salir del grupo";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(294, 398);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txbListaUsuarios
            // 
            this.txbListaUsuarios.Enabled = false;
            this.txbListaUsuarios.Location = new System.Drawing.Point(340, 67);
            this.txbListaUsuarios.Multiline = true;
            this.txbListaUsuarios.Name = "txbListaUsuarios";
            this.txbListaUsuarios.Size = new System.Drawing.Size(160, 179);
            this.txbListaUsuarios.TabIndex = 5;
            this.txbListaUsuarios.Visible = false;
            // 
            // SalaChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 450);
            this.Controls.Add(this.txbListaUsuarios);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Name = "SalaChat";
            this.Text = "SalaChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalaChat_FormClosing);
            this.Load += new System.EventHandler(this.SalaChat_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private TextBox textBox1;
        private Button btnList;
        private Button btnExit;
        private ListBox listBoxMensajes;
        private PictureBox pictureBox1;
        private TextBox txbListaUsuarios;
    }
}