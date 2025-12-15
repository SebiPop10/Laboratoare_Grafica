using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorFractal
{
    public partial class Form1 : Form
    {
        private Random rnd= new Random();
        public Form1()
        {
            InitializeComponent();
            this.Text = "Nor Fractal";
            this.Size = new Size(800, 700);
            this.Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.SkyBlue);

            DrawFractalCloud(g, 250, 250, 80, 3);
        }

        private void DrawFractalCloud(Graphics g, float x, float y, float radius, int depth)
        {
            if(depth == 0)
            {
                return;
            }

            Brush brush= new SolidBrush(Color.FromArgb(200, 255, 255, 255));
            g.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);

            int children = 2;

            for(int i=0; i<children; i++)
            {
                float angle = (float)(rnd.NextDouble() * 2 * Math.PI);
                float distance = radius * 0.5f;
                float newX = x + (float)(Math.Cos(angle) * distance);
                float newY = y + (float)(Math.Sin(angle) * distance);
                DrawFractalCloud(g, newX, newY, radius * 0.5f, depth - 1);
            }
        }
    }
}
