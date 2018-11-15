var figure = [];

for (var i = 0; i < 6; i++) {
    for (var j = 0; j < 10; j++) {
        figure.push([i * 20, j * 20, Math.random()*10], 1);
    }
}



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
