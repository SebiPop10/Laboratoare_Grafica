using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prob_slide11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Scalare dreptunghi";
            this.Size= new Size(800, 600);
            this.BackColor = Color.White;
            this.DoubleBuffered=true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            PointF[] origRect= new PointF[]
            {
                new PointF(100,100),
                new PointF(200,100),
                new PointF(200,200),
                new PointF(100,200)
            };

            g.DrawPolygon(new Pen(Color.Blue,2), origRect);

            float scale = 2.0f;

            PointF[] scaledRect = new PointF[origRect.Length];

            for(int i=0; i<origRect.Length; i++)
            {
                scaledRect[i] = new PointF(origRect[i].X * scale, origRect[i].Y * scale);
            }

            g.DrawPolygon(new Pen(Color.Red,2), scaledRect);

            base.OnPaint(e);
        }
    }
}
