using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prob_Slide15
{
    public partial class Form1 : Form
    {
        private PointF[] rectanglePoints;  // Dreptunghiul inițial
        private float angle = 0;           // Unghiul curent (în radiani)
        public Form1()
        {
            InitializeComponent();
            // Setează evenimente
            pictureBox1.Paint += pictureBox1_Paint;
            numAngle.Minimum = 0;
            numAngle.Maximum = 360;
            numAngle.Increment = 10;
            numAngle.ValueChanged += numAngle_ValueChanged;

            // Dreptunghi de bază (100x60)
            rectanglePoints = new PointF[]
            {
                new PointF(50, 50),
                new PointF(150, 50),
                new PointF(150, 110),
                new PointF(50, 110)
            };
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dreptunghiul rotit cu unghiul α
            PointF[] rotatedRectangle = RotatePoints(rectanglePoints, angle);

            // Desenează dreptunghiul original (albastru)
            using (var brushOrig = new SolidBrush(Color.FromArgb(120, Color.SteelBlue)))
            using (var penOrig = new Pen(Color.Blue, 2))
            {
                g.FillPolygon(brushOrig, rectanglePoints);
                g.DrawPolygon(penOrig, rectanglePoints);
            }

            // Desenează dreptunghiul rotit (roșu)
            using (var brushRot = new SolidBrush(Color.FromArgb(120, Color.IndianRed)))
            using (var penRot = new Pen(Color.Red, 2))
            {
                g.FillPolygon(brushRot, rotatedRectangle);
                g.DrawPolygon(penRot, rotatedRectangle);
            }

            // Desenează axele pentru claritate
            DrawAxes(g);
        }

        private void numAngle_ValueChanged(object sender, EventArgs e)
        {
            angle = (float)(Math.PI * (double)numAngle.Value / 180.0); // conversie grade → radiani
            pictureBox1.Invalidate(); // redesenare
        }

        private PointF[] RotatePoints(PointF[] points, float alpha)
        {
            PointF[] result = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                float x = points[i].X;
                float y = points[i].Y;
                float xNew = (float)(x * Math.Cos(alpha) - y * Math.Sin(alpha));
                float yNew = (float)(x * Math.Sin(alpha) + y * Math.Cos(alpha));
                result[i] = new PointF(xNew, yNew);
            }
            return result;
        }

        private void DrawAxes(Graphics g)
        {
            using (var pen = new Pen(Color.Gray, 1))
            {
                g.DrawLine(pen, 0, pictureBox1.Height / 2, pictureBox1.Width, pictureBox1.Height / 2); // axa X
                g.DrawLine(pen, pictureBox1.Width / 2, 0, pictureBox1.Width / 2, pictureBox1.Height); // axa Y
            }
        }
    }
}
