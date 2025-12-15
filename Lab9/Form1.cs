using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab9
{
    public partial class Form1 : Form
    {
        Bitmap imagineJulia;
        public Form1()
        {
            InitializeComponent();
            this.Text="Julia Set";
            this.Width = 800;
            this.Height = 800;

            GenereazaSetJulia();
            this.DoubleBuffered = true;
        }

        private void GenereazaSetJulia()
        {
            int latime = this.ClientSize.Width;
            int inaltime=this.ClientSize.Height;

            imagineJulia = new Bitmap(latime, inaltime);
            double xMin = -3, xMax = 3;
            double yMin = -3, yMax = 3;

            double cRe = -0.7;
            double cIm = 0.27015;
            int maxIter = 150;

            for(int px=0; px<latime; px++)
            {
                for(int py=0; py<inaltime; py++)
                {
                    double x0 = xMin + px * (xMax - xMin) / (latime - 1);
                    double y0 = yMin + py * (yMax - yMin) / (inaltime - 1);
                    double x = x0;
                    double y = y0;
                    int iteration = 0;
                    while(x*x + y*y < 4 && iteration < maxIter)
                    {
                        double xTemp = x*x - y*y + cRe;
                        y = 2*x*y + cIm;
                        x = xTemp;
                        iteration++;
                    }
                    Color color;
                    if(iteration == maxIter)
                    {
                        color = Color.Black;
                    }
                    else
                    {
                        int colorValue = (int)(255 * iteration / maxIter);
                        color = Color.FromArgb(colorValue, colorValue, 255 - colorValue);
                    }
                    imagineJulia.SetPixel(px, py, color);
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(imagineJulia != null)
            {
                e.Graphics.DrawImage(imagineJulia, 0, 0);
            }
        }
    }
}
