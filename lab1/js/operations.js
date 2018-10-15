var move_help = function(mx, my, figure)
{
    var m_fig = math.matrix(figure);

    var move_m = [
        [1,  0,  0],
        [0,  1,  0],
        [mx, my, 1],
    ];
    var m_move = math.matrix(move_m);
    var result = math.multiply(m_fig, m_move);
    return result;
}

var move_m = function (figure, moved, context, canvas, e) {
    var mx =e.x - 800 / 2;
    var my =500 / 2-e.y;

    var len = math.sqrt(mx*mx + my*my);
    mx = 2 * mx / len;
    my = 2 * my / len;

    var result = move_help(mx, my, figure);
    moved['x'] += mx;
    moved['y'] += my;
    drawFigure(context, result.valueOf(), canvas);
    return result.valueOf();
};

var push_m= function (figure, moved, context, canvas, e) {

    var m_fig = math.matrix(figure);
    var mx =(((e.y - 250) / 250) >= 0) ? (1.05) : (0.95);
    var my =(((250-e.y) / 250) >= 0) ? (1.05) : (0.95);

    var push_m = [
        [mx, 0,  0],
        [0,  my, 0],
        [0,  0,  1],
    ];
    var m_move = math.matrix(push_m);
    var result = math.multiply(m_fig, m_move);
    drawFigure(context, result.valueOf(), canvas);
    return result.valueOf();
};

var rotate_m= function (figure, moved, context, canvas, e) {

    var m = moved['x']  - 800 / 2;
    var n = -moved['y'] + 500 / 2;
    var m_fig = math.matrix(figure);
    var angle =(((e.y - 250) / 250) >= 0) ? (0.1) : (-0.1);
    var rot_m = [
        [Math.cos(angle),                            Math.sin(angle),                            0],
        [-Math.sin(angle),                           Math.cos(angle),                            0],
        [-m*(Math.cos(angle)-1) + n*Math.sin(angle), -n*(Math.cos(angle)-1) - m*Math.sin(angle), 1],
    ];
    var m_move = math.matrix(rot_m);
    var result = math.multiply(m_fig, m_move);
    drawFigure(context, result.valueOf(), canvas);
    return result.valueOf();
};

var reflect_point = function (figure, moved, context, canvas) {
    //y = 1/2 * x + 0
    var p2 = {
        x: 3, //always 1
        y: 2  // value
    };

    var p1 = {
        x: 1,
        y: 0
    };

    // angle in radians
    var angleRadians = Math.atan2(p2.y - p1.y, p2.x - p1.x);
    angle = (math.atan2(p2.y, p2.x) - math.atan2(p1.y, p1.x)) * 180 / Math.PI;
    ///console.log(angle * 180 / Math.PI);

    var n = 0;
    var m_fig = math.matrix(figure);
    var reflect = [
        [1,  0,  0],
        [0,  -1, 0],
        [0,  0,  1],
    ];
    var move1 = [
        [1,  0,  0],
        [0,  1, 0],
        [0,  n,  1],
    ];
    var move2 = [
        [1,  0,  0],
        [0,  1, 0],
        [0,  -n,  1],
    ];
    var rotate1 = [
        [Math.cos(angle),                            Math.sin(angle),                            0],
        [-Math.sin(angle),                           Math.cos(angle),                            0],
        [0, 0, 1],
    ];
    var rotate2 = [
        [Math.cos(angle),                          -Math.sin(angle),                            0],
        [Math.sin(angle),                           Math.cos(angle),                            0],
        [0, 0, 1],
    ];

    var m_move1 = math.matrix(move1);
    var m_move2 = math.matrix(move2);
    var m_rotate1 = math.matrix(rotate1);
    var m_rotate2 = math.matrix(rotate2);
    var m_reflect = math.matrix(reflect);

    var f = math.multiply(m_move2, m_rotate1);
    var s = math.multiply(f, m_reflect);
    var t = math.multiply(s, m_rotate2);
    var last = math.multiply(t, m_move1);
    var result = math.multiply(m_fig, last);
    
    drawFigure(context, result.valueOf(), canvas);
    return result.valueOf();
};