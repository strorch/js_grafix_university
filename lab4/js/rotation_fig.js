class Rotation
{
    static createRotation()
    {
        const line =  [
            [0,     10,    10,      1],
            [0,     20,    20,      1],
            [0,     100,   100,     1],
            [0,     150,   200,     1]
        ];
        const rot = [100, 0, 10, 1];
        
        let array_to_draw = [];
        for (let i = 0; i < line.length; i++)
        {
            let each_arr = [];
            for (let j = 0; j < 360; j += 30)
            {
                each_arr.push(Operations.line_rotate(line, j * Math.PI/180, rot));
            }
            array_to_draw.push(each_arr);
        }
        let result = [];
        for (let i = 0; i < array_to_draw.length; i++)
        {
            for (let j = 0; j < array_to_draw[i].length; j ++)
            {
                for (let k = 0; k < array_to_draw[i][j].length; k++)
                {
                    result.push(array_to_draw[i][j][k]);
                }
            }
        }
        return result;
    }

    static DrawRotation(figure, context)
    {
        let new_arr= [];
        for (let i = 0; i < 4; i++)
        {
            new_arr[i] = [];
            for (let j = 0; j < 12; j ++)
            {
                new_arr[i][j] = [];
                for (let k = 0; k < 4; k++)
                {
                    new_arr[i][j].push(figure[i* 12*4 + j*4 + k]);
                }
            }
        }
        for (let j = 0; j < 12; j++)
        {
            for (let k = 0; k < 4; k++)
            {
                for (let i = 0; i < 4; i++)
                {
                    if (j + 1 !== 12)
                        Utils.drawLine(context, Operations.projected(new_arr[i][j][k]), Operations.projected(new_arr[i][j + 1][k]), '#000000');
                    if (k + 1 !== 4)
                        Utils.drawLine(context, Operations.projected(new_arr[i][j][k]), Operations.projected(new_arr[i][j][k + 1]), '#000000');
                    if (j + 1 === 12)
                        Utils.drawLine(context, Operations.projected(new_arr[i][0][k]), Operations.projected(new_arr[i][11][k]), '#000000');
                    if (k + 1 === 4)
                        Utils.drawLine(context, Operations.projected(new_arr[i][j][0]), Operations.projected(new_arr[i][j][3]), '#000000');
                }
            }
        }
    }
}