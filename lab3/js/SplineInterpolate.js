class SplineInterpolation
{
    //единственная публичная ф-я, которая возвращает сплайн - основная
    static Func<double, double> GetSplineInterpolant(double[] xValues, double[] fValues, bool NeedSort = false)
    {
        if (xValues.Length != fValues.Length)
            throw new ArgumentException("Входные массивы должны быть одинаковой длинны");

        //длинна массивов - вынес сюда чтобы передавать в ф-и и экономить по одной строке ;)
        int n = xValues.Length;

        //если нужна сортировка проводим ее - при этом портятся входные массивы
        if (NeedSort)
            SortPointsByX(xValues, fValues, n);

        //подсчет h для всех иксов
        double[] h = Calc_h(xValues, n);

        //получение кортежа массивов для метода прогонки, по сути три диагонали 
        //(Та, что под главной, ГЛАВНАЯ, та что над главной) еще можно сказть A, C, B
        Tuple<double[], double[], double[]> ArrayTuple = GetACBTuple(h, n);

        //столбец f для решения матрицы
        double[] f = GetF(fValues, h, n);

        //решием систему и находим c, преобразование в список для добавления первого и последнего С=0
        List<double> c= TridiagonalMatrixSolving.Normal(ArrayTuple.Item1, ArrayTuple.Item2, ArrayTuple.Item3, f).ToList();

        //добавление c0=0 и cn = 0
        c.Insert(0, 0.0);
        c.Add(0.0);

        //подсчет d
        double[] d = Calc_d(c, h, n);

        //подсчет b
        double[] b = Calc_b(c, d, h, fValues,n);

        //fValues - то же что и а
        return BuildSpline(fValues, b, c, d ,xValues);
    }

    //сортировка по иксу
    private static void SortPointsByX(double[] xValues, double[] fValues, int n)
    {
        //создаем массив размера n кортежей из двух вещественных числел  для связывания  x и y
        Tuple<double, double>[] tuple = new Tuple<double, double>[n];

        for (int i = 0; i < n; i++)
            tuple[i] = Tuple.Create<double, double>(xValues[i], fValues[i]);

        //сортируем
        tuple =  tuple.OrderBy(t => t.Item1).ToArray();

        //заносим во входные массивы значения после сортировки
        for (int i = 0; i < n; i++)
        {
            var t = tuple[i];

            xValues[i] =t.Item1;
            fValues[i] = t.Item2;
        }
    }

    //ф-я занимается подсчетом h для всех x
    private static double[] Calc_h(double[] xValues, int n)
    {
        var hArray = new double[n];

        //от i-го икса отнимаем i-1 - ый
        for (int i = 1; i < n; i++)
            hArray[i] = xValues[i] - xValues[i - 1];

        return hArray;
    }
    
    //получение кортежей массивов для метода прогонки
    private static Tuple<double[], double[], double[]> GetACBTuple(double[] h, int n)
    {
        //три диагонали 
        var A = new double[n-2];        //n-2  потому что c0 = cn = 0
        var C = new double[n-2];        //то есть 2 с нам уже известно
        var B = new double[n-2];

        for (int i = 1; i < (n-1); i++)
        {
            C[i-1] = 2*(h[i] + h[i + 1]);     //Смещение С[i-1] из-за того, что непозволительно пропускать первый элемент в C
                                                //потому что это будет передаваться в нормальный метод прогонки
            
            if (i != 1)
                A[i-1] = h[i];

            if (i != n - 2)
                B[i-1] = h[i + 1];

        }

        return new Tuple<double[], double[], double[]>(A, C, B);
    }

    //подсчет f столбца для матрицы
    private static double[] GetF(double[] fValues, double[] h, int n)
    {
            /*
            *В этой функции:
            *f        - вектор столбец свободных членов матрицы 
            *fvalues  - значения ф-и в i-тых точках - в книге это просто f-iтые
            */
        double[] f = new double[n-2];

        for (int i = 1; i < (n - 1); i++)
            f[i - 1] = 6 * ((fValues[i + 1] - fValues[i]) / h[i + 1] - (fValues[i] - fValues[i-1]) / h[i]);

        return f;
    }

    //подсчет d для сплайна
    private static double[] Calc_d(List<double> c, double[] h, int n)
    {
        double[] d = new double[n];

        for (int i = 1; i < n; i++)
            d[i] = (c[i] - c[i - 1]) / h[i];

        return d;
    }

    //подсчет b
    private static double[] Calc_b(List<double> c, double[] d, double[] h, double[] fValues, int n)
    {
        double[] b = new double[n];

        for (int i = 1; i < n; i++)
            b[i] = 0.5*h[i]*c[i]-Math.Pow(h[i],2)*d[i]/6+(fValues[i]-fValues[i-1])/h[i];

        return b;
    }

    //строим сплайн из всего готового
    private static Func<double, double> BuildSpline(double[] fValues, double[] b, List<double> c, double[] d, double[] xValues)
    {
        Func<double, double> spline = x =>
        {
            double xMIn = xValues.Min();
            double xMax = xValues.Max();

            if (x<xMIn||x>xMax)
                throw new ArgumentException("Входной параметр должен лежать между минимальным и максимальным значением по х", "x");

            //находим нужный полином в сплайне
            int i = 1;
            while (x > xValues[i])
                i++;

            //подставляем значения
            double result = 0;

            result += fValues[i];
            result += b[i] * (x - xValues[i]);
            result += 0.5 * c[i] * Math.Pow(x - xValues[i], 2);
            result += d[i] *Math.Pow(x - xValues[i], 3)/6;

            return result;
        };

        return spline;
    }
}

