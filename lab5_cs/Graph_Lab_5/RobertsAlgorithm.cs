using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMatrix;

namespace Graph_Lab_5
{
    public static class RobertsAlgorithm
    {
        public static double[] ToSpeciesCS(double[] p, double r, double fi, double te)
        {
            Vector point = new Vector(p);
            double cosf = Math.Cos(fi),
                sinf = Math.Sin(fi),
                cost = Math.Cos(te),
                sint = Math.Sin(te);
            Matrix A = new Matrix(new double[4][]
            {
                new double[] { -sinf, cosf, 0, 0 },
                new double[] { -cost*cosf, -cost*sinf, sint, 0 },
                new double[] { -sint*cosf, -sint*sinf, -cost, r },
                new double[] { 0, 0, 0, 1 }
            });

            return A.MultiplyOnVectorColumn(point).GetCloneOfData();
        }

        public static double[] PlaneEquation(double[][] p)
        {
            double a = (p[1][1] - p[0][1]) * (p[2][2] - p[0][2]) - (p[1][2] - p[0][2]) * (p[2][1] - p[0][1]),//new Matrix(new double[][] { new double[] { p[1][1] - p[0][1], p[1][2] - p[0][2] }, new double[] { p[2][1] - p[0][1], p[2][2] - p[0][2] } }).Determinant(),
                b = -(p[1][0] - p[0][0]) * (p[2][2] - p[0][2]) + (p[1][2] - p[0][2]) * (p[2][0] - p[0][0]),//-new Matrix(new double[][] { new double[] { p[1][0] - p[0][0], p[1][2] - p[0][2] }, new double[] { p[2][0] - p[0][0], p[2][2] - p[0][2] } }).Determinant(),
                c = (p[1][0] - p[0][0]) * (p[2][1] - p[0][1]) - (p[1][1] - p[0][1]) * (p[2][0] - p[0][0]);//new Matrix(new double[][] { new double[] { p[1][0] - p[0][0], p[1][1] - p[0][1] }, new double[] { p[2][0] - p[0][0], p[2][1] - p[0][1] } }).Determinant();

            return new double[] { a, b, c,
                -p[0][0] * a - p[0][1] * b - p[0][2] * c};
        }

        public static int[] Algorith(double[][][] Points, double E, double fi, double teta)
        {
            Points = Points.Select(f => f.Select(p => ToSpeciesCS(p, E, fi, teta)).ToArray()).ToArray();
            //ObserverPoint = RobertsAlgorithm.ToSpeciesCS(ObserverPoint, 1000, 0, 0);

            List<int> fIndexes = new List<int>();
            for (int i = 0; i < Points.Length; i++)
            {
                if (PlaneEquation(Points[i])[3] > 0)
                    fIndexes.Add(i);
            }

            return fIndexes.ToArray();
        }

        public static bool IsVisibleSide(double[] p1, double[] p2, double[] p3, double E, double fi, double teta)
        {
            List<double[]> newTriangle = new List<double[]>();
            newTriangle.Add(ToSpeciesCS(p1, E, fi, teta));
            newTriangle.Add(ToSpeciesCS(p2, E, fi, teta));
            newTriangle.Add(ToSpeciesCS(p3, E, fi, teta));

            return PlaneEquation(newTriangle.ToArray())[3] > 0;
        }
    }
}
