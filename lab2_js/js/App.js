class App 
{
    /*

    триметрія, кабінетна, триточкова перспективна

    обертання відносно рухомої осі, паралельна Оу і пересувається z2 + x2 = R2 
    
    */
    constructor()
    {
        this.figure = App.initialize_figure();
        this.angle = [0, 0, 0];
        this.moved = [1, 1, 1];
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
    draw_field(context, canvas)
    {
        Utils.clear_window(context, canvas);

        let rotated_arr = [];
        this.figure.forEach( (item) => {
            rotated_arr.push( Operations.rotate_ort(item, this.angle));
        });
        Utils.drawAxiss(context, this.axisMap);
        App.drawFigure(context, rotated_arr);

        // Utils.drawAxiss(context, this.axisMap);


        //Utils.drawAxiss(context, this.axisMap);
        // let rotated_arr = [];ww
        // this.axisMap.forEach( (item) => {
            // rotated_arr.push( Operations.rotate_ort(item, this.angle));
        // });
        //console.log(rotated_arr);        
        // let new_arr = Operations.move_figure(rotated_arr, this.moved);
        //console.log(new_arr);
        // Utils.drawAxiss(context, new_arr);
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
            //console.log(key);
            if (key == 'w') {
                this.angle[0] += 5;
                this.draw_field(context, canvas);
            }
            else if (key == 's') {
                this.angle[0] += -5;
                this.draw_field(context, canvas);
            }
            else if (key == 'a') {
                this.angle[1] += 5;
                this.draw_field(context, canvas);
            }
            else if (key == 'd') {
                this.angle[1] += -5;
                this.draw_field(context, canvas);
            }
            else if (key == 'q') {
                this.angle[2] += 5;
                this.draw_field(context, canvas);
            }
            else if (key == 'e') {
                this.angle[2] += -5;
                this.draw_field(context, canvas);
            }
            else
                return;
        });
    }
}