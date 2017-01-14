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
            this.tbRameno1 = new System.Windows.Forms.TextBox();
            this.tbRameno2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
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
            this.textBox1.Text = "Drag and Drop file here...";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
            // 
            // btnSpracujKod
            // 
            this.btnSpracujKod.Location = new System.Drawing.Point(274, 345);
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
            // tbRameno1
            // 
            this.tbRameno1.Location = new System.Drawing.Point(164, 302);
            this.tbRameno1.Name = "tbRameno1";
            this.tbRameno1.Size = new System.Drawing.Size(57, 20);
            this.tbRameno1.TabIndex = 5;
            // 
            // tbRameno2
            // 
            this.tbRameno2.Location = new System.Drawing.Point(164, 366);
            this.tbRameno2.Name = "tbRameno2";
            this.tbRameno2.Size = new System.Drawing.Size(57, 20);
            this.tbRameno2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Dlzka ramena 1 (cm)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Dlzka ramena 2 (cm)";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(414, 396);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 430);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRameno2);
            this.Controls.Add(this.tbRameno1);
            this.Controls.Add(this.listBoxZoznamPortov);
            this.Controls.Add(this.textBoxVypisUhly);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSpracujKod);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.TextBox tbRameno1;
        private System.Windows.Forms.TextBox tbRameno2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReset;
    }
}

