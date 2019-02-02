class Utils 
{
    static initialize_spline_figure()
    {
        let figure = [];

        for (let i = 0; i < 4; i++) {
            let fig1 = []
            for (let j = 0; j < 4; j++) {
                if (i === 2 && j === 2) {
                    fig1.push([i * 30, j * 30, 50, 1]);
                }
                else
                    fig1.push([i * 30, j * 30, 1, 1]);
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
}
