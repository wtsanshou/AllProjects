function RGBAColor(r, g, b, alpha) {
    this.r = r;
    this.g = g;
    this.b = b;
    this.alpha = alpha;
}

RGBAColor.prototype.setFromHSV = function(h, s, v, alpha) {
    h = h % 360;

    if (h < 0) {
        h += 360;
    }

    var c = v * s;
    var h1 = h / 60;
    var x = c * (1 - Math.abs(h1%2 - 1));
    var r1, g1, b1;

    switch (Math.floor(h1)) {
    case 0: r1 = c; g1 = x; b1 = 0; break;
    case 1: r1 = x; g1 = c; b1 = 0; break;
    case 2: r1 = 0; g1 = c; b1 = x; break;
    case 3: r1 = 0; g1 = x; b1 = c; break;
    case 4: r1 = x; g1 = 0; b1 = c; break;
    case 5: r1 = c; g1 = 0; b1 = x; break;
    }

    var m = v - c;

    this.r = Math.floor((r1 + m) * 255);
    this.g = Math.floor((g1 + m) * 255);
    this.b = Math.floor((b1 + m) * 255);
    this.alpha = alpha;
}

RGBAColor.prototype.setRGBA = function(r, g, b, alpha) {
    this.r = r;
    this.g = g;
    this.b = b;
    this.alpha = alpha;
}

var canvasObj;
var canvasCtx;

window.addEventListener("load", loadHandler, false);

function loadHandler() {
    canvasObj = document.getElementById("canvas");

    if (canvasObj.getContext) {
        canvasCtx = canvasObj.getContext("2d");
        setupExample();
    }
}

//---------------- Specific part ----------------

var dropsArray = new Array();

function setupExample() {
    window.setInterval(drawDropsIntervalHandler, 35);
    window.setInterval(newDropIntervalHandler, 130);
}

function newDropIntervalHandler() {
    var pointX = Math.floor(Math.random() * (canvasObj.width+1));
    var pointY = Math.floor(Math.random() * (canvasObj.height+1));

    //-- Generates a random "hue" in the range 0...359 (degrees) --
    var randomHue = Math.random() * 360;

    //-- Adds the drop to the list --
    var drop = new CircleDrop(pointX, pointY, randomHue);

    dropsArray.push(drop);
}

function drawDropsIntervalHandler() {
    //-- Draws all drops --
    drawDrops();
    
    //-- Advances the state of all drops --
    var k = 0;

    for (var i = 0; i < dropsArray.length; i++) {
        dropsArray[k] = dropsArray[i];

        if (dropsArray[k].advance()) {
            k++;
        }
    }

    //-- Truncates the array if necessary --
    dropsArray.length = k;
}

function drawDrops() {
    canvasCtx.fillStyle = "#E8E8E8";
    canvasCtx.fillRect(0, 0, canvasObj.width, canvasObj.height);

    for (var i = 0; i < dropsArray.length; i++) {
        var drop = dropsArray[i];

        canvasCtx.fillStyle = "rgba(" + drop.color.r + "," + drop.color.g + "," + drop.color.b + "," + drop.color.alpha + ")";
        canvasCtx.beginPath();
        canvasCtx.arc(drop.centerX, drop.centerY, drop.radius, 0, 2*Math.PI, false);
        canvasCtx.fill();
    }
}

function CircleDrop(centerX, centerY, hue) {
    this.centerX = centerX;
    this.centerY = centerY;
    this.radius = 1;
    this.color = new RGBAColor();
    this.color.setFromHSV(hue, 1, 1, 1);
}

CircleDrop.prototype.advance = function() {
    this.radius += 2;
    this.color.alpha -= 0.03;

    if (this.color.alpha <= 0) {
        return false;
    }

    return true;
}