class Utils 
{
    static clear_window(context, canvas) {
        context.clearRect(0, 0, canvas.width, canvas.height);
    }
    
    static drawFigure(context, figure, canvas) {
    
        context.clearRect(0, 0, canvas.width, canvas.height);
        context.beginPath();
        context.moveTo(figure[0][0] + 800 / 2, -figure[0][1] + 500 / 2);
        
        for (let i = 0; i < figure.length - 1; i++)
            context.lineTo(figure[i + 1][0] + 800 / 2, -figure[i + 1][1] + 500 / 2);
    
        context.lineTo(figure[0][0] + 800 / 2, -figure[0][1] + 500 / 2);
        context.closePath();
        context.stroke();
    }
    
    
    static drawLine()
    {
        let canvas = document.getElementById("myCanvas");
        let context = canvas.getContext("2d");
        drawFigure(context, figure, canvas);
    
        let a = parseFloat(document.getElementById('a').value);
        let b = parseFloat(document.getElementById('b').value);
    
        let s= [-500 , -500 * a + b];
        let f= [500, 500 * a + b];
        context.beginPath();
        context.moveTo(f[0] + 800 / 2, -(f[1]) + 500 / 2);
        context.lineTo(s[0] + 800 / 2, -(s[1]) + 500 / 2);
        context.closePath();
        context.stroke();
    }
}
