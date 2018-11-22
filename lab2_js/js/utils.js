class Utils 
{
    static clear_window(context, canvas) {
        context.clearRect(0, 0, canvas.width, canvas.height);
    }
    
    static drawFigure(context, figure) {
        let i = -1;
        while (++i < 6)
        {
            let j = -1;
            while (++j < 10)
            {
                if (j + 1 < 10)
                    Utils.drawLine(context,figure[i * 6 + j], figure[i * 6 + j + 1]);
                if (i + 1 < 6)
                    Utils.drawLine(context,figure[i * 6 + j], figure[(i  + 1) * 6 + j]);
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
