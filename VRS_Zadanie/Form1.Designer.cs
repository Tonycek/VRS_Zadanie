namespace VRS_Zadanie
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSpracujKod = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxVypisUhly = new System.Windows.Forms.TextBox();
            this.listBoxZoznamPortov = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(230, 264);
            this.textBox1.TabIndex = 0;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // btnSpracujKod
            // 
            this.btnSpracujKod.Location = new System.Drawing.Point(144, 341);
            this.btnSpracujKod.Name = "btnSpracujKod";
            this.btnSpracujKod.Size = new System.Drawing.Size(99, 23);
            this.btnSpracujKod.TabIndex = 1;
            this.btnSpracujKod.Text = "Spracuj kod";
            this.btnSpracujKod.UseVisualStyleBackColor = true;
            this.btnSpracujKod.Click += new System.EventHandler(this.btnSpracujKod_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(564, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(244, 265);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxVypisUhly
            // 
            this.textBoxVypisUhly.Location = new System.Drawing.Point(249, 12);
            this.textBoxVypisUhly.Multiline = true;
            this.textBoxVypisUhly.Name = "textBoxVypisUhly";
            this.textBoxVypisUhly.Size = new System.Drawing.Size(235, 265);
            this.textBoxVypisUhly.TabIndex = 3;
            // 
            // listBoxZoznamPortov
            // 
            this.listBoxZoznamPortov.FormattingEnabled = true;
            this.listBoxZoznamPortov.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.listBoxZoznamPortov.Location = new System.Drawing.Point(12, 285);
            this.listBoxZoznamPortov.Name = "listBoxZoznamPortov";
            this.listBoxZoznamPortov.Size = new System.Drawing.Size(120, 134);
            this.listBoxZoznamPortov.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 430);
            this.Controls.Add(this.listBoxZoznamPortov);
            this.Controls.Add(this.textBoxVypisUhly);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSpracujKod);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSpracujKod;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxVypisUhly;
        private System.Windows.Forms.ListBox listBoxZoznamPortov;
    }
}

