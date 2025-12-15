using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabGrafica_Sapt4
{
    public partial class Form1 : Form
    {

        // Vectorul de translație (2,3)
        private readonly Point translationVector = new Point(2, 3);

        // Coordonatele triunghiului inițial
        private Point[] originalTriangle;
        public Form1()
        {
            InitializeComponent();

            // Inițializează triunghiul original (coordonate în pixeli)
            originalTriangle = new Point[]
            {
                new Point(50, 50),
                new Point(150, 50),
                new Point(100, 150)
            };

            // Atașăm evenimentul Paint al PictureBox-ului
            pictureBox1.Paint += pictureBox1_Paint;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Triunghiul tradus
            Point[] translatedTriangle = TranslatePoints(originalTriangle, translationVector);

            // Desenăm triunghiul original (albastru, semi-transparent)
            using (var brushOrig = new SolidBrush(Color.FromArgb(120, Color.SteelBlue)))
            using (var penOrig = new Pen(Color.Blue, 2))
            {
                g.FillPolygon(brushOrig, originalTriangle);
                g.DrawPolygon(penOrig, originalTriangle);
            }

            // Desenăm triunghiul tradus (roșu)
            using (var brushTrans = new SolidBrush(Color.FromArgb(120, Color.IndianRed)))
            using (var penTrans = new Pen(Color.Red, 2))
            {
                g.FillPolygon(brushTrans, translatedTriangle);
                g.DrawPolygon(penTrans, translatedTriangle);
            }

            // Desenăm punctele vârfurilor
            DrawVertices(g, originalTriangle, Color.Blue);
            DrawVertices(g, translatedTriangle, Color.Red);
        }

        private static Point[] TranslatePoints(Point[] points, Point vec)
        {
            Point[] result = new Point[points.Length];
            for (int i = 0; i < points.Length; i++)
                result[i] = new Point(points[i].X + vec.X, points[i].Y + vec.Y);
            return result;
        }

        private void DrawVertices(Graphics g, Point[] pts, Color color)
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
