namespace FiestaVersionsGrapper
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.BinDialog = new System.Windows.Forms.OpenFileDialog();
            this.ClientDateBox = new System.Windows.Forms.TextBox();
            this.HashBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(411, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BinDialog
            // 
            this.BinDialog.FileName = "Fiesta.bin";
            this.BinDialog.Filter = "All Files (*bin.*)|*.bin";
            // 
            // ClientDateBox
            // 
            this.ClientDateBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientDateBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientDateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.ClientDateBox.Location = new System.Drawing.Point(124, 22);
            this.ClientDateBox.Name = "ClientDateBox";
            this.ClientDateBox.ReadOnly = true;
            this.ClientDateBox.Size = new System.Drawing.Size(268, 19);
            this.ClientDateBox.TabIndex = 1;
            // 
            // HashBox
            // 
            this.HashBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.HashBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HashBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.HashBox.Location = new System.Drawing.Point(124, 76);
            this.HashBox.Name = "HashBox";
            this.HashBox.ReadOnly = true;
            this.HashBox.Size = new System.Drawing.Size(349, 19);
            this.HashBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "ClientDate :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(16, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "ClientHash :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 130);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HashBox);
            this.Controls.Add(this.ClientDateBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "FiestaVersionsGrapper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog BinDialog;
        private System.Windows.Forms.TextBox ClientDateBox;
        private System.Windows.Forms.TextBox HashBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

