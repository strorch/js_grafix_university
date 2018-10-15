    var move = function (figure, context, canvas, e) {
        //console.log(figure);
        var m_fig = math.matrix(figure);
        var mx =e.x - 800 / 2;
        var my =500 / 2-e.y;
        var len = math.sqrt(mx*mx + my*my);
        mx = 2 * mx / len;
        my = 2 * my / len;

        var move_m = [
            [1,  0,  0],
            [0,  1,  0],
            [mx, my, 1],
        ];
        var m_move = math.matrix(move_m);
        var result = math.multiply(m_fig, m_move);

        drawFigure(context, result.valueOf(), canvas);
        return result.valueOf();
    };

    var push_m= function (figure, context, canvas, e) {
        var m_fig = math.matrix(figure);
        var mx =(((e.y - 250) / 250) >= 0) ? (1.05) : (0.95);
        var my =(((250-e.y) / 250) >= 0) ? (1.05) : (0.95);

        //console.log(mx+" "+my);
        var push_m = [
            [mx, 0,  0],
            [0,  my, 0],
            [0,  0,  1],
        ];
        var m_move = math.matrix(push_m);
        var result = math.multiply(m_fig, m_move);
        // console.log(result.valueOf());
        drawFigure(context, result.valueOf(), canvas);
        return result.valueOf();
    };

    var clear_window = function (context, canvas) {
        context.clearRect(0, 0, canvas.width, canvas.height);
    };

    var drawFigure = function (context, figure, canvas) {

        context.clearRect(0, 0, canvas.width, canvas.height);
        context.beginPath();
        context.moveTo(figure[0][0] + 800 / 2, -figure[0][1] + 500 / 2);
        for (var i = 0; i < figure.length - 1; i++)
            context.lineTo(figure[i + 1][0] + 800 / 2, -figure[i + 1][1] + 500 / 2);

        context.lineTo(figure[0][0] + 800 / 2, -figure[0][1] + 500 / 2);
        context.closePath();
        context.stroke();
    };