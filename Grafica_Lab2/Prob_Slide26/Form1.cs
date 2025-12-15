using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Prob_Slide26
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                int x1 = int.Parse(textBox1.Text);
                int y1 = int.Parse(textBox2.Text);
                int x2 = int.Parse(textBox3.Text);
                int y2 = int.Parse(textBox4.Text);

               
                double panta = (double)(y2 - y1) / (x2 - x1);
                double lungime = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

               
                label5.Text = $"Panta: {panta:F2}";
                label6.Text = $"Lungime: {lungime:F2}";

                // Desenare cu alg DDA
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);

                    // Axele de referință (opțional)
                    Pen axa = new Pen(Color.LightGray, 1);
                    g.DrawLine(axa, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2);
                    g.DrawLine(axa, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height);

                    // Offset pentru a muta originea în centrul PictureBox-ului
                    int offsetX = pictureBox1.Width / 2;
                    int offsetY = pictureBox1.Height / 2;

                    // Apelăm funcția DDA pentru a desena linia
                    DDA_Line(bmp, offsetX + x1, offsetY - y1, offsetX + x2, offsetY - y2, Color.Red);
                }

                pictureBox1.Image = bmp;
            }
            catch (Exception)
            {
                MessageBox.Show("Introdu doar valori numerice valide!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DDA_Line(Bitmap bmp, double x1, double y1, double x2, double y2, Color culoare)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;

            double steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            double xIncrement = dx / steps;
            double yIncrement = dy / steps;

            double x = x1;
            double y = y1;

            for (int i = 0; i <= steps; i++)
            {
                // Verificăm limitele pentru a evita erorile
                if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                    bmp.SetPixel((int)Math.Round(x), (int)Math.Round(y), culoare);

                x += xIncrement;
                y += yIncrement;
            }

            // Desenare puncte de start și final 
            int px1 = (int)Math.Round(x1);
            int py1 = (int)Math.Round(y1);
            int px2 = (int)Math.Round(x2);
            int py2 = (int)Math.Round(y2);

            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (px1 + i >= 0 && px1 + i < bmp.Width && py1 + j >= 0 && py1 + j < bmp.Height)
                        bmp.SetPixel(px1 + i, py1 + j, Color.Blue);
                    if (px2 + i >= 0 && px2 + i < bmp.Width && py2 + j >= 0 && py2 + j < bmp.Height)
                        bmp.SetPixel(px2 + i, py2 + j, Color.Blue);
                }
            }
        }
    }
}
