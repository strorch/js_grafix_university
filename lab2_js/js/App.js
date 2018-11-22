class App 
{
    constructor()
    {
        this.figure = App.initialize_figure();
        this.angle = [20, -20, 20];
        this.moved = { 
            'x': 0, 
            'y': 0
        };
        this.rot_p = {
            'x': 0, 
            'y': 0
        };
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
                figure.push([i * 20, j * 20, Math.random()*10]);
            }
        }
        return figure;
    }

    draw_field(context, canvas, angle)
    {
        Utils.clear_window(context, canvas);
        //Utils.drawAxiss(context, this.axisMap);
        let new_arr = [];
        this.axisMap.forEach( (item) => {
            new_arr.push( Operations.rotate_ort(item, angle));
        });
        Utils.drawAxiss(context, new_arr);
        // Utils.drawFigure(context, new_arr);
    }

    init()
    {
        let canvas = document.getElementById("myCanvas");
        let context = canvas.getContext("2d");

        Utils.clear_window(context, canvas);
        this.draw_field(context, canvas, this.angle);

        document.addEventListener('keydown', (e) => {
            let key = e.key;
            //console.log(this.angle);
            if (key == 'w') {
                this.angle[0] += 5;
                this.draw_field(context, canvas, this.angle);
            }
            else if (key == 's') {
                this.angle[0] += -5;
                this.draw_field(context, canvas, this.angle);
            }
            else if (key == 'a') {
                this.angle[1] += 5;
                this.draw_field(context, canvas, this.angle);
            }
            else if (key == 'd') {
                this.angle[1] += -5;
                this.draw_field(context, canvas, this.angle);
            }
            else if (key == 'q') {
                this.angle[2] += 5;
                this.draw_field(context, canvas, this.angle);
            }
            else if (key == 'e') {
                this.angle[2] += -5;
                this.draw_field(context, canvas, this.angle);
            }
            else
                return;
        });

        // canvas.onmousedown = (e) => {
        //     if (point === true) {
        //         rot_p['x'] = e.x
        //         rot_p['y'] = e.y;
        //     }
        //     if (point === false)
        //     {
        //         let moveAt = (e) => {
        //             if (status == 0)
        //                 this.figure = Operations.move_m(this.figure, moved, context, canvas, e);
        //             else if (status == 1)
        //                 this.figure = Operations.push_m(this.figure, moved, context, canvas, e);
        //             else if (status == 2)
        //                 this.figure = Operations.rotate_m(this.figure, rot_p, context, canvas, e);
        //         };
    
        //         document.onmousemove = (e) => {
        //             moveAt(e);
        //         };
    
        //         canvas.onmouseup = (e) =>{
        //             document.onmousemove = null;
        //             canvas.onmouseup = null;
        //         };
        //         Utils.drawFigure(context, figure, canvas);
        //         context.fillRect(rot_p['x'] , rot_p['y'] - 5, 5,5);
        //     }
        //     if (point === true) {
        //         Utils.drawFigure(context, figure, canvas);
        //         context.fillRect(rot_p['x'] , rot_p['y'] - 5, 5,5);
        //     }
            
        // };
    }
}