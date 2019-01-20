using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMatrix;

namespace Graph_Lab_5
{
    public static class PointTransformation3D
    {
        public static double[] ObliqueProection(double[] v, double l, double fi)
        {
            Vector vector = new Vector(v);
            Matrix proectMatrix = new Matrix(new double[][] {
                new double[] { 1, 0, l * Math.Cos(fi), 0 },
                new double[] { 0, 1, l * Math.Sin(fi), 0 },
                new double[] { 0, 0, 1, 0 },
                new double[] { 0, 0, 0, 1 }
            });

            return proectMatrix.MultiplyOnVectorColumn(vector).GetCloneOfData();
        }

        public static double[] IsometricProection(double[] v)
        {
            Vector vector = new Vector(v);
            Matrix proectMatrix = new Matrix(new double[][] {
                new double[] { 0.707, 0, -0.707, 0 },
                new double[] { -0.408, 0.816, -0.408, 0 },
                new double[] { 0, 0, 1, 0 },
                new double[] { 0, 0, 0, 1 }
            });

            return proectMatrix.MultiplyOnVectorColumn(vector).GetCloneOfData();
        }

        public static double[] RotateX(double[] v, double f)
        {
            Vector vector = new Vector(v);
            Matrix rotateMatrix = new Matrix(new double[][] {
                new double[] { 1, 0, 0, 0 },
                new double[] { 0, Math.Cos(f), -Math.Sin(f), 0 },
                new double[] { 0, Math.Sin(f), Math.Cos(f), 0 },
                new double[] { 0, 0, 0, 1 }
            });

            return rotateMatrix.MultiplyOnVectorColumn(vector).GetCloneOfData();
        }

        public static double[] RotateY(double[] v, double f)
        {
            Vector vector = new Vector(v);
            Matrix rotateMatrix = new Matrix(new double[][] {
                new double[] { Math.Cos(f), 0, Math.Sin(f), 0 },
                new double[] { 0, 1, 0, 0 },
                new double[] { -Math.Sin(f), 0, Math.Cos(f), 0 },
                new double[] { 0, 0, 0, 1 }
            });

            return rotateMatrix.MultiplyOnVectorColumn(vector).GetCloneOfData();
        }

        public static double[] RotateZ(double[] v, double f)
        {
            Vector vector = new Vector(v);
            Matrix rotateMatrix = new Matrix(new double[][] {
                new double[] { Math.Cos(f), -Math.Sin(f), 0, 0 },
                new double[] { Math.Sin(f), Math.Cos(f), 0, 0 },
                new double[] { 0, 0, 1, 0 },
                new double[] { 0, 0, 0, 1 }
            });

            return rotateMatrix.MultiplyOnVectorColumn(vector).GetCloneOfData();
        }

        public static double[] Translation(double[] v, double a, double b, double c)
        {
            Vector vector = new Vector(v);
            Matrix transMatrix = new Matrix(new double[][] {
                new double[] { 1, 0, 0, a },
                new double[] { 0, 1, 0, b },
                new double[] { 0, 0, 1, c },
                new double[] { 0, 0, 0, 1 }
            });

            return transMatrix.MultiplyOnVectorColumn(vector).GetCloneOfData();
        }

        public static double[] Scaling(double[] v, double a, double b, double c)
        {
            Vector vector = new Vector(v);
            Matrix transMatrix = new Matrix(new double[][] {
                new double[] { a, 0, 0, 0 },
                new double[] { 0, b, 0, 0 },
                new double[] { 0, 0, c, 0 },
                new double[] { 0, 0, 0, 1 }
            });

            return transMatrix.MultiplyOnVectorColumn(vector).GetCloneOfData();
        }

        public static double[] PerspectiveProection(double[] v, double p, double q, double r)
        {
            Vector vector = new Vector(v);
            Matrix proectMatrix = new Matrix(new double[][] {
                new double[] { 1, 0, 0, 0 },
                new double[] { 0, 1, 0, 0 },
                new double[] { 0, 0, 1, 0 },
                new double[] { p, q, r, 1 }
            });

            return proectMatrix.MultiplyOnVectorColumn(vector).GetCloneOfData();
        }
    }
}
