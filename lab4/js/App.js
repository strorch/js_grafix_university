class App 
{
    constructor()
    {
        this.figure = [];
        this.moving = 2;
        this.type = 0;
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
            this.figure = handler(this.figure, 0.2, 'x')
            this.figure = handler(this.figure, -0.2, 'y')
            this.figure = handler(this.figure, 0.2, 'z')
        }
        this.new_field(context, canvas, this.figure);
    }

    new_field(context, canvas, figure)
    {
        let axis = Operations.projected(this.axisMap);
        Utils.clear_window(context, canvas);
        Utils.drawAxiss(context, axis);
        App.draw_figure(context,figure, this.type);
    }
    static get_figure(type)
    {
        let figure;
        (type == 0) ? (figure = SplineCalculation.SplineToArr(SplineCalculation.CreateSpline(Utils.initialize_spline_figure()))) : 0;
        (type == 1) ? (figure = Rotation.createRotation()) : 0;
        (type == 2) ? (figure = Kunsa.createCunsa()) : 0;
        return figure;
    }
    static draw_figure(context, figure, type)
    {
        (type == 0) ? (SplineCalculation.draw_spline(context, figure)) : 0;
        (type == 1) ? (Rotation.DrawRotation(figure, context)) : 0;
        (type == 2) ? (figure = Kunsa.DrawKunsa(figure, context)) : 0;
    }

    init()
    {
        let canvas = document.getElementById("myCanvas");
        let context = canvas.getContext("2d");
        let trans_mode = document.getElementsByName('transform');
        let type = document.getElementsByName('type');
        
        trans_mode.forEach(x => {
            x.onchange = (e) => {
                this.moving = parseInt(e.target.value);
            };
        });
        type.forEach(x => {
            x.onchange = (e) => {
                this.type = parseInt(e.target.value);
                this.figure = App.get_figure(this.type);
                this.new_field(context, canvas, this.figure);
            };
        });
        this.figure = SplineCalculation.SplineToArr(SplineCalculation.CreateSpline(Utils.initialize_spline_figure()));
        
        this.new_field(context, canvas, this.figure);
        
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
