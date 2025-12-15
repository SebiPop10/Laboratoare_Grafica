using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriunghiSierpinski
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Triunghiul lui Sierpinski";
            this.Size = new Size(800, 700);
            this.BackColor = Color.White;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            PointF p1 = new PointF(400, 50);
            PointF p2 = new PointF(50, 650);
            PointF p3 = new PointF(750, 650);

            int adancime = 6;
            DeseneazaSierpinski(g, adancime, p1, p2, p3);
        }

        private void DeseneazaSierpinski(Graphics g, int adancime, PointF a, PointF b, PointF c)
        {
            if (adancime == 0)
            {
                g.FillPolygon(Brushes.Blue, new PointF[] { a, b, c });  
                return;
            }
                
            PointF ab=Medie(a,b);   
            PointF bc=Medie(b,c);
            PointF ca=Medie(c,a);

            DeseneazaSierpinski(g, adancime - 1, a, ab, ca);
            DeseneazaSierpinski(g, adancime - 1, ab, b, bc);
            DeseneazaSierpinski(g, adancime - 1, ca, bc, c);
        }

        private PointF Medie(PointF p1, PointF p2)
        {
            return new PointF((p1.X + p2.X) / 2f, (p1.Y + p2.Y) / 2f);
        }
    }
}
