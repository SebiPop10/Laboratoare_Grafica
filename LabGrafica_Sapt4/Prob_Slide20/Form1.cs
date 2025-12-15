using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prob_Slide20
{
    public partial class Form1 : Form
    {
        private PointF[] squarePoints;
        private float alpha = 15f; // unghiul de forfecare (grade)
        public Form1()
        {
            InitializeComponent();
            squarePoints = new PointF[]
           {
                new PointF(100, 100),
                new PointF(200, 100),
                new PointF(200, 200),
                new PointF(100, 200)
           };

            // Setări pentru NumericUpDown
            numAngle.Minimum = 0;
            numAngle.Maximum = 89;
            numAngle.Increment = 1;
            numAngle.Value = 15;
            numAngle.DecimalPlaces = 0;
            numAngle.ValueChanged += numAngle_ValueChanged;

            pictureBox1.Paint += pictureBox1_Paint;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Conversie în radiani
            float radians = (float)(Math.PI * alpha / 180.0);
            PointF[] shearedSquare = ShearPoints(squarePoints, radians);

            // Desenăm pătratul original (albastru)
            using (var brushOrig = new SolidBrush(Color.FromArgb(120, Color.SteelBlue)))
            using (var penOrig = new Pen(Color.Blue, 2))
            {
                g.FillPolygon(brushOrig, squarePoints);
                g.DrawPolygon(penOrig, squarePoints);
            }

            // Desenăm pătratul forfecat (roșu)
            using (var brushShear = new SolidBrush(Color.FromArgb(120, Color.IndianRed)))
            using (var penShear = new Pen(Color.Red, 2))
            {
                g.FillPolygon(brushShear, shearedSquare);
                g.DrawPolygon(penShear, shearedSquare);
            }

            // Desenăm vârfurile
            DrawVertices(g, squarePoints, Color.Blue);
            DrawVertices(g, shearedSquare, Color.Red);

            // Afișăm valoarea curentă a unghiului
            g.DrawString($"α = {alpha}°", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 10, 10);
        }

        private void numAngle_ValueChanged(object sender, EventArgs e)
        {
            alpha = (float)numAngle.Value;
            pictureBox1.Invalidate(); // redesenează la schimbarea unghiului
        }

        private PointF[] ShearPoints(PointF[] points, float alphaRad)
        {
            PointF[] result = new PointF[points.Length];
            float tanA = (float)Math.Tan(alphaRad);

            for (int i = 0; i < points.Length; i++)
            {
                float x = points[i].X;
                float y = points[i].Y;

                float xNew = x + y * tanA;
                float yNew = y;

                result[i] = new PointF(xNew, yNew);
            }

            return result;
        }

        private void DrawVertices(Graphics g, PointF[] pts, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                foreach (var p in pts)
                {
                    int r = 4;
                    g.FillEllipse(brush, p.X - r, p.Y - r, r * 2, r * 2);
                }
            }
        }
    }
}
