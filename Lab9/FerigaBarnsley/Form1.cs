using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerigaBarnsley
{
    public partial class Form1 : Form
    {
        private List<(double, double)> puncteFractal;

        //Genereaza fractalul Feriga Barnsley in timp real
        //public TrackBar trackBarIteratii;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Fractalul Feriga Barnsley";
            this.Width = 800;
            this.Height = 800;
            this.DoubleBuffered = true;
            
            this.ResumeLayout(false);
        }

        private List<(double, double)> GenereazaFerigaBarnsley(int nrPuncte)
        {
            var puncte = new List<(double, double)>();
            double x = 0, y = 0;
            Random rand = new Random();

            for (int i = 0; i < nrPuncte; i++)
            {
                double r = rand.NextDouble();
                double xNou, yNou;
                if (r < 0.01)
                {
                    xNou = 0;
                    yNou = 0.16 * y;
                }
                else if (r < 0.86)
                {
                    xNou = 0.85 * x + 0.04 * y;
                    yNou = -0.04 * x + 0.85 * y + 1.6;
                }
                else if (r < 0.93)
                {
                    xNou = 0.20 * x - 0.26 * y;
                    yNou = 0.23 * x + 0.22 * y + 1.6;
                }
                else
                {
                    xNou = -0.15 * x + 0.28 * y;
                    yNou = 0.26 * x + 0.24 * y + 0.44;
                }
                x = xNou;
                y = yNou;
                puncte.Add((x, y));
            }
            return puncte;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (puncteFractal == null || puncteFractal.Count == 0)
                return;

            int latime = this.ClientSize.Width;
            int inaltime = this.ClientSize.Height;

            double xMin = double.MaxValue, xMax = double.MinValue;
            double yMin = double.MaxValue, yMax = double.MinValue;

            // Calculează limitele fractalului
            foreach (var p in puncteFractal)
            {
                if (p.Item1 < xMin) xMin = p.Item1;
                if (p.Item1 > xMax) xMax = p.Item1;
                if (p.Item2 < yMin) yMin = p.Item2;
                if (p.Item2 > yMax) yMax = p.Item2;
            }

            int total = puncteFractal.Count;

            for (int i = 0; i < total; i++)
            {
                var punct = puncteFractal[i];

                // Transformare coordonate
                int px = (int)((punct.Item1 - xMin) / (xMax - xMin) * latime);
                int py = (int)(inaltime - (punct.Item2 - yMin) / (yMax - yMin) * inaltime);

                // --------------------------
                // GRADIENT PROGRESIV
                // --------------------------
                double t = (double)i / total; // 0 → 1

                // Exemplu gradient verde → galben → roșu
                int r = (int)(255 * t);
                int g = (int)(255 * (1 - Math.Abs(t - 0.5) * 2)); // scade spre roșu
                int b = 0;

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(r, g, b)))
                {
                    e.Graphics.FillRectangle(brush, px, py, 1, 1);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            trackBar1.Value = trackBar1.Value+1000;
            puncteFractal = GenereazaFerigaBarnsley(trackBar1.Value);
            label1.Text = "Val curenta: " + trackBar1.Value.ToString();
            this.Invalidate();
        }

        
    }
}
