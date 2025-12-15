using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanLine_Simplificat
{
    public partial class Form1 : Form
    {
        private List<Point> points = new List<Point>
        {
            new Point(100, 150),
            new Point(200, 50),
            new Point(300, 150),
            new Point(250, 250),
            new Point(150, 250),
            new Point(50, 300),
        };


        public Form1()
        {
            InitializeComponent();
            this.Text = "Scan-Line Simplificat";
            this.Width = 800;
            this.Height = 600;
            this.Paint += DeseneazaPoligonUmplut;
        }

        

        private void DeseneazaPoligonUmplut(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = Brushes.LightBlue;

            int ymin=int.MaxValue, ymax=int.MinValue;
            foreach (var p in points)
            {
                ymin= Math.Min(ymin, p.Y);
                ymax= Math.Max(ymax, p.Y);
            }

            for(int y=ymin; y<=ymax; y++)
            {
                List<int> intersections = new List<int>();
                for(int i=0; i<points.Count; i++)
                {
                    Point p1 = points[i];
                    Point p2 = points[(i + 1) % points.Count];
                    if((p1.Y <= y && p2.Y > y) || (p2.Y <= y && p1.Y > y))
                    {
                        float x = p1.X + (float)(y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
                        intersections.Add((int)x);
                    }
                }
                intersections.Sort();
                for(int i=0; i<intersections.Count; i+=2)
                {
                    //if(i + 1 < intersections.Count)
                    //{
                    //    g.FillRectangle(brush, intersections[i], y, intersections[i + 1] - intersections[i], 1);
                    //}
                    g.DrawLine(Pens.Black, intersections[i], y, intersections[i + 1], y);
                }
            }
            g.DrawPolygon(Pens.Black, points.ToArray());
        }
    }
}
