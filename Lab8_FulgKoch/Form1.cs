using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8_FulgKoch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Fulgul lui Koch";
            this.Size=new Size(800, 700);
            this.BackColor = Color.White;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g= e.Graphics; 

            PointF p1= new PointF(150, 500);
            PointF p2= new PointF(650, 500);
            PointF p3= new PointF(400, 150);

            int adancime = 5;
            DeseneazaKoch(g,p1, p2, adancime);
            DeseneazaKoch(g, p2, p3, adancime);
            DeseneazaKoch(g, p3, p1, adancime);
        }

        private void DeseneazaKoch(Graphics g, PointF a, PointF b, int adancime)
        {
            if(adancime==0)
            {
                g.DrawLine(Pens.Blue, a, b);
                return;
            }

            PointF pctIntermediar1 = new PointF(a.X + (b.X - a.X) / 3f, a.Y + (b.Y - a.Y) / 3f);
            PointF pctIntermediar2 = new PointF(a.X + 2f * (b.X - a.X) / 3f, a.Y + 2f * (b.Y - a.Y) / 3f);

            double unghi = Math.PI / 3;
            float deltaX= pctIntermediar2.X - pctIntermediar1.X;
            float deltaY= pctIntermediar2.Y - pctIntermediar1.Y;

            PointF varfTriunghi = new PointF(
                (float)(pctIntermediar1.X + deltaX/2 - Math.Sqrt(3)*deltaY/2),
                (float)(pctIntermediar1.Y + deltaY/2 - Math.Sqrt(3)*deltaX/2)
            );

            DeseneazaKoch(g, a, pctIntermediar1, adancime - 1);
            DeseneazaKoch(g, pctIntermediar1, varfTriunghi, adancime - 1);
            DeseneazaKoch(g, varfTriunghi, pctIntermediar2, adancime - 1);
            DeseneazaKoch(g, pctIntermediar2, b, adancime - 1);
        }
    }
}
