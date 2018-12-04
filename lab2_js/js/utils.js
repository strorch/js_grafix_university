class Utils 
{
    static initialize_figure()
    {
        let figure = [];

        for (let i = 0; i < 6; i++) {
            for (let j = 0; j < 10; j++) {
                figure.push([i * 30, j * 30, Math.random()*15, 1]);
            }
        }
        return figure;
    }
    
    static clear_window(context, canvas) {
        context.clearRect(0, 0, canvas.width, canvas.height);
    }
 
    static drawFigure(context, figure) {
        for (let i = 0; i < 6; i++)
        {
            for (let j = 0; j < 10; j++)
            {
                if (j + 1 < 10)
                    Utils.drawLine(context,figure[i * 10 + j], figure[i * 10 + j + 1], '#000000');
                if (i + 1 < 6)
                    Utils.drawLine(context,figure[i * 10 + j], figure[(i  + 1) * 10 + j], '#000000');
            }
        }
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

    static drawAxiss(context, axis)
    {
        Utils.drawLine(context, axis[0], axis[1], '#ff0000');
        Utils.drawLine(context, axis[0], axis[2], '#00ff00');
        Utils.drawLine(context, axis[0], axis[3], '#0000ff');
    }
}
