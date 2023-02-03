namespace ClienteChat
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
            this.btnSala0 = new System.Windows.Forms.Button();
            this.btnSala1 = new System.Windows.Forms.Button();
            this.btnSala2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSala0
            // 
            this.btnSala0.Location = new System.Drawing.Point(53, 125);
            this.btnSala0.Name = "btnSala0";
            this.btnSala0.Size = new System.Drawing.Size(75, 54);
            this.btnSala0.TabIndex = 0;
            this.btnSala0.Tag = "42069";
            this.btnSala0.Text = "Sala 0";
            this.btnSala0.UseVisualStyleBackColor = true;
            this.btnSala0.Click += new System.EventHandler(this.btnSala1_Click);
            // 
            // btnSala1
            // 
            this.btnSala1.Location = new System.Drawing.Point(134, 125);
            this.btnSala1.Name = "btnSala1";
            this.btnSala1.Size = new System.Drawing.Size(75, 54);
            this.btnSala1.TabIndex = 1;
            this.btnSala1.Tag = "63000";
            this.btnSala1.Text = "Sala 1";
            this.btnSala1.UseVisualStyleBackColor = true;
            this.btnSala1.Click += new System.EventHandler(this.btnSala1_Click);
            // 
            // btnSala2
            // 
            this.btnSala2.Location = new System.Drawing.Point(215, 125);
            this.btnSala2.Name = "btnSala2";
            this.btnSala2.Size = new System.Drawing.Size(75, 54);
            this.btnSala2.TabIndex = 2;
            this.btnSala2.Tag = "64000";
            this.btnSala2.Text = "Sala 2";
            this.btnSala2.UseVisualStyleBackColor = true;
            this.btnSala2.Click += new System.EventHandler(this.btnSala1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(167, 84);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre usuario";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 226);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSala2);
            this.Controls.Add(this.btnSala1);
            this.Controls.Add(this.btnSala0);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSala0;
        private Button btnSala1;
        private Button btnSala2;
        private TextBox textBox1;
        private Label label1;
    }
}