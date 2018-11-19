
class Operations
{

    static move_help(mx, my, figure)
    {
        let m_fig = math.matrix(figure);

        let move_m = [
            [1,  0,  0],
            [0,  1,  0],
            [mx, my, 1],
        ];
        let m_move = math.matrix(move_m);
        let result = math.multiply(m_fig, m_move);
        return result;
    }

    static move_m(figure, moved, context, canvas, e) {
        let mx =e.x - 800 / 2;
        let my =500 / 2-e.y;

        let len = math.sqrt(mx*mx + my*my);
        mx = 2 * mx / len;
        my = 2 * my / len;

        let result = move_help(mx, my, figure);
        moved['x'] += mx;
        moved['y'] += my;
        drawFigure(context, result.valueOf(), canvas);
        return result.valueOf();
    }

    static push_m(figure, moved, context, canvas, e) {

        let m_fig = math.matrix(figure);
        let mx =(((e.y - 250) / 250) >= 0) ? (1.05) : (0.95);
        let my =(((250-e.y) / 250) >= 0) ? (1.05) : (0.95);

        let push_m = [
            [mx, 0,  0],
            [0,  my, 0],
            [0,  0,  1],
        ];
        let m_move = math.matrix(push_m);
        let result = math.multiply(m_fig, m_move);
        drawFigure(context, result.valueOf(), canvas);
        return result.valueOf();
    }

    static rotate_m(figure, moved, context, canvas, e) {

        let m = moved['x']  - 800 / 2;
        let n = -moved['y'] + 500 / 2;
        let m_fig = math.matrix(figure);
        let angle =(((e.y - 250) / 250) >= 0) ? (0.1) : (-0.1);
        let rot_m = [
            [Math.cos(angle),                            Math.sin(angle),                            0],
            [-Math.sin(angle),                           Math.cos(angle),                            0],
            [-m*(Math.cos(angle)-1) + n*Math.sin(angle), -n*(Math.cos(angle)-1) - m*Math.sin(angle), 1],
        ];
        let m_move = math.matrix(rot_m);
        let result = math.multiply(m_fig, m_move);
        drawFigure(context, result.valueOf(), canvas);
        context.fillRect(rot_p['x'] , rot_p['y'] - 5, 5,5);
        return result.valueOf();
    }

    static reflect_point(figure, moved, context, canvas) {    
        let angle = math.atan(a);
        console.log(angle);
        let n = -b;
        let m_fig = math.matrix(figure);
        let reflect = [
            [1, 0,  0],
            [0,  -1,  0],
            [0,  0,  1],
        ];
        let move1 = [
            [1,  0,  0],
            [0,  1,  0],
            [0,  n,  1],
        ];
        let move2 = [
            [1,  0,  0],
            [0,  1,  0],
            [0,  -n, 1],
        ];
        let rotate1 = [
            [Math.cos(angle),                            Math.sin(angle),                            0],
            [-Math.sin(angle),                           Math.cos(angle),                            0],
            [0, 0, 1],
        ];
        let rotate2 = [
            [Math.cos(angle),                          -Math.sin(angle),                            0],
            [Math.sin(angle),                           Math.cos(angle),                            0],
            [0, 0, 1],
        ];

        let m_move1 = math.matrix(move1);
        let m_move2 = math.matrix(move2);
        let m_rotate1 = math.matrix(rotate1);
        let m_rotate2 = math.matrix(rotate2);
        let m_reflect = math.matrix(reflect);

        let f = math.multiply(m_move1, m_rotate2);
        let s = math.multiply(f, m_reflect);
        let t = math.multiply(s, m_rotate1);
        let last = math.multiply(t, m_move2);

        let result = math.multiply(m_fig, last);
        
        drawFigure(context, result.valueOf(), canvas);
        return result.valueOf();
    }
}