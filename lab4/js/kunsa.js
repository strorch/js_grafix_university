class Kunsa
{
    static getUVCunsa(u, v, par)
    {
        const F1 = (t) => {
            return 2*t*t*t - 3*t*t + 1;
        };
        const F2 = (t) => {
            return -2*t*t*t + 3*t*t;
        };
        const F3 = (t) => {
            return t*t*t - 2*t*t + t;
        };
        const F4 = (t) => {
            return t*t*t - t*t;
        };
        const P_0_0 = [-100, 0,     100];
        const P_0_1 = [-100, -100,  -100];
        const P_1_0 = [ 100, -100,  -100];
        const P_1_1 = [ 100, 0,     -100];
        // const u = 0.5, v = 0.5;

        let FU = [F1(u), F2(u), F3(u), F4(u)];
        let FV = [[F1(u)], [F2(v)], [F3(v)], [F4(v)]];

        let arr = [
            [P_0_0[par], P_0_1[par], P_0_0[par], P_0_1[par]],
            [P_1_0[par], P_1_1[par], P_1_0[par], P_1_1[par]],
            [P_0_0[par], P_0_1[par], P_0_0[par], P_0_1[par]],
            [P_1_0[par], P_1_1[par], P_1_0[par], P_1_1[par]]
        ];

        let FU_m = math.matrix(FU);
        let FV_m = math.matrix(FV);
        let arr_m = math.matrix(arr);
        let f = math.multiply(FU_m, arr_m);
        let res = math.multiply(f, FV_m);
        return res.valueOf()[0];
    }

    static createCunsa()
    {
        let res = [];
        for (let i = 0; i <= 1; i += 0.05)
        {
            for (let j = 0; j <= 1; j += 0.05)
            {
                res.push([
                    Kunsa.getUVCunsa(i, j, 0),
                    Kunsa.getUVCunsa(i, j, 1),
                    Kunsa.getUVCunsa(i, j, 2),
                    1
                ]);
            }
        }
        return res;
    }

    static DrawKunsa(figure, context)
    {
        for (let i = 0; i < 20; i++) {
            for (let j = 0; j < 20; j++) {
                if (j +1< 20)
                    Utils.drawLine(context, Operations.projected(figure[i * 20 + j]), Operations.projected(figure[i*20 + j + 1]), "#ff0000");
                if (i +1< 20)
                    Utils.drawLine(context, Operations.projected(figure[i * 20 + j]), Operations.projected(figure[(i+1) * 20 + j]), "#ff0000");
            }
        }
    }
}