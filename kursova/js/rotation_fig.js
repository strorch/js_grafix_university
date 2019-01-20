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
        console.log(array_to_draw);
        // console.log(array_to_draw[0)

        /*let l1 = array_to_draw.length;
        let l2 = array_to_draw[0].length;
        let l3 = array_to_draw[0][0].length;
        // console.log(array_to_draw);
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
        
       for (let i = 0; i < array_to_draw.length; i++)
        {
            for (let j = 0; j < array_to_draw[i].length; j ++)
            {
                for (let k = 0; k < array_to_draw[i][j].length; k++)
                {
                    if (k + 1 === array_to_draw[i][j].length)
                       break;
                    Utils.drawLine(context, array_to_draw[i][j][k], array_to_draw[i][j][k+1], '#000000')

                }
            }
        }*/
    }
}