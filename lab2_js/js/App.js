class App 
{
    /*

    триметрія, кабінетна, триточкова перспективна

    обертання відносно рухомої осі, паралельна Оу і пересувається z2 + x2 = R2 
    
    */
    constructor()
    {
        this.figure = Utils.initialize_figure();
        this.projection = 0;
        this.moving = 2;
        this.axisMap = [
            [0, 0 , 0, 1],
            [0, 100, 0, 1],
            [100, 0, 0, 1],
            [0, 0, 100, 1],
        ];
    }

    draw_field(context, canvas, key, handler)
    {
        if (key == 'w') {
            this.figure = handler(this.figure, 0.2, 'y')
        }
        else if (key == 's') {
            this.figure = handler(this.figure, -0.2, 'y')
        }
        else if (key == 'a') {
            this.figure = handler(this.figure, 0.2, 'x')
        }
        else if (key == 'd') {
            this.figure = handler(this.figure, -0.2, 'x')
        }
        else if (key == 'q') {
            this.figure = handler(this.figure, 0.2, 'z')
        }
        else if (key == 'e') {
            this.figure = handler(this.figure, -0.2, 'z')
        }
        else if (key == null) {
            this.figure = handler(this.figure, 0.2, 'x')
            this.figure = handler(this.figure, -0.2, 'y')
            this.figure = handler(this.figure, 0.2, 'z')
        }
        else{}
        let fg = Operations.projected(this.figure, this.projection);
        let axis = Operations.projected(this.axisMap, this.projection);
        Utils.clear_window(context, canvas);
        Utils.drawAxiss(context, axis);
        Utils.drawFigure(context, fg);
    }

    init()
    {
        let canvas = document.getElementById("myCanvas");
        let context = canvas.getContext("2d");
        let projection = document.getElementsByName('projection');
        let trans_mode = document.getElementsByName('transform');
        
        projection.forEach(x => {
            x.onchange = (e) => {
                this.projection = parseInt(e.target.value);
                this.draw_field(context, canvas, 'kek', undefined);
            };
        });
        trans_mode.forEach(x => {
            x.onchange = (e) => {
                this.moving = parseInt(e.target.value);
            };
        });

        this.draw_field(context, canvas, null, Operations.rotate_ort);

        document.addEventListener('keydown', (e) => {
            let key = e.key;
            let handler_move;
            (this.moving === 0) ? (handler_move = Operations.move_figure) : 0;
            (this.moving === 1) ? (handler_move = Operations.push_figure) : 0;
            (this.moving === 2) ? (handler_move = Operations.rotate_ort) : 0;
            (this.moving === 3) ? (handler_move = Operations.reflect_figure) : 0;
            this.draw_field(context, canvas, key, handler_move);
        });
    }
}