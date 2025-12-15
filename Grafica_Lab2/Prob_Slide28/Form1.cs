using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prob_Slide28
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem == null)
            {
                MessageBox.Show("Selectează o funcție din listă!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string functie = checkedListBox1.SelectedItem.ToString();

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                // Desenăm axele X și Y
                Pen axa = new Pen(Color.LightGray, 1);
                int cx = pictureBox1.Width / 2;
                int cy = pictureBox1.Height / 2;
                g.DrawLine(axa, 0, cy, pictureBox1.Width, cy); // axa X
                g.DrawLine(axa, cx, 0, cx, pictureBox1.Height); // axa Y

                // Stil pentru grafic
                Pen grafic = new Pen(Color.Red, 2);

                // Intervalul de desenare
                double xMin = -10;
                double xMax = 10;
                double pas = 0.1;

                // Scalare (pixeli / unitate)
                double scaleX = 25; // 1 unitate = 25 pixeli
                double scaleY = 25;

                PointF? punctAnterior = null;

                for (double x = xMin; x <= xMax; x += pas)
                {
                    double y = CalculeazaY(functie, x);

                    // Convertim în coordonate pixel (cu originea în centru)
                    float xp = (float)(cx + x * scaleX);
                    float yp = (float)(cy - y * scaleY);

                    if (punctAnterior != null)
                        g.DrawLine(grafic, punctAnterior.Value, new PointF(xp, yp));

                    punctAnterior = new PointF(xp, yp);
                }
            }

            pictureBox1.Image = bmp;
        }

        private double CalculeazaY(string functie, double x)
        {
            switch (functie)
            {
                case "y=x": return x;
                case "y=x^2": return x * x;
                case "y=sin(x)": return Math.Sin(x);
                case "y=cos(x)": return Math.Cos(x);
                case "y = |x|": return Math.Abs(x);
                case "y = e^x": return Math.Exp(x);
                default: return 0;
            }
        }
    }
}
