class Bezier
{
    static GetBezier(points)
    {
        let points_array = [];
        for (let t = 0; t <= 1; t += 0.02)
        {
            let p = [0, 0, 1, 1];
            for (let k = 0; k < points.length; k++)
            {
                p[0] += points[k][0] * Bezier.Bkn(t, points.length-1, k);
                p[1] += points[k][1] * Bezier.Bkn(t, points.length-1, k);
            }
                
            points_array.push(p);
        }
        return points_array;
    }

    static Bkn(t, n, k)
    {
        return Bezier.nk(n, k) * Math.pow(t, k) * Math.pow(1 - t, n - k);
    }

    static nk(n, k)
    {
        const factorial = n => {
            let result = 1;
            for (let i = 1; i <= n; i++)
                result *= i;
            return result;
        };
        return factorial(n) / ( factorial(k) * factorial(n - k));
    }
}