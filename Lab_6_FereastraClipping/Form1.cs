using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_6_FereastraClipping
{
    public partial class Form1 : Form
    {
        float xmin = 100, ymin = 100, xmax = 300, ymax = 200;

        const int INTERIOR = 0;
        const int STANGA = 1;
        const int DREAPTA = 2;

        private void Desenare(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen contur = new Pen(Color.Black, 2);
            g.DrawRectangle(contur, xmin, ymin, xmax-xmin, ymax-ymin);
            Pen linieOriginala = new Pen(Color.Green, 1);
            g.DrawLine(linieOriginala, 50, 50, 400, 250);
            Clipping(g, 50, 50, 400, 250);
        }

        const int JOS = 4;
        const int SUS = 8;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Lab 6 Fereastra Clipping - Algoritmul Cohen-Sutherland";
            this.Width = 500;
            this.Height = 400;
            this.Paint += Desenare;
        }

        int CodRegiune(float x, float y)
        {
            int cod = INTERIOR;
            if (x < xmin) // in stanga
                cod |= STANGA;
            else if (x > xmax) // in dreapta
                cod |= DREAPTA;
            if (y < ymin) // jos
                cod |= JOS;
            else if (y > ymax) // sus
                cod |= SUS;
            return cod;
        }

        void Clipping(Graphics grafica, float x1, float y1, float x2, float y2)
        {
            int cod1 = CodRegiune(x1, y1);
            int cod2 = CodRegiune(x2, y2);
            bool acceptat = false;
            while (true)
            {
                if ((cod1|cod2) ==0)
                {
                    acceptat = true;
                    break;
                }
                else if ((cod1 & cod2) != 0)
                {
                    break;
                }
                else
                {
                    int cod_exterior;
                    float x = 0, y = 0;
                    if (cod1!=0)
                        cod_exterior = cod1;
                    else
                        cod_exterior = cod2;
                    if ((cod_exterior & SUS) != 0)
                    {
                        x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                        y = ymax;
                    }
                    else if ((cod_exterior & JOS) != 0)
                    {
                        x = x1 + (x2 - x1) * (ymin - y1) / (y2 - y1);
                        y = ymin;
                    }
                    else if ((cod_exterior & DREAPTA) != 0)
                    {
                        y = y1 + (y2 - y1) * (xmax - x1) / (x2 - x1);
                        x = xmax;
                    }
                    else if ((cod_exterior & STANGA) != 0)
                    {
                        y = y1 + (y2 - y1) * (xmin - x1) / (x2 - x1);
                        x = xmin;
                    }
                    if (cod_exterior == cod1)
                    {
                        x1 = x;
                        y1 = y;
                        cod1 = CodRegiune(x1, y1);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        cod2 = CodRegiune(x2, y2);
                    }
                }
            }
            if (acceptat)
            {
                grafica.DrawLine(Pens.Red, x1, y1, x2, y2);
            }
        }


    }
}
