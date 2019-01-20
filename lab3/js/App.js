class App 
{
    /*

    триметрія, кабінетна, триточкова перспективна

    обертання відносно рухомої осі, паралельна Оу і пересувається z2 + x2 = R2 
    
    */
    constructor()
    {
        this.xyz = {
            x: t => {
                return 3 * Math.cos(t);
            },
            y: t => {
                return 3 * Math.sin(t);
            },
            z: t => {
                return t * t;
            }
        }
        // this.start_figure = Utils.initialize_figure(this.xyz);
        this.start_figure = [
            [0,0,1,1],
            [100,50,1,1],
            [300,0,1,1],
        ];
        this.figure = this.start_figure;
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
        else if (key == null && handler !== null) {
            // console.log(this.crazy_mode.status);
            this.figure = handler(this.figure, 0.2, 'x')
            this.figure = handler(this.figure, -0.2, 'y')
            this.figure = handler(this.figure, 0.2, 'z')
        }
        else if (key == '+' && this.crazy_mode.status !== false) {
            // Console.log('kek');
            this.figure = Operations.line_rotate(this.figure, this.crazy_mode.line_points);
        }
        Utils.clear_window(context, canvas);
        Utils.drawAxiss(context, this.axisMap);
        Utils.drawFigure(context, this.figure);
    }

    init()
    {
        let canvas = document.getElementById("myCanvas");
        let context = canvas.getContext("2d");
        let type = document.getElementsByName('type');
        let trans_mode = document.getElementsByName('transform');
        
        type.forEach(x => {
            x.onchange = (e) => {
                let f_type = parseInt(e.target.value);
                if (f_type === 0)
                    this.figure = Bezier.GetBezier(this.start_figure);
                else if (f_type === 1)
                    this.figure = InterpolateSpline.getSpline(this.start_figure);
                this.draw_field(context, canvas, 'kek', undefined);
            };
        });
        trans_mode.forEach(x => {
            x.onchange = (e) => {
                this.moving = parseInt(e.target.value);
            };
        });
        
        this.figure = Bezier.GetBezier(this.figure);
        // console.log(this.figure);
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
