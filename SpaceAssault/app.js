// Оболочка возвращает доступный в текущем браузере способ обновления кадра.
var requestAnimFrame = (function () {
    return window.requestAnimationFrame ||
        window.webkitRequestAnimationFrame ||
        window.mozRequestAnimationFrame ||
        window.oRequestAnimationFrame ||
        window.msRequestAnimationFrame ||
        function (callback) {
            window.setTimeout(callback, 1000 / 60);
        };
})();

var player = {
    position: [0, 0],
    previousPosition: [0, 0],
    sprite: new Sprite('img/sprites.png', [0, 0], [39, 39], 16, [0, 1])
};
var bullets = [];
var enemies = [];
var explosions = [];
var megaliths = [];

var lastFire = Date.now();
var gameTime = 0;
var isGameOver;
var terrainPattern;

var score = 0;
var scoreElement = document.getElementById('score');

var playerSpeed = 200;
var bulletSpeed = 500;
var enemySpeed = 100;

var canvas = document.getElementById('canvas');
var context = canvas.getContext("2d");

// Основной цикл приложения
var lastTime;
function main() {
    var now = Date.now();
    var differenceOfTime = (now - lastTime) / 1000.0;

    update(differenceOfTime);
    render();

    lastTime = now;
    requestAnimFrame(main);
};

function initial() {
    terrainPattern = context.createPattern(resources.get('img/background.png'), 'repeat');
    document.getElementById('restart').onclick = reset;
    reset();
    lastTime = Date.now();
    main();
};

function update(differenceOfTime) {
    gameTime += differenceOfTime;

    handleInput(differenceOfTime);
    updateEntities(differenceOfTime);

    if (Math.random() < 1 - Math.pow(.993, gameTime)) {
        enemies.push({
            position: [canvas.width, Math.random() * (canvas.height - 39)],
            sprite: new Sprite(
                'img/sprites.png',
                [0, 78],
                [80, 39],
                6,
                [0, 1, 2, 3, 2, 1])
        });
    }
    checkCollisions(differenceOfTime);
    scoreElement.innerHTML = score;

};

function updateEntities(differenceOfTime) {
    for (var i = 0; i < enemies.length; i++) {
        enemies[i].position[0] -= enemySpeed * differenceOfTime;

        if (enemies[i].position[0] + enemies[i].sprite.size[0] < 0) {
            enemies.splice(i, 1);
            i--;
        }
    }

    for (var i = 0; i < bullets.length; i++) {
        var bullet = bullets[i];

        switch (bullet.direction) {
            case 'up':
                bullet.position[1] -= bulletSpeed * differenceOfTime;
                break;
            case 'down':
                bullet.position[1] += bulletSpeed * differenceOfTime;
                break;
            default:
                bullet.position[0] += bulletSpeed * differenceOfTime;
                break;
        }

        if (bullet.position[1] < 0 || bullet.position[1] > canvas.height ||
            bullet.position[0] > canvas.width) {
            bullets.splice(i, 1);
            i--;
        }

        if (explosions.length > 100) {
            explosions.splice(0, 10);
        }

    }
}

function collides(x, y, r, b, x2, y2, r2, b2) {
    return !(r <= x2 || x > r2 ||
        b <= y2 || y > b2);
}

function boxCollides(position, size, position2, size2) {
    return collides(position[0], position[1],
        position[0] + size[0], position[1] + size[1],
        position2[0], position2[1],
        position2[0] + size2[0], position2[1] + size2[1]);
}

