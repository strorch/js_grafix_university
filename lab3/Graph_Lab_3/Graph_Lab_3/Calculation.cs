using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMatrix;

namespace Graph_Lab_3
{
    public static class Calculation
    {
        public static class Curves
        {
            private static int Factorial(int i)
            {
                if (i <= 1)
                    return 1;
                else return i * Factorial(i - 1);
            }

            private static double C(int m, int i)
            {
                return (double)Factorial(m) / (Factorial(i) * Factorial(m - i));
            }

            public static Func<double, double[]> BuildBezier(double[][] p)
            {
                Func<double, double[]> resf = t =>
                {
                    double[] res = new double[2];
                    for (int i = 0; i < p.Length; i++)
                    {
                        res[0] += C(p.Length - 1, i) * Math.Pow(t, i) * Math.Pow(1d - t, p.Length - i - 1) * p[i][0];
                        res[1] += C(p.Length - 1, i) * Math.Pow(t, i) * Math.Pow(1d - t, p.Length - i - 1) * p[i][1];
                    }
                    return res;
                };

                return resf;
            }
        }

        public static class Splines
        {
            public static Func<double, double> GetSplineInterpolant(double[] xValues,double[] fValues, bool NeedSort = false)
            {
                int n = xValues.Length;
                if (NeedSort)
                    SortPointsByX(xValues, fValues, n);

                double[] h = Calc_h(xValues, n);
                Tuple<double[], double[], double[]> ArrayTuple = GetACBTuple(h, n);
                double[] f = GetF(fValues, h, n);

                List<double> c = TridiagonalMatrixSolving.Normal(ArrayTuple.Item1, ArrayTuple.Item2, ArrayTuple.Item3, f).ToList();
                c.Insert(0, 0.0);
                c.Add(0.0);

                double[] d = Calc_d(c, h, n);
                double[] b = Calc_b(c, d, h, fValues, n);

                return BuildSpline(fValues, b, c, d, xValues);
            }

            private static void SortPointsByX(double[] xValues, double[] fValues, int n)
            {
                Tuple<double, double>[] tuple = new Tuple<double, double>[n];
                for (int i = 0; i < n; i++)
                    tuple[i] = Tuple.Create<double, double>(xValues[i], fValues[i]);

                tuple = tuple.OrderBy(t => t.Item1).ToArray();
                for (int i = 0; i < n; i++)
                {
                    var t = tuple[i];
                    xValues[i] = t.Item1;
                    fValues[i] = t.Item2;
                }
            }

            private static double[] Calc_h(double[] xValues, int n)
            {
                var hArray = new double[n];
                for (int i = 1; i < n; i++)
                    hArray[i] = xValues[i] - xValues[i - 1];
                return hArray;
            }

            private static Tuple<double[], double[], double[]> GetACBTuple(double[] h, int n)
            {
                var A = new double[n - 2]; 
                var C = new double[n - 2]; 
                var B = new double[n - 2];
                for (int i = 1; i < (n - 1); i++)
                {
                    C[i - 1] = 2 * (h[i] + h[i + 1]); 
               
                    if (i != 1)
                        A[i - 1] = h[i];
                    if (i != n - 2)
                        B[i - 1] = h[i + 1];
                }
                return new Tuple<double[], double[], double[]>(A, C, B);
            }

            private static double[] GetF(double[] fValues, double[] h, int n)
            {
                double[] f = new double[n - 2];
                for (int i = 1; i < (n - 1); i++)
                    f[i - 1] = 6 * ((fValues[i + 1] - fValues[i]) / h[i + 1] - (fValues[i] -
                   fValues[i - 1]) / h[i]);
                return f;
            }

            private static double[] Calc_d(List<double> c, double[] h, int n)
            {
                double[] d = new double[n];
                for (int i = 1; i < n; i++)
                    d[i] = (c[i] - c[i - 1]) / h[i];
                return d;
            }

