using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prob_slide18_ClippingPeOImag
{
    public partial class Form1 : Form
    {

        // Coordonatele ferestrei de clipping
        Rectangle clipRect = new Rectangle(150, 100, 200, 150);

        // Coordonatele liniei inițiale
        PointF p1 = new PointF(100, 200);
        PointF p2 = new PointF(400, 150);

        Bitmap image;

        // Codurile regiunilor (Cohen–Sutherland)
        const int INSIDE = 0; // 0000
        const int LEFT = 1;   // 0001
        const int RIGHT = 2;  // 0010
        const int BOTTOM = 4; // 0100
        const int TOP = 8;    // 1000
        public Form1()
        {
            InitializeComponent();
            InitializeComponent();
            this.Text = "Clipping cu algoritmul Cohen–Sutherland";
            this.ClientSize = new Size(600, 400);

            // Încarcă o imagine de fundal
            image = new Bitmap(600, 400);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);
                g.FillEllipse(Brushes.LightBlue, 200, 120, 180, 120);
                g.DrawString("Imagine de fundal", new Font("Arial", 14), Brushes.DarkBlue, 200, 260);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(image, 0, 0);

            // Desenează fereastra de clipping (roșie)
            using (Pen pen = new Pen(Color.Red, 2))
                g.DrawRectangle(pen, clipRect);

            // Aplică algoritmul Cohen–Sutherland
            if (CohenSutherlandClip(ref p1, ref p2, clipRect))
            {
                // Linia tăiată (verde)
                using (Pen pen = new Pen(Color.Green, 3))
                    g.DrawLine(pen, p1, p2);
            }
            else
            {
                // Linia este complet în afara ferestrei
                using (Pen pen = new Pen(Color.Gray, 2))
                    g.DrawLine(pen, p1, p2);
            }
        }

        int ComputeCode(PointF p, Rectangle rect)
        {
            int code = INSIDE;

            if (p.X < rect.Left)
                code |= LEFT;
            else if (p.X > rect.Right)
                code |= RIGHT;
            if (p.Y < rect.Top)
                code |= TOP;
            else if (p.Y > rect.Bottom)
                code |= BOTTOM;

            return code;
        }

        bool CohenSutherlandClip(ref PointF p1, ref PointF p2, Rectangle rect)
        {
            int code1 = ComputeCode(p1, rect);
            int code2 = ComputeCode(p2, rect);
            bool accept = false;

            while (true)
            {
                if ((code1 | code2) == 0)
                {
                    // Total inside
                    accept = true;
                    break;
                }
                else if ((code1 & code2) != 0)
                {
                    // Total outside
                    break;
                }
                else
                {
                    // Parțial înăuntru
                    double x = 0, y = 0;
                    int codeOut = code1 != 0 ? code1 : code2;

                    if ((codeOut & TOP) != 0)
                    {
                        x = p1.X + (p2.X - p1.X) * (rect.Top - p1.Y) / (p2.Y - p1.Y);
                        y = rect.Top;
                    }
                    else if ((codeOut & BOTTOM) != 0)
                    {
                        x = p1.X + (p2.X - p1.X) * (rect.Bottom - p1.Y) / (p2.Y - p1.Y);
                        y = rect.Bottom;
                    }
                    else if ((codeOut & RIGHT) != 0)
                    {
                        y = p1.Y + (p2.Y - p1.Y) * (rect.Right - p1.X) / (p2.X - p1.X);
                        x = rect.Right;
                    }
                    else if ((codeOut & LEFT) != 0)
                    {
                        y = p1.Y + (p2.Y - p1.Y) * (rect.Left - p1.X) / (p2.X - p1.X);
                        x = rect.Left;
                    }

                    if (codeOut == code1)
                    {
                        p1 = new PointF((float)x, (float)y);
                        code1 = ComputeCode(p1, rect);
                    }
                    else
                    {
                        p2 = new PointF((float)x, (float)y);
                        code2 = ComputeCode(p2, rect);
                    }
                }
            }

            return accept;
        }
    }
}
