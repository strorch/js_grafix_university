class Utils 
{
    static initialize_figure(xyz)
    {
        let figure = [
        ];
        for(let t = 1; t < 5; t+= 0.2)
        {
            figure.push([xyz.x(t),xyz.y(t),1,1])
        }
        return figure;
    }

    static clear_window(context, canvas) {
        context.clearRect(0, 0, canvas.width, canvas.height);
    }
 
    static drawFigure(context, figure) {
        figure.forEach((element, index) => {
            if (index === figure.length - 1)
                return;

            if (index === 25)
                Utils.drawLine(context,figure[index], figure[index+1], '#00ff00');
            else 
                Utils.drawLine(context,figure[index], figure[index+1], '#000000'); 
        });
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
