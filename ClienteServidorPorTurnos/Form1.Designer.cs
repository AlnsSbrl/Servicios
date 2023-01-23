namespace ClienteServidorPorTurnos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.btnCambiarConexion = new System.Windows.Forms.Button();
            this.txbDNIUsuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(57, 61);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnList
            // 
            this.btnList.Enabled = false;
            this.btnList.Location = new System.Drawing.Point(146, 61);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 23);
            this.btnList.TabIndex = 1;
            this.btnList.Text = "List";
            this.btnList.UseVisualStyleBackColor = true;
            // 
            // btnCambiarConexion
            // 
            this.btnCambiarConexion.Location = new System.Drawing.Point(266, 27);
            this.btnCambiarConexion.Name = "btnCambiarConexion";
            this.btnCambiarConexion.Size = new System.Drawing.Size(94, 57);
            this.btnCambiarConexion.TabIndex = 2;
            this.btnCambiarConexion.Text = "Cambiar conexión";
            this.btnCambiarConexion.UseVisualStyleBackColor = true;
            this.btnCambiarConexion.Click += new System.EventHandler(this.btnCambiarConexion_Click);
            // 
            // txbDNIUsuario
            // 
            this.txbDNIUsuario.Enabled = false;
            this.txbDNIUsuario.Location = new System.Drawing.Point(121, 27);
            this.txbDNIUsuario.Name = "txbDNIUsuario";
            this.txbDNIUsuario.Size = new System.Drawing.Size(100, 23);
            this.txbDNIUsuario.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Usuario";
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(57, 120);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(303, 229);
            this.listBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 376);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbDNIUsuario);
            this.Controls.Add(this.btnCambiarConexion);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnAdd;
        private Button btnList;
        private Button btnCambiarConexion;
        private TextBox txbDNIUsuario;
        private Label label1;
        private ListBox listBox1;
    }
}