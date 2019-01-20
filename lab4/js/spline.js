class SplineCalculation
{
    static BsplinePoint(u, v, figures)
    {
        const n0 = u => {
            return Math.pow(1 - u, 3) / 6;
        }
        const n1 = u => {
            return (3* u*u*u - 6 * u*u + 4) / 6;
        }
        const n2 = u => {
            return (-3* u*u*u +3* u*u + 3 * u + 1) / 6;
        }
        const n3 = u => {
            return Math.pow(u, 3) / 6;
        }
        const n_arr = [n0, n1, n2, n3];
        let result = [0, 0, 0, 1];
        for(let i = 0; i < 4; i++) {
            for (let j = 0; j < 4; j++) {
                result[0] += n_arr[i](u) * n_arr[j](v) * figures[i][j][0];
                result[1] += n_arr[i](u) * n_arr[j](v) * figures[i][j][1];
                result[2] += n_arr[i](u) * n_arr[j](v) * figures[i][j][2];
            }
        }
        return result;
    }

    static CreateSpline(figures)
    {
        let new_arr = [];
        for (let i = 0.0; i <= 1; i += 0.05) {
            let small_arr = [];
            for (let j = 0.0; j <= 1; j += 0.05) {
                small_arr.push(SplineCalculation.BsplinePoint(i, j, figures));
            }
            new_arr.push(small_arr);
        }
        return new_arr;
    }

    static SplineToArr(spline)
    {
        let new_arr = [];
        spline.forEach(element => {
            element.forEach(x=>{
                new_arr.push(x);
            });
        });
        return new_arr;
    }

    static ArrToSpline(spline)
    {
        let new_arr = [];
        spline.forEach(element => {
            element.forEach(x=>{
                new_arr.push(x);
            });
        });
        return new_arr;
    }

    static draw_spline(context, new_arr)
    {
        for (let i = 0; i < 20; i++) {
            for (let j = 0; j < 20; j++) {
                if (j +1< 20)
                    Utils.drawLine1(context, Operations.projected(new_arr[i * 20 + j]), Operations.projected(new_arr[i*20 + j + 1]), "#ff0000");
                if (i +1< 20)
                    Utils.drawLine1(context, Operations.projected(new_arr[i * 20 + j]), Operations.projected(new_arr[(i+1) * 20 + j]), "#ff0000");
            }
        }
    }
}