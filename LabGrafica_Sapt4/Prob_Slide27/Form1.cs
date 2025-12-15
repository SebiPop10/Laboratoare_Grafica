using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prob_Slide27
{
    public partial class Form1 : Form
    {
        // Struct pentru punct 3D
        private struct Point3D
        {
            public float X, Y, Z;
            public Point3D(float x, float y, float z) { X = x; Y = y; Z = z; }
        }

        // Date inițiale
        private Point3D P = new Point3D(80, 40, 60);                       // punctul P
        private Point3D P_translated;                                       // P + T
        private Point3D P_rotated;                                          // P rotit în jurul Ox
        private Point3D lineP1 = new Point3D(30, -10, 20);                  // P1 pentru linie
        private Point3D lineP2 = new Point3D(130, 30, 80);                  // P2 pentru linie
        private Point3D lineP1_scaled;                                      // P1 scalat
        private Point3D lineP2_scaled;                                      // P2 scalat

        // Fix: vector de translație T(1,1,1)
        private readonly Point3D T = new Point3D(1, 1, 1);

        // Parametri proiecție
        private float viewScale = 2.0f; // scala proiecției 3D -> pixeli
        private PointF viewCenter;
        public Form1()
        {
            InitializeComponent();

            // Setări inițiale controale (în caz că nu le-ai setat în designer)
            numRot.Minimum = 0;
            numRot.Maximum = 360;
            numRot.Value = 45;
            numRot.Increment = 5;

            numScale.Minimum = 1;
            numScale.Maximum = 10;
            numScale.DecimalPlaces = 2;
            numScale.Value = 2;
            numScale.Increment = 1;

            // calculează variantele inițiale
            ComputeTransforms();

            // atașare eveniment desen
            pictureBox1.Paint += pictureBox1_Paint;

            // evenimente control
            numRot.ValueChanged += (s, e) => { ComputeTransforms(); pictureBox1.Invalidate(); };
            numScale.ValueChanged += (s, e) => { ComputeTransforms(); pictureBox1.Invalidate(); };
            btnReset.Click += (s, e) => { ResetScene(); pictureBox1.Invalidate(); };

            // centru vizualizare
            pictureBox1.Resize += (s, e) => { viewCenter = new PointF(pictureBox1.Width / 2f, pictureBox1.Height / 2f); };
            viewCenter = new PointF(pictureBox1.Width / 2f, pictureBox1.Height / 2f);
        }

        private void ResetScene()
        {
            // Reset la valorile inițiale
            P = new Point3D(80, 40, 60);
            lineP1 = new Point3D(30, -10, 20);
            lineP2 = new Point3D(130, 30, 80);

            numRot.Value = 45;
            numScale.Value = 2;

            ComputeTransforms();
        }

        private void ComputeTransforms()
        {
            // 1) Translație P -> P + T
            P_translated = Translate(P, T);

            // 2) Rotație în jurul Ox: unghi din NumericUpDown (grade -> radiani)
            float alphaDeg = (float)numRot.Value;
            float alphaRad = alphaDeg * (float)(Math.PI / 180.0);
            P_rotated = RotateX(P, alphaRad);

            // 3) Scalarea dreptunghiului/liniei (față de origine) - factor din NumericUpDown
            float s = (float)numScale.Value;
            lineP1_scaled = ScalePoint(lineP1, s);
            lineP2_scaled = ScalePoint(lineP2, s);
        }

        // Transformări 3D (față de origine)
        private Point3D Translate(Point3D p, Point3D t) => new Point3D(p.X + t.X, p.Y + t.Y, p.Z + t.Z);

        // Rotație în jurul Ox (sens trigonometric)
        private Point3D RotateX(Point3D p, float alpha)
        {
            // x' = x
            // y' = y*cos(alpha) - z*sin(alpha)
            // z' = y*sin(alpha) + z*cos(alpha)
            float yNew = (float)(p.Y * Math.Cos(alpha) - p.Z * Math.Sin(alpha));
            float zNew = (float)(p.Y * Math.Sin(alpha) + p.Z * Math.Cos(alpha));
            return new Point3D(p.X, yNew, zNew);
        }

        private Point3D ScalePoint(Point3D p, float s) => new Point3D(p.X * s, p.Y * s, p.Z * s);

        // Proiecție 3D -> 2D (simplificată). Ajusteaz-o după nevoie.
        // Aici folosim o combinație de izometric / perspective simplă:
        // screenX = centerX + (x - z) * viewScale
        // screenY = centerY - (y + z*0.5) * viewScale
        private PointF Project(Point3D p)
        {
            float sx = viewCenter.X + (p.X - p.Z) * viewScale;
            float sy = viewCenter.Y - (p.Y + p.Z * 0.5f) * viewScale;
            return new PointF(sx, sy);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Axele coordonatelor (3D) - reprezentate schematic
            DrawAxes(g);

            // ---------- DESENĂRI ----------
            // 1) Punct original P (albastru)
            DrawPoint3D(g, P, Color.SteelBlue, "P (orig)");

            // 2) Punct translat (P + T) -> roșu
            DrawPoint3D(g, P_translated, Color.Red, "P + T(1,1,1)");

            // 3) Punct rotit în jurul Ox -> portocaliu
            DrawPoint3D(g, P_rotated, Color.Orange, "P rotit ~ Ox");

            // 4) Linia originală (P1->P2) (verde)
            DrawLine3D(g, lineP1, lineP2, Color.Green, "Linie originală");

            // 5) Linia scalată (violet)
            DrawLine3D(g, lineP1_scaled, lineP2_scaled, Color.Purple, $"Linie scalată x{numScale.Value}");

            // Legendă simplă în colț
            DrawLegend(g);
        }

        private void DrawAxes(Graphics g)
        {
            // desenăm 3 axe stilizate
            using (var pen = new Pen(Color.Gray, 1))
            {
                // Ox: axă orizontală spre dreapta (X pozitiv)
                PointF origin = Project(new Point3D(0, 0, 0));
                PointF xEnd = Project(new Point3D(120, 0, 0));
                PointF yEnd = Project(new Point3D(0, 120, 0));
                PointF zEnd = Project(new Point3D(0, 0, 120));

                // Axele
                g.DrawLine(pen, origin, xEnd);
                g.DrawLine(pen, origin, yEnd);
                g.DrawLine(pen, origin, zEnd);

                // săgeți (simple)
                g.DrawString("Ox", this.Font, Brushes.Gray, xEnd);
                g.DrawString("Oy", this.Font, Brushes.Gray, yEnd);
                g.DrawString("Oz", this.Font, Brushes.Gray, zEnd);
            }
        }

        private void DrawPoint3D(Graphics g, Point3D p, Color color, string label = null)
        {
            var pt = Project(p);
            using (var brush = new SolidBrush(color))
            using (var pen = new Pen(color, 1))
            {
                float r = 6f;
                g.FillEllipse(brush, pt.X - r, pt.Y - r, r * 2, r * 2);
                g.DrawEllipse(pen, pt.X - r, pt.Y - r, r * 2, r * 2);
                if (!string.IsNullOrEmpty(label))
                {
                    g.DrawString(label, this.Font, Brushes.Black, pt.X + 8, pt.Y - 8);
                }
            }
        }

        private void DrawLine3D(Graphics g, Point3D p1, Point3D p2, Color color, string label = null)
        {
            var a = Project(p1);
            var b = Project(p2);
            using (var pen = new Pen(color, 2))
            {
                g.DrawLine(pen, a, b);
                // desenăm vârfurile
                g.FillEllipse(new SolidBrush(color), a.X - 3, a.Y - 3, 6, 6);
                g.FillEllipse(new SolidBrush(color), b.X - 3, b.Y - 3, 6, 6);

                if (!string.IsNullOrEmpty(label))
                {
                    // label aproape de mijlocul segementului
                    var mid = new PointF((a.X + b.X) / 2, (a.Y + b.Y) / 2);
                    g.DrawString(label, this.Font, Brushes.Black, mid.X + 6, mid.Y + 6);
                }
            }
        }

        private void DrawLegend(Graphics g)
        {
            int x = 10, y = 10;
            int h = 16;
            g.DrawString("Legend:", this.Font, Brushes.Black, x, y);
            y += h;
            g.FillRectangle(Brushes.SteelBlue, x, y, 12, 12); g.DrawString("P (orig)", this.Font, Brushes.Black, x + 18, y - 2); y += h;
            g.FillRectangle(Brushes.Red, x, y, 12, 12); g.DrawString("P + T(1,1,1)", this.Font, Brushes.Black, x + 18, y - 2); y += h;
            g.FillRectangle(Brushes.Orange, x, y, 12, 12); g.DrawString("P rotit ~ Ox", this.Font, Brushes.Black, x + 18, y - 2); y += h;
            g.FillRectangle(Brushes.Green, x, y, 12, 12); g.DrawString("Linie orig", this.Font, Brushes.Black, x + 18, y - 2); y += h;
            g.FillRectangle(Brushes.Purple, x, y, 12, 12); g.DrawString("Linie scalată", this.Font, Brushes.Black, x + 18, y - 2); y += h;
        }
    }
}