//класс интерполирования параметрическим сплайном
public static class ParametricSplineInterpolation
{
    //длинна массивов
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

    //возвращает параметрический сплайн для x,y
    private static Func<double, double> GetParametricSpline(double[] InputValues)
    {
        //список для значений переменной t
        //для параметрического сплайна (для x и y) t будет лежать на оси абсцис 
        List<double> tValues = new List<double>();

        for (int i = 1; i <= ParametricSplineInterpolation.n; i++)
            tValues.Add(i);

        //строим используя обычную интерполяцию сплайнами
        return SplineInterpolation.GetSplineInterpolant(tValues.ToArray(), InputValues);
    }

    //возвращает делегат, который зааисит от t
    //а сам делегат по входному t возвращает кортеж (x, y) 
    private static Func<double, Tuple<double, double>> GetSplineFromT(Func<double, double> XParametric, Func<double, double> YParametric)
    {
        Func<double, Tuple<double, double>> ParametricSpline = t => 
        {
            //валидация входного параметра
            if (t < 1 || t > n)
                throw new ArgumentException("Параметр t должен лежать в диапазоне от 1 до n. "+ 
                                            "n - количество точек, по которым строится сплайн");


            //получаем x, y из параметрических уравнений
            return Tuple.Create<double, double>(XParametric(t), YParametric(t));
        };

        return ParametricSpline;
    }
}

public static class CyclicSplineInterpolation
{
    //длинна массивов
    static int n;

    //единственная публичная ф-я, которая возвращает сплайн (циклический)
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

    //возвращает параметрический сплайн для x,y
    private static Func<double, double> GetParametricSpline(double[] InputValues)
    {
        //список для значений переменной t
        //для параметрического сплайна (для x и y) t будет лежать на оси абсцис 
        List<double> tValues = new List<double>();

        for (int i = 1; i <= CyclicSplineInterpolation.n; i++)
            tValues.Add(i);

        //строим используя обычную интерполяцию сплайнами
        return SplineInterpolationForCyclic.GetSplineInterpolant(tValues.ToArray(), InputValues);
    }

    //возвращает делегат, который зааисит от t
    //а сам делегат по входному t возвращает кортеж (x, y) 
    private static Func<double, Tuple<double, double>> GetSplineFromT(Func<double, double> XParametric, Func<double, double> YParametric)
    {
        Func<double, Tuple<double, double>> ParametricSpline = t =>
        {
            //валидация входного параметра
            if (t < 0 || t > n)
                throw new ArgumentException("Параметр t должен лежать в диапазоне от 0 до n. " +
                                            "n - количество точек, по которым строится сплайн");
            
            //получаем x, y из параметрических уравнений

            return Tuple.Create<double, double>(XParametric(t), YParametric(t));
        };

        return ParametricSpline;
    }
}

public static class SplineInterpolationForCyclic
{
    static int N;

    static int ind(int val)
    {
        if (val >= N)
            return val % N;

        else if (val < 0)
            return val + N;
        else
            return val; 
    }

