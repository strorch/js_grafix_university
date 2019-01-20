class Utils 
{
    static initialize_spline_figure()
    {
        let figure = [];

        for (let i = 0; i < 4; i++) {
            let fig1 = []
            for (let j = 0; j < 4; j++) {
                fig1.push([i * 30, j * 30, Math.random()*30, 1]);
            }
            figure.push(fig1);
        }
        return figure;
    }
    
    static clear_window(context, canvas) {
        context.clearRect(0, 0, canvas.width, canvas.height);
    }
    
    static drawLine(context, f, s,color)
    {
        context.beginPath();
        context.moveTo(f[0] + 800 / 2, -(f[1]) + 500 / 2);
        context.lineTo(s[0] + 800 / 2, -(s[1]) + 500 / 2);
        context.strokeStyle = color;
        context.closePath();
        context.stroke();
    }

    static drawLine1(context, f, s,color)
    {
        context.beginPath();
        context.moveTo(f[0]*16 + 800 / 2, -(f[1]*10) + 500 / 2);
        context.lineTo(s[0]*16 + 800 / 2, -(s[1]*10) + 500 / 2);
        context.strokeStyle = color;
        context.closePath();
        context.stroke();
    }

    static drawAxiss(context, axis)
    {
        Utils.drawLine(context, axis[0], axis[1], '#ff0000');
        Utils.drawLine(context, axis[0], axis[2], '#00ff00');
        Utils.drawLine(context, axis[0], axis[3], '#0000ff');
    }
    static drawFigure(context, figure) {
        figure.forEach((element, index) => {
            if (index === figure.length - 1)
                return;
            Utils.drawLine(context,figure[index], figure[index+1], '#000000'); 
        });
    }

    static rotation_figure(context, line)
    {
        let rot = [
            [100, 10, 80,1],
            [10, 20, 90,1],
        ];
        
        // console.log(line1)
        // Utils.drawFigure(context, line1);

        
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
        // console.log(array_to_draw[0)

        let l1 = array_to_draw.length;
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
        }
    }
}
