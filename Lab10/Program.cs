using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float Ax=0, Ay=0, Az=0;
            float Bx=1, By=0, Bz=0;
            float Cx=0, Cy=1, Cz=0;

            float nx, ny, nz;
            CalculateTriangleNormal(Ax, Ay, Az, Bx, By, Bz, Cx, Cy, Cz, out nx, out ny, out nz);
            Console.WriteLine($"Normal Vector: ({nx}, {ny}, {nz})");
        }

        static void CalculateTriangleNormal(float Ax, float Ay, float Az, float Bx, float By, float Bz, float Cx, float Cy, float Cz, out float nx, out float ny, out float nz)
        {
            float Ux = Bx - Ax;
            float Uy = By - Ay;
            float Uz = Bz - Az;

            float Vx = Cx - Ax;
            float Vy = Cy - Ay;
            float Vz = Cz - Az;

            nx= Uy * Vz - Uz * Vy;
            ny= Uz * Vx - Ux * Vz;
            nz= Ux * Vy - Uy * Vx;

            float length = (float)Math.Sqrt(nx * nx + ny * ny + nz * nz);
            if (length!=0)
            {
                nx /= length;
                ny /= length;
                nz /= length;
            }
        }
    }
}
