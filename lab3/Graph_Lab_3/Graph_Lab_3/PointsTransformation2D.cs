using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMatrix;

namespace Graph_Lab_3
{
    public static class PointTransformations2D
    {
        public static double Angle(double[] v1, double[] v2)
        {
            return Math.Atan2(v2[1], v2[0]) - Math.Atan2(v1[1], v1[0]);
        }

        public static double[] Shift(double[] p, double kx, double ky)
        {
            return new double[] { p[0] + kx, p[1] + ky, 1 };
        }

        public static double[] Turn(double[] p, double alp)
        {
            Vector vp = new Vector(p);
            Matrix turnMat = new Matrix(new double[][] {
                new double[] { Math.Cos(alp), -Math.Sin(alp), 0 },
                new double[] { Math.Sin(alp), Math.Cos(alp), 0 },
                new double[] { 0, 0, 1 }
            });

            return turnMat.MultiplyOnVectorColumn(vp).GetCloneOfData();
        }

        public static double[] ReflectionX(double[] p)
        {
            p[1] *= -1;

            return p;
        }

        public static double[] Scaling(double[] p, double kx, double ky)
        {
            Vector vp = new Vector(p);
            Matrix scalMat = new Matrix(new double[][] {
                new double[] { kx, 0, 0 },
                new double[] { 0, ky, 0 },
                new double[] { 0, 0, 1 }
            });

            return scalMat.MultiplyOnVectorColumn(vp).GetCloneOfData();
        }
    }
}
