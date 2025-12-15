using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbSlide12
{
    public partial class Form1 : Form
    {
        private List<Point> controlPoints = new List<Point>();
        private Button btnReset;

        public Form1()
        {
            InitializeComponent();

            // îmbunătățiri UI
            this.Text = "Bézier - click 4 puncte";
            this.DoubleBuffered = true;

            // buton reset (poți să-l pui și din designer)
            btnReset = new Button()
            {
                Text = "Reset",
                Width = 80,
                Height = 30,
                Left = 10,
                Top = 10
            };
            btnReset.Click += (s, e) =>
            {
                controlPoints.Clear();
                Invalidate();
            };
            this.Controls.Add(btnReset);
        }

        // folosesc override ca să nu depind de wiring din designer
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // adaug punct doar dacă sunt < 4
            if (controlPoints.Count < 4)
            {
                controlPoints.Add(e.Location);
                Invalidate();
            }
            // optional: dacă vrei să permită și mai multe seturi,
            // poți reseta automat după 4 puncte:
            // else { controlPoints.Clear(); controlPoints.Add(e.Location); Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Desenăm un mic text cu numărul de puncte (diagnostic)
            string info = $"Puncte: {controlPoints.Count} (click pentru a adăuga, Reset pentru a șterge)";
            g.DrawString(info, this.Font, Brushes.Black, new PointF(100, 12));

            // Desenează punctele de control (roșii) și indexul lor
            for (int i = 0; i < controlPoints.Count; i++)
            {
                var p = controlPoints[i];
                g.FillEllipse(Brushes.Red, p.X - 4, p.Y - 4, 8, 8);
                // indexul punctului lângă el
                g.DrawString(i.ToString(), this.Font, Brushes.Black, p.X + 6, p.Y - 8);
            }

            // Desenează poligonul de control (gri) dacă sunt >= 2 puncte
            if (controlPoints.Count >= 2)
            {
                g.DrawLines(Pens.Gray, controlPoints.ToArray());
            }

            // Desenează curba Bézier doar când avem exact 4 puncte
            if (controlPoints.Count == 4)
            {
                DrawBezier(g, controlPoints.ToArray());
            }
        }

        private void DrawBezier(Graphics g, Point[] pts)
        {
            int steps = 200; // mai fin
            PointF prev = BezierPoint(pts, 0f);

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
            // Asigură-te că primim exact 4 puncte
            if (pts == null || pts.Length != 4)
                return PointF.Empty;

            float u = 1 - t;
            float b0 = u * u * u;
            float b1 = 3 * u * u * t;
            float b2 = 3 * u * t * t;
            float b3 = t * t * t;

            float x = b0 * pts[0].X + b1 * pts[1].X + b2 * pts[2].X + b3 * pts[3].X;
            float y = b0 * pts[0].Y + b1 * pts[1].Y + b2 * pts[2].Y + b3 * pts[3].Y;

            return new PointF(x, y);
        }



    }
}
