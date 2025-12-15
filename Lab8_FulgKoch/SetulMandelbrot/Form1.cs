using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetulMandelbrot
{
    public partial class Form1 : Form
    {
        const int LATIME = 800;
        const int INALTIME = 600;
        const int MAX_ITERATII = 500;

        double minRe= -2.5;
        double maxRe= 1.0;
        double minIm= -1.0;
        double maxIm= 1.0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Setul Mandelbrot";
            this.Size = new Size(LATIME, INALTIME);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(LATIME, INALTIME);   

            for(int y=0; y<INALTIME; y++)
            {
                double cIm = maxIm - y * (maxIm - minIm) / (INALTIME - 1);
                for(int x=0; x<LATIME; x++)
                {
                    double cRe = minRe + x * (maxRe - minRe) / (LATIME - 1);
                    double Z_re = cRe, Z_im = cIm;
                    bool esteInMultime = true;
                    int n;
                    for(n=0; n<MAX_ITERATII; n++)
                    {
                        double Z_re2 = Z_re * Z_re, Z_im2 = Z_im * Z_im;
                        if(Z_re2 + Z_im2 > 4)
                        {
                            esteInMultime = false;
                            break;
                        }
                        Z_im = 2 * Z_re * Z_im + cIm;
                        Z_re = Z_re2 - Z_im2 + cRe;
                    }
                    if(esteInMultime)
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        int culoare = (int)(255 * n / MAX_ITERATII);
                        bmp.SetPixel(x, y, Color.FromArgb(culoare, culoare, culoare));
                    }
                }
            }
            e.Graphics.DrawImage(bmp, 0, 0);

        }
    }
}
