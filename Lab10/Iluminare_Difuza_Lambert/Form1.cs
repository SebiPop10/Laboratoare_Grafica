using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Iluminare_Difuza_Lambert
{
    public partial class Form1 : Form
    {
        const int GridSize = 100;
        float[,] heightMap = new float[GridSize, GridSize];

        float lighyX=0.5f, lightY=0.5f, lightZ=-1f;
        float kd=1.0f; // Coeficient de difuzie
        float IL=1.0f; // Intensitatea luminii
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ClientSize= new Size(600, 600);
            GenerateSurface();
        }

        private void GenerateSurface()
        {
            for(int x=0; x<GridSize; x++)
            {
                for(int y=0; y<GridSize; y++)
                {
                    float dx=x- GridSize / 2f;
                    float dy=y- GridSize / 2f;
                    heightMap[x, y] = (float)(50 * Math.Sin(dx * 0.1f) * Math.Cos(dy * 0.1f));
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawLambert(e.Graphics);
        }

        private void DrawLambert(Graphics g)
        {
            int cell=Math.Min(ClientSize.Width, ClientSize.Height) / GridSize;

            for(int x=1;x<GridSize-1;x++)
            {
                for(int y=1;y<GridSize-1;y++)
                {
                    float xL=x-1, yL=y, zL=heightMap[x-1,y];
                    float xR=x+1, yR=y, zR=heightMap[x+1,y];
                    float xU=x, yU=y-1, zU=heightMap[x,y-1];
                    float xD=x, yD=y+1, zD=heightMap[x,y+1];

                    float dxX= xR - xL;
                    float dxY= yR - yL;
                    float dxZ= zR - zL;

                    float dyX= xU-xD;
                    float dyY= yU - yD;
                    float dyZ= zU - zD;

                    float nx= dxY * dyZ - dxZ * dyY;
                    float ny= dxZ * dyX - dxX * dyZ;
                    float nz= dxX * dyY - dxY * dyX;

                    float length=(float)Math.Sqrt(nx * nx + ny * ny + nz * nz);
                    if(length!=0)
                    {
                        nx /= length;
                        ny /= length;
                        nz /= length;
                    }

                    float dot= Math.Max(0f,nx * lighyX + ny * lightY + nz * lightZ);
                    float intensity= kd * IL * dot;
                    int shade= (int)(intensity * 255);
                    if(shade>255) shade=255;
                    if(shade<0) shade=0;

                    Brush brush= new SolidBrush(Color.FromArgb(shade, shade, shade));
                    g.FillRectangle(brush, x * cell, y * cell, cell, cell);
                }
            }
        }
    }
}
