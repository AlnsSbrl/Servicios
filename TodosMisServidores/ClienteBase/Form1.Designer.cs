namespace ClienteBase
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
            this.btnTime = new System.Windows.Forms.Button();
            this.btnDateSan = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTime
            // 
            this.btnTime.Location = new System.Drawing.Point(94, 145);
            this.btnTime.Name = "btnTime";
            this.btnTime.Size = new System.Drawing.Size(91, 23);
            this.btnTime.TabIndex = 0;
            this.btnTime.Tag = "time";
            this.btnTime.Text = "Mostrar hora";
            this.btnTime.UseVisualStyleBackColor = true;
            this.btnTime.Click += new System.EventHandler(this.commandButtonClick);
            // 
            // btnDateSan
            // 
            this.btnDateSan.Location = new System.Drawing.Point(191, 145);
            this.btnDateSan.Name = "btnDateSan";
            this.btnDateSan.Size = new System.Drawing.Size(91, 23);
            this.btnDateSan.TabIndex = 1;
            this.btnDateSan.Tag = "date";
            this.btnDateSan.Text = "Mostrar fecha";
            this.btnDateSan.UseVisualStyleBackColor = true;
            this.btnDateSan.Click += new System.EventHandler(this.commandButtonClick);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(94, 194);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(91, 23);
            this.btnAll.TabIndex = 2;
            this.btnAll.Tag = "all";
            this.btnAll.Text = "Mostrar todo";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.commandButtonClick);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(166, 98);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(116, 23);
            this.txtPassword.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Password";
            // 
            // lblResult
            // 
            this.lblResult.AutoEllipsis = true;
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(94, 231);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(59, 15);
            this.lblResult.TabIndex = 6;
            this.lblResult.Text = "Resultado";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(191, 194);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Tag = "close";
            this.btnClose.Text = "Cerrar servicio";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.commandButtonClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 47);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cambiar conexion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.changeConnectionClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 285);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnDateSan);
            this.Controls.Add(this.btnTime);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnTime;
        private Button btnDateSan;
        private Button btnAll;
        private TextBox txtPassword;
        private Label label1;
        private Label lblResult;
        private Button btnClose;
        private Button button1;
    }
}