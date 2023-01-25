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
            this.label2 = new System.Windows.Forms.Label();
            this.listAlumnos = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(25, 62);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Tag = "Add";
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnList
            // 
            this.btnList.Enabled = false;
            this.btnList.Location = new System.Drawing.Point(106, 62);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 23);
            this.btnList.TabIndex = 1;
            this.btnList.Tag = "list";
            this.btnList.Text = "List";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCambiarConexion
            // 
            this.btnCambiarConexion.Location = new System.Drawing.Point(296, 28);
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
            this.txbDNIUsuario.Location = new System.Drawing.Point(81, 31);
            this.txbDNIUsuario.Name = "txbDNIUsuario";
            this.txbDNIUsuario.Size = new System.Drawing.Size(100, 23);
            this.txbDNIUsuario.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // listAlumnos
            // 
            this.listAlumnos.FormattingEnabled = true;
            this.listAlumnos.ItemHeight = 15;
            this.listAlumnos.Location = new System.Drawing.Point(25, 154);
            this.listAlumnos.Name = "listAlumnos";
            this.listAlumnos.Size = new System.Drawing.Size(365, 169);
            this.listAlumnos.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 376);
            this.Controls.Add(this.listAlumnos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbDNIUsuario);
            this.Controls.Add(this.btnCambiarConexion);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnAdd);
            this.Name = "Form1";
            this.Text = "Lista de la compra";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
        private Label label2;
        private ListBox listAlumnos;
    }
}