function checkCollisions(differenceOfTime) {
    var position, position2, size, size2;
    checkPlayerBounds();
    outer:for (var i = 0; i < enemies.length; i++) {
        position = enemies[i].position;
        size = enemies[i].sprite.size;

        for (var j = 0; j < bullets.length; j++) {
            position2 = bullets[j].position;
            size2 = bullets[j].sprite.size;

            if (boxCollides(position, size, position2, size2)) {
                enemies.splice(i, 1);
                i--;
                score++;
                explosions.push({
                    position: position,
                    sprite: new Sprite(
                        'img/sprites.png',
                        [0, 117],
                        [39, 39],
                        16,
                        [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                        true
                    )

                });
                bullets.splice(j, 1);
                break outer;
            }
        }
        for (var j = 0; j < megaliths.length; j++) {
            position2 = megaliths[j].position;
            size2 = megaliths[j].sprite.size;

            if (boxCollides(position, size, position2, size2)) {
                if ((position[1] + size[1]/2) > (position2[1] + size2[1]/2)) {
                    enemies[i].position[1] += enemySpeed * differenceOfTime;
                } else {
                    enemies[i].position[1] -= enemySpeed * differenceOfTime;
                }
            }
        }
        if (boxCollides(position, size, player.position, player.sprite.size)) {
            gameOver();
        }
    }
    for (var i = 0; i < bullets.length; i++) {
        position = bullets[i].position;
        size = bullets[i].sprite.size;

        for (var j = 0; j < megaliths.length; j++) {
            position2 = megaliths[j].position;
            size2 = megaliths[j].sprite.size;

            if (boxCollides(position, size, position2, size2)) {
                bullets.splice(i, 1);
                i--;
                break;
            }
        }
    }
}

function checkPlayerBounds() {
    if(player.position[0] < 0) {
        player.position[0] = 0;
    }
    else if(player.position[0] > canvas.width - player.sprite.size[0]) {
        player.position[0] = canvas.width - player.sprite.size[0];
    }

    if(player.position[1] < 0) {
        player.position[1] = 0;
    }
    else if(player.position[1] > canvas.height - player.sprite.size[1]) {
        player.position[1] = canvas.height - player.sprite.size[1];
    }
}

function render() {
    context.fillStyle = terrainPattern;
    context.fillRect(0, 0, canvas.width, canvas.height);

    if (!isGameOver) {
        renderEntity(player);
    }

    renderEntities(enemies);
    renderEntities(bullets);
    renderEntities(explosions);
    renderEntities(megaliths);

}

function renderEntity(entity) {
    context.save();
    context.translate(entity.position[0], entity.position[1]);
    entity.sprite.render(context);
    context.restore();
}

function renderEntities(array) {
    for (var i = 0; i < array.length; i++) {
        renderEntity(array[i]);
    }
}

function isPlayerCollideWithMegaliths(differenceOfTime) {
    for (var i = 0; i < megaliths.length; i++) {
        var position = megaliths[i].position;
        var size = megaliths[i].sprite.size;

        if (boxCollides(position, size, player.position, player.sprite.size)) {
            return true
        }
    }
    return false;
}
function handleInput(differenceOfTime) {
    if (input.isDown('DOWN') || input.isDown('s')) {
        player.position[1] += playerSpeed * differenceOfTime;
        if (isPlayerCollideWithMegaliths()) {
            player.position[1] -= playerSpeed * differenceOfTime;
        }
    }

    if (input.isDown('UP') || input.isDown('w')) {
        player.position[1] -= playerSpeed * differenceOfTime;
        if (isPlayerCollideWithMegaliths()) {
            player.position[1] += playerSpeed * differenceOfTime;
        }
    }

    if (input.isDown('LEFT') || input.isDown('a')) {
        player.position[0] -= playerSpeed * differenceOfTime;
        if (isPlayerCollideWithMegaliths()) {
            player.position[0] += playerSpeed * differenceOfTime;
        }
    }

    if (input.isDown('RIGHT') || input.isDown('d')) {
        player.position[0] += playerSpeed * differenceOfTime;
        if (isPlayerCollideWithMegaliths()) {
            player.position[0] -= playerSpeed * differenceOfTime;
        }
    }

    if (input.isDown('SPACE') &&
        !isGameOver &&
        Date.now() - lastFire > 100) {
        var x = player.position[0] + player.sprite.size[0] / 2;
        var y = player.position[1] + player.sprite.size[1] / 2;

        bullets.push({
            position: [x, y],
            direction: 'forward',
            sprite: new Sprite('img/sprites.png', [0, 39], [18, 8], 1, [0])
        });
        bullets.push({
            position: [x, y],
            direction: 'up',
            sprite: new Sprite('img/sprites.png', [0, 50], [9, 5], 1, [0])
        });
        bullets.push({
            position: [x, y],
            direction: 'down',
            sprite: new Sprite('img/sprites.png', [0, 60], [9, 5], 1, [0])
        });
        lastFire = Date.now();
    }
}

function gameOver() {
    isGameOver = true;
    document.getElementById('gameOver').style.display = 'flex';
}

function reset() {
    document.getElementById('gameOver').style.display = 'none';
    isGameOver = false;
    gameTime = 0;
    score = 0;

    enemies = [];
    bullets = [];
    explosions = [];
    megaliths = createMegaliths(3, 5);

    player.position = [50, canvas.height / 2];
};

function createMegaliths(min, max) {
    var megaliths = [];
    var amount = randomInteger(min, max);

    for (var i = 0; i < amount; i++) {
        megaliths.push({
            position: [randomInteger(0, 445), randomInteger(0, 447)],
            sprite: new Sprite('img/sprites.png', [3, 213], [55, 53], 1, [0])
        });
    }


    return megaliths;
}

function randomInteger(min, max) {
    let rand = min + Math.random() * (max + 1 - min);
    return Math.floor(rand);
  }

resources.onCompleteLoad(initial);
resources.load([
    'img/sprites.png',
    'img/background.png',
]);
