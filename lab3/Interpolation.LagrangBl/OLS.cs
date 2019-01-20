using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMatrix;
using Gauss;

namespace Interpolation.InterpolationBL
{
    public static class OLS
    {
        //поля с префиксов m_ - member
        static int m_n;                         //количество точек
        static int m_matrixSize;                //размер для матриц и ветора b         
        static double m_min;                    //минимум по х
        static double m_max;                    //максимум по х
        static Func<double, double>[] m_fi;     //массив фи
        static double[] m_x;                    //массив х
        static double[] m_y;                    //массив х
        
        
        /// <summary>
        /// Строит функцию которая дает наилучшее приближение по МНК
        /// </summary>
        /// <param name="x">массив х (сетка)</param>
        /// <param name="y">массив у (значения у в сетке)</param>
        /// <param name="m">Степень полинома приближения (1 - линейная функия)</param>
        /// <returns></returns>
        public static Func<double, double> GetOLSPolinom(double[] x, double[] y, int m)
        {
            if (x.Length!=y.Length)
                throw new ArgumentException("Входные массивы должны иметь одинаковую длинну");
            
            //заполняем поля
            m_n = x.Length;
            m_min = x.Min();
            m_max = x.Max();
            m_x = x;
            m_y = y;

            GetFi(m+1);               //функции фи
            var gama = CalcGama();    //подсчет матрицы
            var b = Getb();           //правый столбец уравнений 

            LinearSystem LS = new LinearSystem(gama, b, 0.0000001, m+1);
            LS.Kv_k_Method();
            var roots = LS.XVector; //находим корни системы

            return BuildPolinom(roots, m);
        }

        //берется простая система линейно не зависимых фи в виде 1, x^2, x^3, x^4 и т.д.
        /// <summary>Возвращает функции фи для МНК</summary>
        /// <param name="quantity">Количество функций фи</param>
        static void GetFi(int quantity)
        {
            var fi = new Func<double, double>[quantity];

            for (int i = 0; i < quantity; i++)
                fi[i] = x => Math.Pow(x, i);

            m_fi = fi;
        }

        static double Fi(int index, double x)
        {
            return Math.Pow(x, index);
        }

        //потом нужно написать свой метод для симметрических матриц
        private static double[,] CalcGama()
        {
            int size = m_fi.Length;
            //var gama = ArrayMatrix.GetJaggedArray(size, size);

            var gama = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j < i)
                        continue;

                    double sum=0;
                    for (int k = 0; k < m_n; k++)
                    {
                        var a = Fi(i, m_x[k]);
                        var b = Fi(j, m_x[k]);
 
                        sum += a* b;
                    }
                    gama[j,i] = gama[i,j] = sum;
                }
            }

            return gama;
        }

        //подсчет вектора результатов для матрицы
        private static double[] Getb()
        {
            //разбираемся с размерностью b
            int length = m_fi.Length;
            double[] b = new double[length];

            //заполняем его
            for (int i = 0; i < length; i++)
            {
                double sum = 0;

                for (int j = 0; j < m_n; j++)
                    sum += Fi(i, m_x[j])* m_y[j];

                b[i] = sum;
            }

            return b;
        }

        private static Func<double, double> BuildPolinom(double[] roots , int m)
        {
            double min = m_min;
            double max = m_max;

            Func<double, double> polinom = x =>
            {
                if (x < min || min > max)
                    throw new ArgumentException("x должен лежать в пределах xMin, xMax");

                //roots - это тоже что и а по книге
                double sum = roots[0];

                for (int i = 1; i <= m; i++)
                    sum += roots[i] * Math.Pow(x, i);

                return sum;
            };

            return polinom;
        }

    }
}