    //единственная публичная ф-я, которая возвращает сплайн
    public static Func<double, double> GetSplineInterpolant(double[] xValues, double[] fValues)
    {
        if (xValues.Length != fValues.Length)
            throw new ArgumentException("Входные массивы должны быть одинаковой длинны");

        //длинна массивов - вынес сюда чтобы передавать в ф-и и экономить по одной строке ;)
        int n = xValues.Length;
        SplineInterpolationForCyclic.N = n;

        //подсчет h для всех иксов
        //double[] h = Calc_h(xValues, n);

        //получение кортежа массивов для метода прогонки, по сути три диагонали 
        //(Та, что под главной, ГЛАВНАЯ, та что над главной) еще можно сказть A, C, B
        Tuple<double[], double[], double[]> ArrayTuple = GetACBTuple(n);

        //столбец f для решения матрицы
        double[] f = GetF(fValues, n);

        //решием систему и находим c, преобразование в список для добавления первого и последнего С=0
        List<double> c = TridiagonalMatrixSolving.CyclycWithoutMinuses(ArrayTuple.Item1, ArrayTuple.Item2, ArrayTuple.Item3, f).ToList();

        //добавление c0=0 и cn = 0
        //c.Insert(0, 0.0);
        //c.Add(0.0);

        //подсчет d
        double[] d = Calc_d(c, n);

        //подсчет b
        double[] b = Calc_b(c, d, fValues, n);

        //fValues - то же что и а
        return BuildSpline(fValues, b, c, d, xValues);
    }

    //ф-я занимается подсчетом h для всех x
    private static double[] Calc_h(double[] xValues, int n)
    {
        var hArray = new double[n];

        // hArray[0] = xValues[0]-xValues[n-1];

        //от i-го икса отнимаем i-1 - ый
        for (int i = 1; i < n; i++)
            hArray[i] = xValues[i] - xValues[i - 1];

        return hArray;
    }

    //получение кортежей массивов для метода прогонки
    private static Tuple<double[], double[], double[]> GetACBTuple(int n)
    {
        //три диагонали 
        var A = new double[n];        //n-2  потому что c0 = cn = 0
        var C = new double[n];        //то есть 2 с нам уже известно
        var B = new double[n];

        for (int i = 0; i < n ; i++)
        {
            A[i] = 1;
            
            B[i] = 1;
            C[i] = 4;
        }

        return new Tuple<double[], double[], double[]>(A, C, B);
    }

    //подсчет f столбца для матрицы
    private static double[] GetF(double[] fValues, int n)
    {
        /*
            *В этой функции:
            *f        - вектор столбец свободных членов матрицы 
            *fvalues  - значения ф-и в i-тых точках - в книге это просто f-iтые
        */
        double[] f = new double[n];

    
        for (int i = 1; i < n-1; i++)
            f[i] = 6 * ((fValues[i + 1] - fValues[i])  - (fValues[i] - fValues[i - 1]));

        f[0] = 6 * ((fValues[1] - fValues[0]) - (fValues[0] - fValues[n - 1]));
        f[n - 1] = 6*((fValues[0] - fValues[n - 1]) - (fValues[n - 1] - fValues[n - 2]));

        return f;
    }

    //подсчет d для сплайна
    private static double[] Calc_d(List<double> c, int n)
    {
        double[] d = new double[n];

        for (int i = 1; i < n; i++)
            d[i] = (c[i] - c[i - 1]);

        d[0] = c[0] - c[n - 1];

        return d;
    }

    //подсчет b
    private static double[] Calc_b(List<double> c, double[] d, double[] fValues, int n)
    {
        double[] b = new double[n];

        for (int i = 1; i < n; i++)
            b[i] = 0.5 * c[i] -  d[i] / 6 + (fValues[i] - fValues[i - 1]);

        b[0] = 0.5 * c[0] - d[0] / 6 + (fValues[0] - fValues[n - 1]);
        
        return b;
    }

    //строим сплайн из всего готового
    private static Func<double, double> BuildSpline(double[] fValues, double[] b, List<double> c, double[] d, double[] xValues)
    {
        Func<double, double> spline = x =>
        {
            double xMIn = xValues.Min();
            double xMax = xValues.Max();

            //if (x < xMIn || x > xMax)
            //    throw new ArgumentException("Входной параметр должен лежать между минимальным и максимальным значением по х", "x");

            
            //находим нужный полином в сплайне
            int i = 0;
            while (x > xValues[i])
                i++;

            //подставляем значения
            double result = 0;

            result += fValues[i];
            result += b[i] * (x - xValues[i]);
            result += 0.5 * c[i] * Math.Pow(x - xValues[i], 2);
            result += d[i] * Math.Pow(x - xValues[i], 3) / 6;

            return result;
        };

        return spline;
    }
}
