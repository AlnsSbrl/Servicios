namespace ClienteServidorPorTurnos
{
    partial class FormConexion
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
            this.txbAntiguaIP = new System.Windows.Forms.TextBox();
            this.txbAntiguoPuerto = new System.Windows.Forms.TextBox();
            this.txbNuevoPuerto = new System.Windows.Forms.TextBox();
            this.txbNuevaIP = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbAntiguaIP
            // 
            this.txbAntiguaIP.Enabled = false;
            this.txbAntiguaIP.Location = new System.Drawing.Point(177, 90);
            this.txbAntiguaIP.Name = "txbAntiguaIP";
            this.txbAntiguaIP.Size = new System.Drawing.Size(100, 23);
            this.txbAntiguaIP.TabIndex = 0;
            // 
            // txbAntiguoPuerto
            // 
            this.txbAntiguoPuerto.Enabled = false;
            this.txbAntiguoPuerto.Location = new System.Drawing.Point(177, 139);
            this.txbAntiguoPuerto.Name = "txbAntiguoPuerto";
            this.txbAntiguoPuerto.Size = new System.Drawing.Size(100, 23);
            this.txbAntiguoPuerto.TabIndex = 1;
            // 
            // txbNuevoPuerto
            // 
            this.txbNuevoPuerto.Location = new System.Drawing.Point(71, 139);
            this.txbNuevoPuerto.Name = "txbNuevoPuerto";
            this.txbNuevoPuerto.Size = new System.Drawing.Size(100, 23);
            this.txbNuevoPuerto.TabIndex = 3;
            this.txbNuevoPuerto.TextChanged += new System.EventHandler(this.txbNuevaIP_TextChanged);
            // 
            // txbNuevaIP
            // 
            this.txbNuevaIP.Location = new System.Drawing.Point(71, 90);
            this.txbNuevaIP.Name = "txbNuevaIP";
            this.txbNuevaIP.Size = new System.Drawing.Size(100, 23);
            this.txbNuevaIP.TabIndex = 2;
            this.txbNuevaIP.TextChanged += new System.EventHandler(this.txbNuevaIP_TextChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(187, 209);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(84, 209);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nuevo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Antiguo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Port";
            // 
            // FormConexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 265);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txbNuevoPuerto);
            this.Controls.Add(this.txbNuevaIP);
            this.Controls.Add(this.txbAntiguoPuerto);
            this.Controls.Add(this.txbAntiguaIP);
            this.Name = "FormConexion";
            this.Text = "FormConexion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConexion_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txbAntiguaIP;
        private TextBox txbAntiguoPuerto;
        private TextBox txbNuevoPuerto;
        private TextBox txbNuevaIP;
        private Button btnGuardar;
        private Button btnCancelar;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}