using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7
{
    public partial class Form1 : Form
    {
        private Point[] ControlPoints= new Point[]
        {
            new Point(50, 300),
            new Point(150, 50),
            new Point(300, 400),
            new Point(500, 200)
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;

            // Draw control points
            foreach (var point in ControlPoints)
            {
                g.FillEllipse(Brushes.Red, point.X - 3, point.Y - 3, 6,6);
            }

            // Draw control polygon 
            g.DrawLines(Pens.Gray, ControlPoints);

            // Draw Bezier curve

            DrawBezier(g, ControlPoints);
        }

        private void DrawBezier(Graphics g, Point[] pts)
        {
            int steps = 100;
            PointF prev=BezierPoint(pts, 0);

            for (int i = 1; i <= steps; i++)
            {
                float t = (float)i / steps;
                PointF curr = BezierPoint(pts, t);
                g.DrawLine(Pens.Blue, prev, curr);
                prev = curr;
            }

        }

        private PointF BezierPoint(Point[] pts, float t)
        {
           float x= (float)(Math.Pow(1 - t, 3) * pts[0].X +
                            3 * Math.Pow(1 - t, 2) * t * pts[1].X +
                            3 * (1 - t) * Math.Pow(t, 2) * pts[2].X +
                            Math.Pow(t, 3) * pts[3].X);

              float y= (float)(Math.Pow(1 - t, 3) * pts[0].Y + 
                            3 * Math.Pow(1 - t, 2) * t * pts[1].Y +
                            3 * (1 - t) * Math.Pow(t, 2) * pts[2].Y +
                            Math.Pow(t, 3) * pts[3].Y);

            return new PointF(x, y);

        }
    }
}
