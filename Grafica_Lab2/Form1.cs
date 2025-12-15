using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafica_Lab2
{
    public partial class Form1 : Form
    {
        Bitmap canvas;
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            //this.Text = "Puncte";
            this.Width = 800;
            this.Height = 600;
            this.Paint += new PaintEventHandler(Form1_Paint);

            //Prob1-desenare 5 puncte pe bitmap
            //canvas = new Bitmap(this.Width, this.Height);
            //canvas.SetPixel(100, 80, Color.Red);
            //canvas.SetPixel(160, 200, Color.Blue);
            //canvas.SetPixel(400, 300, Color.Green);
            //canvas.SetPixel(200, 400, Color.Black);
            //canvas.SetPixel(300, 100, Color.Yellow);

            //Prob2-sa se traseze dreapta pt care se cunosc coordonatele a 2 pct P1 si P2 prin care trece
            //canvas = new Bitmap(this.Width, this.Height);
            //PointF P1= new PointF(100, 80);
            //PointF P2 = new PointF(160, 200);
            //int x;
            //float m, y;
            //m=(P2.Y - P1.Y) / (P2.X - P1.X);
            //for(x=(int)P1.X; x<=P2.X; x++)
            //{
            //    y = P1.Y + m * (x - P1.X);
            //    canvas.SetPixel(x, (int)y, Color.Red);
            //}

            //DDA
            //canvas = new Bitmap(this.Width, this.Height);
            //PointF P1 = new PointF(100, 80);
            //PointF P2 = new PointF(160, 200);
            //int dx, dy, steps, k;
            //dx = (int)(P2.X - P1.X);
            //dy = (int)(P2.Y - P1.Y);
            //steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            //float xIncrement = dx / (float)steps;
            //float yIncrement = dy / (float)steps;
            //float x = P1.X;
            //float y = P1.Y;
            //for(k = 0; k <= steps; k++)
            //{
            //    canvas.SetPixel((int)x, (int)y, Color.Red);
            //    x += xIncrement;
            //    y += yIncrement;
            //}

            //Bresenham-varianta 1
            //canvas = new Bitmap(this.Width, this.Height);
            //Point P1 = new Point(100, 80);
            //Point P2 = new Point(160, 200);
            //var 1(copilot)
            //int dx, dy, sx, sy, err, e2;
            //dx = Math.Abs(P2.X - P1.X);
            //dy = Math.Abs(P2.Y - P1.Y);
            //sx = (P1.X < P2.X) ? 1 : -1;
            //sy = (P1.Y < P2.Y) ? 1 : -1;
            //err = dx - dy;
            //while (true)
            //{
            //    canvas.SetPixel(P1.X, P1.Y, Color.Red);
            //    if (P1.X == P2.X && P1.Y == P2.Y) break;
            //    e2 = 2 * err;
            //    if (e2 > -dy)
            //    {
            //        err -= dy;
            //        P1.X += sx;
            //    }
            //    if (e2 < dx)
            //    {
            //        err += dx;
            //        P1.Y += sy;
            //    }
            //}


            //var 2(pp)
            //int dx = P2.X - P1.X;
            //int dy = P2.Y - P1.Y;
            //int p = 2 * dy - dx;
            //int x = P1.X, y = P1.Y;
            //canvas.SetPixel(P1.X, P1.Y, Color.Red);
            //do
            //{
            //    if (p < 0)
            //    {
            //        x++;
            //        p = p + 2 * dy;
            //    }
            //    if (p >= 0)
            //    {
            //        x++;
            //        y++;
            //        p = p + 2 * dy - 2 * dx;
            //    }
            //    canvas.SetPixel(x, y, Color.Red);
            //} while (x < P2.X);

            //aplic care deseneaza 3 pct pe ecran(pozitii diferite).
            //a) Leaga aceste puncte cu linii pt a forma un triunghi.
            //b) Foloseste pentru fiecare linie culori si grosime diferite

            //cu DrawLine
            //canvas = new Bitmap(this.Width, this.Height);
            //g = Graphics.FromImage(canvas);
            //PointF P1 = new Point(100, 80);
            //PointF P2 = new Point(160, 200);
            //PointF P3 = new Point(400, 300);
            //g.DrawLine(Pens.Red, P1.X, P1.Y, P2.X, P2.Y);
            //g.DrawLine(Pens.Blue, P2, P3);
            //g.DrawLine(Pens.Black, P1, P3);

            //this.Paint += new PaintEventHandler(Form1_Paint);

            //fara DrawLine
            canvas = new Bitmap(this.Width, this.Height);
            PointF P1 = new PointF(100, 80);
            PointF P2 = new PointF(160, 200);
            PointF P3 = new PointF(400, 300);

            DrawLine(new Point((int)P1.X, (int)P1.Y), new Point((int)P2.X, (int)P2.Y), Color.Red);
            DrawLine(new Point((int)P2.X, (int)P2.Y), new Point((int)P3.X, (int)P3.Y), Color.Blue);
            DrawLine(new Point((int)P1.X, (int)P1.Y), new Point((int)P3.X, (int)P3.Y), Color.Black);


        }

        private void DrawLine(Point p1, Point p2, Color color)
        {
            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);
            int sx = (p1.X < p2.X) ? 1 : -1;
            int sy = (p1.Y < p2.Y) ? 1 : -1;
            int err = dx - dy;

            int x = p1.X, y = p1.Y;

            while (true)
            {
                canvas.SetPixel(x, y, color);
                if (x == p2.X && y == p2.Y) break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0);
        }
    }
}
