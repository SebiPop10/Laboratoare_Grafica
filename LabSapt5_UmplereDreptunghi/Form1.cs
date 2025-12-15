using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabSapt5_UmplereDreptunghi
{
    public partial class Form1 : Form
    {
        private Bitmap bmp;
        private PictureBox pictureBox;
        public Form1()
        {
            //InitializeComponent();
            this.Text = "Umplere Dreptunghi cu Scan-Line";
            this.Width = 800;
            this.Height = 600;
            bmp = new Bitmap(800, 600);
            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = bmp
            };
            this.Controls.Add(pictureBox);

            UmpleDreptunghi(50, 50, 200, 100, Color.Blue);
        }

        private void UmpleDreptunghi(int xmin, int ymin, int xmax, int ymax, Color culoare)
        {
            for (int i = ymin; i <= ymax; i++)
            {
                for (int j = xmin; j <= xmax; j++)
                {
                    bmp.SetPixel(j, i, culoare);
                }
            }
            pictureBox.Invalidate();
        }

        //[STAThread]
        //public static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    //Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}
    }
}
