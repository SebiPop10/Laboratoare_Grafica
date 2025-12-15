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

namespace LabGrafica_Sapt3
{
    public partial class Form1 : Form
    {
        Bitmap canvas;
        int radius;
        Graphics g;
        private Timer timer;
        private int state = 0;

        public Form1()
        {
            InitializeComponent();
            canvas=new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(canvas);
            timer= new Timer();
            timer.Tick += timer1_Tick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DrawCirclePolar1(g, 300, 50,radius, Pens.Gray);
            //DrawCirclePolar2(g, 300, 150, radius, Pens.Gray);
            //DrawCirclePolar3(g, 300, 250, radius, Pens.Gray);
            state = 0;
            timer.Start();
            timer.Interval = 1000; // 1 second
            pictureBox1.Image = canvas;

        }
        //void DrawCirclePolar1(Graphics g, int x0, int y0, int radius, Pen pen)
        //{
        //    double theta = 0;
        //    double step = 0.01;
        //    int prevX = x0 + (int)(radius * Math.Cos(0));
        //    int prevY = y0 + (int)(radius * Math.Sin(0));
        //    while(theta<=2*Math.PI)
        //    {
        //        int x = x0 + (int)(radius * Math.Cos(theta));
        //        int y = y0 + (int)(radius * Math.Sin(theta));
        //        g.DrawLine(pen, prevX, prevY, x, y);
        //        prevX = x;
        //        prevY = y;
        //        theta += step;
        //    }
        //    //fill the circle with red color
        //    g.FillEllipse(Brushes.Red, x0 - radius, y0 - radius, radius * 2, radius * 2);
        //}
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            radius = int.Parse(textBox2.Text);
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            //g.Clear(Color.White);
            DrawTrafficLight(g, 300, 50, radius, state);
            pictureBox1.Image = canvas;
            state=(state+1)%3;
        }

        public void DrawTrafficLight(Graphics g, int x, int y, int r, int active)
        {
            g.DrawRectangle(Pens.Black, x - r - 10, y - 10, r * 2 + 20, r * 6 + 20);

            Color[] colors = { Color.Red, Color.Yellow, Color.Green };

            for (int i = 0; i < 3; i++)
            {
                Brush brush = (i == active) ? new SolidBrush(colors[i]) : Brushes.Gray;
                g.FillEllipse(brush, x - r, y + i * (2 * r + 10), r * 2, r * 2);
                g.DrawEllipse(Pens.Black, x - r, y + i * (2 * r + 10), r * 2, r * 2);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(textBox1.Text);
                int r = int.Parse(textBox2.Text);

                if (n < 3)
                {
                    MessageBox.Show("Numărul de laturi trebuie să fie cel puțin 3!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                int cx = pictureBox1.Width / 2;
                int cy = pictureBox1.Height / 2;

                // Desenare cerc cu algoritmul MidPoint
                DeseneazaCercMidPoint(bmp, cx, cy, r, Color.Gray);

                // Calculare și desenare poligon regulat
                DeseneazaPoligon(bmp, cx, cy, n, r, Color.Blue);

                pictureBox1.Image = bmp;
            }
            catch
            {
                MessageBox.Show("Introdu valori numerice valide pentru n și r!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Desenare cerc folosind algoritmul MidPoint 
        /// </summary>
        private void DeseneazaCercMidPoint(Bitmap bmp, int xc, int yc, int r, Color culoare)
        {
            int x = 0;
            int y = r;
            int d = 1 - r;

            while (x <= y)
            {
                Set8Pixeli(bmp, xc, yc, x, y, culoare);

                if (d < 0)
                    d += 2 * x + 3;
                else
                {
                    d += 2 * (x - y) + 5;
                    y--;
                }
                x++;
            }
        }

        /// <summary>
        /// Desenare a celor 8 puncte simetrice ale cercului.
        /// </summary>
        private void Set8Pixeli(Bitmap bmp, int xc, int yc, int x, int y, Color c)
        {
            if (InImagine(bmp, xc + x, yc + y)) bmp.SetPixel(xc + x, yc + y, c);
            if (InImagine(bmp, xc - x, yc + y)) bmp.SetPixel(xc - x, yc + y, c);
            if (InImagine(bmp, xc + x, yc - y)) bmp.SetPixel(xc + x, yc - y, c);
            if (InImagine(bmp, xc - x, yc - y)) bmp.SetPixel(xc - x, yc - y, c);
            if (InImagine(bmp, xc + y, yc + x)) bmp.SetPixel(xc + y, yc + x, c);
            if (InImagine(bmp, xc - y, yc + x)) bmp.SetPixel(xc - y, yc + x, c);
            if (InImagine(bmp, xc + y, yc - x)) bmp.SetPixel(xc + y, yc - x, c);
            if (InImagine(bmp, xc - y, yc - x)) bmp.SetPixel(xc - y, yc - x, c);
        }

        private bool InImagine(Bitmap bmp, int x, int y)
        {
            return x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height;
        }

        /// <summary>
        /// Desenare a unui poligon regulat cu n laturi înscris într-un cerc de rază r.
        /// </summary>
        private void DeseneazaPoligon(Bitmap bmp, int cx, int cy, int n, int r, Color culoare)
        {
            double unghiPas = 2 * Math.PI / n;
            PointF[] puncte = new PointF[n];

            
            for (int i = 0; i < n; i++)
            {
                double unghi = -Math.PI / 2 + i * unghiPas; 
                float x = (float)(cx + r * Math.Cos(unghi));
                float y = (float)(cy + r * Math.Sin(unghi));
                puncte[i] = new PointF(x, y);
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                Pen p = new Pen(culoare, 2);
                g.DrawPolygon(p, puncte);
            }
        }
    }
}