            private static double[] Calc_b(List<double> c, double[] d, double[] h, double[]fValues, int n)
            {
                double[] b = new double[n];
                for (int i = 1; i < n; i++)
                    b[i] = 0.5 * h[i] * c[i] - Math.Pow(h[i], 2) * d[i] / 6 + (fValues[i] - fValues[i-1]) / h[i];
                return b;
            }

            private static Func<double, double> BuildSpline(double[] fValues, double[] b, List<double> c, double[] d, double[] xValues)
            {
                Func<double, double> spline = x =>
                {
                    double xMIn = xValues.Min();
                    double xMax = xValues.Max();

                    int i = 1;
                    while (x > xValues[i])
                        i++;

                    double result = 0;
                    result += fValues[i];
                    result += b[i] * (x - xValues[i]);
                    result += 0.5 * c[i] * Math.Pow(x - xValues[i], 2);
                    result += d[i] * Math.Pow(x - xValues[i], 3) / 6;
                    return result;
                };
                return spline;
            }

            static int n;

            //единственная публичная ф-я, которая возвращает сплайн (параметрический)
            public static Func<double, Tuple<double, double>> GetParamericSplineInterpolant(double[] xValues, double[] fValues)
            {
                if (xValues.Length != fValues.Length)
                    throw new ArgumentException("Входные массивы должны быть одинаковой длинны");

                //устанавливается длинна массивов
                n = xValues.Length;

                //получаем параметрические сплайны
                //имеется в виду, что x, y будут зависеть от t 
                Func<double, double> Xspline = GetParametricSpline(xValues);
                Func<double, double> Yspline = GetParametricSpline(fValues);

                //возвращение делегата, который зависит от параметра  t 
                return GetSplineFromT(Xspline, Yspline);
            }

            public static Func<double, Tuple<double, double, double>> GetParamericSplineInterpolant(double[] xValues, double[] yValues, double[] zValues)
            {
                if (xValues.Length != yValues.Length || xValues.Length != zValues.Length)
                    throw new ArgumentException("Входные массивы должны быть одинаковой длинны");

                n = xValues.Length;

                Func<double, double> Xspline = GetParametricSpline(xValues),
                    Yspline = GetParametricSpline(yValues),
                    Zspline = GetParametricSpline(zValues);

                return GetSplineFromT(Xspline, Yspline, Zspline);
            }

            //возвращает параметрический сплайн для x,y
            private static Func<double, double> GetParametricSpline(double[] InputValues)
            {
                //список для значений переменной t
                //для параметрического сплайна (для x и y) t будет лежать на оси абсцис 
                List<double> tValues = new List<double>();

                for (int i = 1; i <= n; i++)
                    tValues.Add(i);

                //строим используя обычную интерполяцию сплайнами
                return GetSplineInterpolant(tValues.ToArray(), InputValues);
            }

            //возвращает делегат, который зааисит от t
            //а сам делегат по входному t возвращает кортеж (x, y) 
            private static Func<double, Tuple<double, double>> GetSplineFromT(Func<double, double> XParametric, Func<double, double> YParametric)
            {
                Func<double, Tuple<double, double>> ParametricSpline = t =>
                {
                    //валидация входного параметра
                    if (t < 1 || t > n)
                        throw new ArgumentException("Параметр t должен лежать в диапазоне от 1 до n. " +
                                                    "n - количество точек, по которым строится сплайн");


                    //получаем x, y из параметрических уравнений
                    return Tuple.Create<double, double>(XParametric(t), YParametric(t));
                };

                return ParametricSpline;
            }

            private static Func<double, Tuple<double, double, double>> GetSplineFromT(Func<double, double> XParametric, Func<double, double> YParametric, Func<double, double> ZParametric)
            {
                Func<double, Tuple<double, double, double>> ParametricSpline = t =>
                {
                    //валидация входного параметра
                    if (t < 1 || t > n)
                        throw new ArgumentException("Параметр t должен лежать в диапазоне от 1 до n. " +
                                                    "n - количество точек, по которым строится сплайн");


                    //получаем x, y из параметрических уравнений
                    return Tuple.Create<double, double, double>(XParametric(t), YParametric(t), ZParametric(t));
                };

                return ParametricSpline;
            }
        }
    }
}
