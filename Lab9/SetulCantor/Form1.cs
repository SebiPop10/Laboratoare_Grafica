using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetulCantor
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Text = "Setul Cantor";
            this.Width = 800;
            this.Height = 600;
            this.DoubleBuffered = true;
           
            this.ResumeLayout(false);
        }

        public List<(double, double)> GenereazaCantor(int iteratii)
        {
            var segmente = new List<(double, double)>
            {
                (0.0, 1.0)
            };

            for(int i=0;i<iteratii;i++)
            {
                var segmenteNoi = new List<(double, double)>();
                foreach (var segment in segmente)
                {
                    double stanga = segment.Item1;
                    double dreapta = segment.Item2;
                    double oTreime = (dreapta - stanga)/3.0;

                    segmenteNoi.Add((stanga, stanga + oTreime));
                    segmenteNoi.Add((dreapta - oTreime, dreapta));
                }
                segmente = segmenteNoi;
            }
            return segmente;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //varianta fara slider pentru nr de iteratii
            //int nrIteratii = 5;

            //varianta cu slider pentru nr de iteratii
            int nrIteratii = trackBarIteratii.Value;
            int inaltimeSegment = 20;
            int spatiuVertical = 10;

            int latimeFormular= this.ClientSize.Width;

            for(int nivel=0; nivel<=nrIteratii; nivel++)
            {
                var segmente = GenereazaCantor(nivel);
                int y = nivel * (inaltimeSegment + spatiuVertical) + spatiuVertical;
                foreach(var segment in segmente)
                {
                    double stanga = segment.Item1;
                    double dreapta = segment.Item2;
                    int x1 = (int)(stanga * latimeFormular);
                    int x2 = (int)(dreapta * latimeFormular);
                    //e.Graphics.FillRectangle(Brushes.Black, x1, y, x2 - x1, inaltimeSegment);
                    e.Graphics.DrawLine(Pens.Black, x1, y, x2, y);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            trackBarIteratii.Value=trackBarIteratii.Value+1;
            //this.Paint+= new PaintEventHandler(Form1_Paint);
            this.Invalidate();
        }

        private void trackBarIteratii_Scroll(object sender, EventArgs e)
        {
            
                
        }
    }
}
