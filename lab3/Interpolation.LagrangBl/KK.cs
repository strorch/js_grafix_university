using System;
using System.Collections;
using System.Data;

namespace Gauss
{
    public class SolutionNotFound : Exception
    {
        public SolutionNotFound(string msg)
            : base("Рішення не можу бути найдено: \r\n" + msg)
        {
        }
    }

    public class LinearSystem
    {
        private double[,] a_matrix;
        private double[] x_vector;
        private double[] y_vector;
        private double[] b_vector;
        private double Eps;
        private int Z;
        private int size;

        public LinearSystem(double[,] a_matrix, double[] b_vector, double Eps, int Z)
            : this(a_matrix, b_vector, Z, Eps)
        {
        }
        public LinearSystem(double[,] a_matrix, double[] b_vector, int Z, double Eps)
        {
            if (a_matrix == null || b_vector == null)
                throw new ArgumentNullException("Один з параметрів рівний 0.");

            int b_length = b_vector.Length;
            int a_length = a_matrix.Length;
            if (a_length != b_length * b_length)
                throw new ArgumentException("к-сть стовпців і рядків");

            this.a_matrix = a_matrix;
            this.b_vector = b_vector;
            this.x_vector = new double[b_length];
            this.y_vector = new double[b_length];
            this.size = b_length;
            this.Eps = Eps;
            this.Z = Z;

        }
        public void Kv_K_Method()
        {
            int N = size;
            double[,] S = new double[N, N];//верхняя треугольная
            double[] D = new double[N];
            //1 stroka
            S[0, 0] = Math.Sqrt(Math.Abs(a_matrix[0, 0]));
            D[0] = Math.Sign(S[0, 0]);
            for (int i = 1; i < N; i++)
            {
                S[0, i] = a_matrix[0, i] / (S[0, 0] * D[0]);
            }
            //2 stroka
            double s = 0;
            for (int i = 0; i < N; i++)
            {
                s = 0;
                for (int l = 0; l <= i - 1; l++)
                    s += S[l, i] * S[l, i] * D[l];
                D[i] = Math.Sign(a_matrix[i, i] - s);
            }
            //3 stroka
            double sum = 0;
            for (int i = 1; i < N; i++)
            {
                sum = 0;
                for (int k = 0; k <= i - 1; k++)
                {
                    sum += Math.Pow(S[k, i], 2) * D[k];
                }
                S[i, i] = Math.Sqrt(Math.Abs(a_matrix[i, i] - sum));
                for (int j = i + 1; j < N; j++)
                {
                    sum = 0;
                    for (int k = 0; k <= i - 1; k++)
                    {
                        sum += S[k, i] * S[k, j] * D[k];/*S[k,i]*/
                    }
                    S[i, j] = (a_matrix[i, j] - sum) / (S[i, i] * D[i]);
                }
            }
            //4

            y_vector[0] = b_vector[0] / (S[0, 0] * D[0]);
            for (int i = 1; i < N; i++)
            {
                sum = 0;
                for (int k = 0; k <= i - 1; k++)
                {
                    sum += S[k, i] * y_vector[k] * D[k];/*S[k,i]*/
                }
                y_vector[i] = (b_vector[i] - sum) / (S[i, i] * D[i]);
            }

            x_vector[N - 1] = y_vector[N - 1] / S[N - 1, N - 1];
            for (int i = N - 2; i >= 0; i--)
            {
                sum = 0;
                for (int k = i + 1; k < N; k++)
                {
                    sum += S[i, k] * x_vector[k];
                }
                x_vector[i] = (y_vector[i] - sum) / S[i, i];
            }
        }
        ////////////////////////////////////////конец
        public double[] XVector
        {
            get
            {
                return x_vector;
            }
        }
        public double[] YVector
        {
            get
            {
                return y_vector;
            }
        }
        #region add
        public void Kv_k_Method()
        {
            Method(ref a_matrix, ref b_vector);
        }
        private void Method(ref double[,] m, ref double[] d)
        {
            int N = size;
            double[] SumEps = new double[N];
            double[] SumS;
            SumS = new double[N];
            double[] ServiseArr = new double[N];
            double Serv = 0;
            SumS = Sum(m, d);
            for (int i = 0; i < N; i++)
            {
                double C = m[i, i];
                ServiseArr = new double[N]; Serv = 0;
                for (int j = i; j < N; j++)
                {
                    m[i, j] = (m[i, j] / C);
                    ServiseArr[j] = m[i, j];
                }
                d[i] = (d[i] / C); Serv = d[i];
                SumS[i] = (SumS[i] / C);
                for (int cn = i + 1; cn < N; cn++)
                {
                    C = m[cn, i];
                    for (int j = i; j < N; j++)
                    {
                        ServiseArr[j] = (m[i, j] * C);
                        m[cn, j] = (m[cn, j] - ServiseArr[j]);
                    }
                    Serv = (d[i] * C); d[cn] = (d[cn] - Serv);
                    SumS[cn] = (SumS[cn] - Serv);
                }
            }
            double ServA;
            for (int i = N - 1; i >= 0; i--)
            {
                double C = m[i, i];
                m[i, i] = m[i, i] / C;
                d[i] = (d[i] / C);
                SumS[i] = SumS[i] / C;
                for (int cn = i - 1; cn >= 0; cn--)
                {
                    ServA = m[i, i]; Serv = d[i]; C = m[cn, i]; ServA = ServA * C;
                    m[cn, i] = m[cn, i] - ServA; Serv = Serv * C;
                    d[cn] = d[cn] - Serv; SumS[cn] = SumS[cn] - Serv;
                }
            }
            SumEps = Sum(m, d);
            double[] nev = new double[N];
            for (int y = 0; y < N; y++)
                x_vector[y] = d[y];
        }
        private double[] Sum(double[,] m, double[] d)
        {
            int N = size;
            double[] Sum = new double[N];
            for (int i = 0; i < N; i++)
            {
                Sum[i] = 0;
                for (int j = 0; j < N; j++)
                    Sum[i] += m[i, j];
                Sum[i] += d[i];
            }
            return Sum;
        }
        #endregion
    }

}

