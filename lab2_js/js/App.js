class App 
{
    /*

    триметрія, кабінетна, триточкова перспективна

    обертання відносно рухомої осі, паралельна Оу і пересувається z2 + x2 = R2 
    
    */
    constructor()
    {
        this.figure = App.initialize_figure();
        this.axisMap = [
            [0, 0 , 0],
            [0, 100, 0],
            [100, 0, 0],
            [0, 0, 100],
        ];
    }

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
    draw_field(context, canvas, angle, axis)
    {
        Utils.clear_window(context, canvas);
        this.figure = Operations.rotate_ort(this.figure, angle, axis)
        Utils.drawAxiss(context, this.axisMap);
        App.drawFigure(context, this.figure);
    }

    init()
    {
        let canvas = document.getElementById("myCanvas");
        let context = canvas.getContext("2d");

        Utils.clear_window(context, canvas);
        this.draw_field(context, canvas, 1, 'x');
        this.draw_field(context, canvas, -1, 'y');
        this.draw_field(context, canvas, 1, 'z');

        document.addEventListener('keydown', (e) => {
            let key = e.key;
            if (key == 'w') {
                this.draw_field(context, canvas, 0.2, 'y');
            }
            else if (key == 's') {
                this.draw_field(context, canvas, -0.2, 'y');
            }
            else if (key == 'a') {
                this.draw_field(context, canvas, 0.2, 'x');
            }
            else if (key == 'd') {
                this.draw_field(context, canvas, -0.2, 'x');
            }
            else if (key == 'q') {
                this.draw_field(context, canvas, 0.2, 'z');
            }
            else if (key == 'e') {
                this.draw_field(context, canvas, -0.2, 'z');
            }
            else
                return;
        });
    }
}