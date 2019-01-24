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


    init()
    {
        let canvas = document.getElementById("myCanvas");
        let context = canvas.getContext("2d");
    
    }
}
