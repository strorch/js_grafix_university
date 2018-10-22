
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


var drawLine = function()
{
    var canvas = document.getElementById("myCanvas");
    var context = canvas.getContext("2d");
    drawFigure(context, figure, canvas);

	a = document.getElementById('a').value.toString();
	b = document.getElementById('b').value.toString();

    //console.log(a + " " + b);
    var s= [-500 , -500 * a + b];
    var f= [500, 500 * a + b];
	context.beginPath();
    context.moveTo(f[0] + 800 / 2, -(f[1]) + 500 / 2);
    context.lineTo(s[0] + 800 / 2, -(s[1]) + 500 / 2);
    context.closePath();
    context.stroke();
}