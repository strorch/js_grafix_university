class Rotation
{
    static createRotation()
    {
        const line =  [
            [20, 20, 0,1],
            [40, 75, 0,1],
            [100, 50, 0,1],
            [160, 100, 0,1],
            // [150, 150, 0,1]
        ];
        const rot = [
            [100, 10, 80,1],
            [10, 20, 90,1],
        ];
        
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
        // console.log(result);
        return result;
    }

    static DrawRotation(figure, context)
    {
        console.log(figure);
        /*for (let i = 0; i < figure.length; i++)
        {
            if (i + 1 === figure.length)
                Utils.drawLine(context, Operations.projected(figure[i]), Operations.projected(figure[i + 1]), '#ffffff');
        }*/
    }
}