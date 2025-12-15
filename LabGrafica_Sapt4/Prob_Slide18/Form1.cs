using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prob_Slide18
{
    public partial class Form1 : Form
    {

        private PointF[] originalTriangle;
        public Form1()
        {
            InitializeComponent();

            // Triunghiul original (în coordonate simple)
            originalTriangle = new PointF[]
            {
                new PointF(50, 50),
                new PointF(150, 80),
                new PointF(100, 150)
            };

            pictureBox1.Paint += pictureBox1_Paint;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Reflexia față de axa y = 1
            PointF[] reflectedTriangle = ReflectPointsY(originalTriangle, 1);

            // Desenează triunghiul original (albastru)
            using (var brushOrig = new SolidBrush(Color.FromArgb(120, Color.SteelBlue)))
            using (var penOrig = new Pen(Color.Blue, 2))
            {
                g.FillPolygon(brushOrig, originalTriangle);
                g.DrawPolygon(penOrig, originalTriangle);
            }

            // Desenează triunghiul reflectat (roșu)
            using (var brushRef = new SolidBrush(Color.FromArgb(120, Color.IndianRed)))
            using (var penRef = new Pen(Color.Red, 2))
            {
                g.FillPolygon(brushRef, reflectedTriangle);
                g.DrawPolygon(penRef, reflectedTriangle);
            }

            // Desenează axa y=1 (linia de reflexie)
            using (var penAxis = new Pen(Color.DarkGreen, 2))
            {
                g.DrawLine(penAxis, 1, 0, 1, pictureBox1.Height); // axa y=1
            }

            // Desenează punctele pentru claritate
            DrawVertices(g, originalTriangle, Color.Blue);
            DrawVertices(g, reflectedTriangle, Color.Red);
        }

        /// <summary>
        /// Reflectă fiecare punct față de linia orizontală y = yLine (ex: y = 1)
        /// </summary>
        private PointF[] ReflectPointsY(PointF[] points, float yLine)
        {
            PointF[] result = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                float x = points[i].X;
                float y = points[i].Y;

                // Reflexie față de linia orizontală y = yLine
                float xNew = x;
                float yNew = 2 * yLine - y;

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
