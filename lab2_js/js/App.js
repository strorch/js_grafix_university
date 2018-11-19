class App 
{
    constructor()
    {
        this.figure = App.initialize_figure();
        this.moved = { 
            'x': 0, 
            'y': 0
        };
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

    init()
    {   
        var moved = { 
            'x': 0, 
            'y': 0
        };
        var rot_p = {
            'x': 0, 
            'y': 0
        };
        var status = 0;
        var point = false;
        var reflect_status = false;
        
        var a = 0;
        var b = 0;
        var canvas = document.getElementById("myCanvas");
        var context = canvas.getContext("2d");
        Utils.drawFigure(context, this.figure, canvas);
        //drawLine(context, canvas);
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