namespace SetulCantor
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
            this.trackBarIteratii = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIteratii)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarIteratii
            // 
            this.trackBarIteratii.Location = new System.Drawing.Point(594, 373);
            this.trackBarIteratii.Name = "trackBarIteratii";
            this.trackBarIteratii.Size = new System.Drawing.Size(104, 56);
            this.trackBarIteratii.TabIndex = 0;
            this.trackBarIteratii.Scroll += new System.EventHandler(this.trackBarIteratii_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 54);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 467);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBarIteratii);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIteratii)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarIteratii;
        private System.Windows.Forms.Button button1;
    }
}

