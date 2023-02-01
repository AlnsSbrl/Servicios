namespace Mensaje
{
    partial class Mensaje
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txbMensajeUsuario = new System.Windows.Forms.TextBox();
            this.lbNombreUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbMensajeUsuario
            // 
            this.txbMensajeUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbMensajeUsuario.Enabled = false;
            this.txbMensajeUsuario.Font = new System.Drawing.Font("MV Boli", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbMensajeUsuario.Location = new System.Drawing.Point(124, 244);
            this.txbMensajeUsuario.Multiline = true;
            this.txbMensajeUsuario.Name = "txbMensajeUsuario";
            this.txbMensajeUsuario.Size = new System.Drawing.Size(156, 48);
            this.txbMensajeUsuario.TabIndex = 2;
            // 
            // lbNombreUsuario
            // 
            this.lbNombreUsuario.AutoSize = true;
            this.lbNombreUsuario.Font = new System.Drawing.Font("Ink Free", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombreUsuario.Location = new System.Drawing.Point(121, 227);
            this.lbNombreUsuario.Name = "lbNombreUsuario";
            this.lbNombreUsuario.Size = new System.Drawing.Size(39, 14);
            this.lbNombreUsuario.TabIndex = 3;
            this.lbNombreUsuario.Text = "label1";
            // 
            // MensajeUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbNombreUsuario);
            this.Controls.Add(this.txbMensajeUsuario);
            this.Name = "MensajeUsuario";
            this.Size = new System.Drawing.Size(800, 450);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txbMensajeUsuario;
        private System.Windows.Forms.Label lbNombreUsuario;
    }
}
