using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Brackets;

namespace Interpolation.InterpolationBL
{
    public static class InterpolationWork
    {
        ///Интерполяционный полином Лагранжа
        ///Разбит на две ф-и для избежания повторения кода
        ///В этом методе создастся только равномерная сетка и вызовется общая часть
        public static Func<double, double> Lagrang(Func<double, double> function, double a, double b, int N)
        {
            double[] x = new double[N];
        
            double h = (b - a) / (N - 1);
            for (int i = 0; i < N; i++)
                x[i] = a + i * h;
        
            return CommonPartOfInterpolation(function, N, x);
        }

        public static Func<double, double> Lagrang(double[] x, double[] f)
        {
            ParameterExpression xParam = Expression.Parameter(typeof(double), "x");
            ConstantExpression NullConst = Expression.Constant(0.0);
            ConstantExpression OneConst = Expression.Constant(1.0);


            Expression resultExpression = Expression.Add(NullConst, NullConst);

            for (int i = 0; i < x.Length; i++)
            {
                Expression chisl = Expression.Add(OneConst, NullConst);
                //Expression znam = Expression.Add(NullConst, NullConst);
                double znam = 1;

                for (int j = 0; j < x.Length; j++)
                {
                    if (i == j)
                        continue;

                    ConstantExpression SecondParam = Expression.Constant(x[j]);
                    Expression skobka = Expression.Subtract(xParam, SecondParam);
                    chisl = Expression.Multiply(chisl, skobka);

                    znam *= (x[i] - x[j]);
                }
                ConstantExpression ZnamConst = Expression.Constant(znam);
                ConstantExpression f_value = Expression.Constant(f[i]);

                Expression ChislMultFunc = Expression.Multiply(chisl, f_value);
                Expression L = Expression.Divide(ChislMultFunc, ZnamConst);

                resultExpression = Expression.Add(resultExpression, L);
            }

            LambdaExpression lyambdaExpression = Expression.Lambda(resultExpression, xParam);
            var InterpolationLyambda = (Func<double, double>)lyambdaExpression.Compile();

            return InterpolationLyambda;
        }

        //БАЛОВСТВО
       // public static Func<double, double> Lagrang(Func<double, double> function, double a, double b, int N)
       // {
       //     Fraction[] x = new Fraction[N];
       // 
       //     Fraction h = new Fraction{numerator  = (b - a) , denominator =  (N - 1)};
       //     for (int i = 0; i < N; i++)
       //         x[i] = a +  h*i;
       // 
       //     return CommonPartOfInterpolation(function, N, x);
       // }

        //Интерполирование по узлам Чебышева
        public static Func<double, double> Chebyshev(Func<double, double> function, double a, double b, int n)
        {
            double[] x = new double[n];

            for (int i = 1; i < n+1; i++)
                x[i-1] = 0.5 * (b + a) + 0.5*(b-a)*Math.Cos((2 * i - 1) * Math.PI / (2 * n ));

            return CommonPartOfInterpolation(function, n, x);
        }

        ///Общая часть для полинома Лагранжа и Интерполирования по узлам Чебышева
        private static Func<double, double> CommonPartOfInterpolation(Func<double, double> function, int N, double[] x)
        {
            double[] f = x.Select(v => function(v)).ToArray();

            ParameterExpression xParam = Expression.Parameter(typeof(double), "x");
            ConstantExpression NullConst = Expression.Constant(0.0);
            ConstantExpression OneConst = Expression.Constant(1.0);


            Expression resultExpression = Expression.Add(NullConst, NullConst);

            for (int i = 0; i < N; i++)
            {
                Expression chisl = Expression.Add(OneConst, NullConst);
                //Expression znam = Expression.Add(NullConst, NullConst);
                double znam = 1;

                for (int j = 0; j < N; j++)
                {
                    if (i == j)
                        continue;

                    ConstantExpression SecondParam = Expression.Constant(x[j]);
                    Expression skobka = Expression.Subtract(xParam, SecondParam);
                    chisl = Expression.Multiply(chisl, skobka);

                    znam *= (x[i] - x[j]);
                }
                ConstantExpression ZnamConst = Expression.Constant(znam);
                ConstantExpression f_value = Expression.Constant(f[i]);

                Expression ChislMultFunc = Expression.Multiply(chisl, f_value);
                Expression L = Expression.Divide(ChislMultFunc, ZnamConst);

                resultExpression = Expression.Add(resultExpression, L);
            }

            LambdaExpression lyambdaExpression = Expression.Lambda(resultExpression, xParam);
            var InterpolationLyambda = (Func<double, double>)lyambdaExpression.Compile();

            return InterpolationLyambda;
        }

        //Лагранж с использованием моего класса
        /*
        public static Func<double, double> LagrangWithMyOwnVariables(Func<double, double> function, double a, double b, int N)
        {
            #region
            double[] x = new double[N];

            double h = (b - a) / (N - 1);
            for (int i = 0; i < N; i++)
                x[i] = a + i * h;

            double[] f = x.Select(v => function(v)).ToArray();
            #endregion

            Bracket resultExpression = new Bracket (){ c = 0 , Xlist = new List<Variable>()};
            Variable xVar = new Variable();

            for (int i = 0; i < N; i++)
            {
                double znam = 1;
                Bracket chisl = new Bracket() { c = 1, Xlist = new List<Variable>() };

                for (int j = 0; j < N; j++)
                {
                    if (i == j)
                        continue;

                    chisl*=(xVar - x[j]);
                
                    znam *= (x[i] - x[j]);
                }

                resultExpression += (f[i]/znam)*chisl;
            }

            return resultExpression.Compile();
        }
        */
        
       //Лагранж с использованием моего класса переменных и дробей
       public static Func<double, double> LagrangWithMyOwnVariablesAndFractions(Func<double, double> function, double a, double b, int N)
        {

            Fraction[] x = new Fraction[N];
            Fraction afr = new Fraction(a); 

            Fraction h = new Fraction{numerator  = (b - a) , denominator =  (N - 1)};
            for (int i = 0; i < N; i++)
                x[i] = afr +  h*i;

            Fraction[] f = x.Select(v => new Fraction(function(v.Value))).ToArray();
           

            Bracket resultExpression = Bracket.Null;
            Variable xVar = new Variable();

            for (int i = 0; i < N; i++)
            {
                Fraction znam = Fraction.One;
                Bracket chisl = Bracket.One;

                for (int j = 0; j < N; j++)
                {
                    if (i == j)
                        continue;

                    chisl *= (xVar - x[j]);
                    
                    znam *= (x[i] - x[j]);
                }

                resultExpression += (f[i] / znam) * chisl;
            }

            return resultExpression.Compile();
        }    
    }
}
