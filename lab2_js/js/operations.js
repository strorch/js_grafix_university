class Operations
{
    static line_rotate(figure, line)
    {
        let a = line[0][0];
        let b = line[0][1];
        let angle = 0.2;
        let rotate_arr = [
            [Math.cos(angle), -Math.sin(angle), 0, 0],
            [Math.sin(angle), Math.cos(angle),  0, 0],
            [0,               0,                1, 0],
            [0,               0,                0, 1],
        ];
        let move_arr_to = [
            [1, 0, 0, 0],
            [0, 1, 0, 0],
            [0, 0, 1, 0],
            [-a, -b, 0, 1],
        ];
        let move_arr_back = [
            [1, 0, 0, 0],
            [0, 1, 0, 0],
            [0, 0, 1, 0],
            [a, b, 0, 1],
        ];
        let fig_m = math.matrix(figure);
        let rotate_m = math.matrix(rotate_arr);
        let move_to_m = math.matrix(move_arr_to);
        let move_back_m = math.matrix(move_arr_back);

        let f = math.multiply(move_to_m, rotate_m);
        let s = math.multiply(f, move_back_m);
        let result = math.multiply(fig_m, s);
        return result.valueOf();
    }

    static projected(figure, projection)
    {
        if (projection === 0)
            return figure;
        let trimetrie = [
            [Math.cos(60*Math.PI/180),  -Math.sin(60*Math.PI/180) * Math.sin(45*Math.PI/180), 0, 0],
            [0,             Math.cos(45*Math.PI/180),                                         0, 0],
            [-Math.sin(60*Math.PI/180), -Math.cos(60*Math.PI/180) * Math.sin(45*Math.PI/180), 0, 0],
            [0,                0,                                  0, 1],
        ];
        
        let kabinet = [
            [1,     0,     0, 0],
            [0,     1,     0, 0],
            [0.354, 0.354, 0, 0],
            [0,     0,     0, 1],
        ];

        let three_point = [
            [1,  0, 0, -1.0/500.0],
            [0,  1, 0, -1.0/900.0],
            [0,  0, 1, 1],
            [0,  0, 0, 1],
        ];

        let projection_arr;
        (projection === 1) ? (projection_arr = trimetrie) : 0;
        (projection === 2) ? (projection_arr = kabinet) : 0;
        (projection === 3) ? (projection_arr = three_point) : 0;
        let projection_m = math.matrix(projection_arr);
        let figure_m = math.matrix(figure);
        let res = math.multiply(figure_m, projection_m);
        return res.valueOf();
    }

    static rotate_ort(figure, angle, axis)
    {
        let z_axis = [
            [Math.cos(angle), -Math.sin(angle), 0, 0],
            [Math.sin(angle), Math.cos(angle),  0, 0],
            [0,               0,                1, 0],
            [0,               0,                0, 1],
        ];
        
        let y_axis = [
            [Math.cos(angle), 0, -Math.sin(angle), 0],
            [0,               1, 0,                0],
            [Math.sin(angle), 0, Math.cos(angle),  0],
            [0,               0, 0,                1],
        ];

        let x_axis = [
            [1, 0,               0,                0],
            [0, Math.cos(angle), -Math.sin(angle), 0],
            [0, Math.sin(angle), Math.cos(angle),  0],
            [0, 0,               0,                1],
        ];

        let rotate;
        (axis === 'x') ? (rotate = x_axis) : 0;
        (axis === 'y') ? (rotate = y_axis) : 0;
        (axis === 'z') ? (rotate = z_axis) : 0;
        let rotate_m = math.matrix(rotate);
        let figure_m = math.matrix(figure);
        let mult_res = math.multiply(figure_m, rotate_m);
        return mult_res.valueOf();
    }

    static push_figure(figure, angle, axis)
    {
        (angle > 0) ? (angle = 1.05) : (angle = 0.95);
        let tab;
        (axis === 'x') ? (tab = {x:angle, y:1, z:1}) : 0;
        (axis === 'y') ? (tab = {x:1, y:angle, z:1}) : 0;
        (axis === 'z') ? (tab = {x:1, y:1, z:angle}) : 0;
        let x = tab.x;
        let y = tab.y;
        let z = tab.z;
        let move_arr = [
            [x, 0, 0, 0],
            [0, y, 0, 0],
            [0, 0, z, 0],
            [0, 0, 0, 1],
        ];

        let move_m = math.matrix(move_arr);
        let figure_m = math.matrix(figure);
        let mult_res = math.multiply(figure_m, move_m);
        return mult_res.valueOf();
    }
    
    static move_figure(figure, angle, axis)
    {
        (angle > 0) ? (angle = 10) : (angle = -10);
        let tab;
        (axis === 'x') ? (tab = {x:angle, y:0, z:0}) : 0;
        (axis === 'y') ? (tab = {x:0, y:angle, z:0}) : 0;
        (axis === 'z') ? (tab = {x:0, y:0, z:angle}) : 0;
        let x = tab.x;
        let y = tab.y;
        let z = tab.z;
        let move_arr = [
            [1, 0, 0, 0],
            [0, 1, 0, 0],
            [0, 0, 1, 0],
            [x, y, z, 1],
        ];

        let move_m = math.matrix(move_arr);
        let figure_m = math.matrix(figure);
        let mult_res = math.multiply(figure_m, move_m);
        return mult_res.valueOf();
    }

    static reflect_figure(figure, angle, axis)
    {
        (angle > 0) ? (angle = 1) : (angle = -1);
        let tab;
        (axis === 'x') ? (tab = {x:angle, y:1, z:1}) : 0;
        (axis === 'y') ? (tab = {x:1, y:angle, z:1}) : 0;
        (axis === 'z') ? (tab = {x:1, y:1, z:angle}) : 0;
        let x = tab.x;
        let y = tab.y;
        let z = tab.z;
        let move_arr = [
            [x, 0, 0, 0],
            [0, y, 0, 0],
            [0, 0, z, 0],
            [0, 0, 0, 1],
        ];

        let move_m = math.matrix(move_arr);
        let figure_m = math.matrix(figure);
        let mult_res = math.multiply(figure_m, move_m);
        return mult_res.valueOf();
    }

}