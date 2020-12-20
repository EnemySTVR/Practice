(function () {
    function Sprite(url, position, size, speed, indexes, once) {
        this.position = position;
        this.size = size;
        this.speed = typeof speed === 'number' ? speed : 0;
        this.indexes = indexes;
        this._index = indexes[0];
        this._counter = 0;
        this._pointer = 0;
        this.url = url;
        this.once = once;
    };

    Sprite.prototype = {
        render: function (context) {
            function draw(ctx) {
                var img = resources.get(ctx.url)
                var xSize = ctx.size[0];
                var ySize = ctx.size[1];
                var x = ctx.position[0] + xSize * ctx._index;
                var y = ctx.position[1];
                context.drawImage(img, x, y, xSize, ySize, 0, 0, xSize, ySize);
            }
            if (this.indexes.length == 1) {
                draw(this);
                return;
            } else {

                if (this._pointer == this.indexes.length && this.once) {
                    return;
                }
                if (this._counter == this.speed) {
                    this._counter = 0;
                    this._pointer++;
                    if (this._pointer == this.indexes.length && !this.once) {
                        this._pointer = 0;
                    }
                }

                this._index = this.indexes[this._pointer];
                draw(this);
                this._counter++;
            }
        },
    };

    window.Sprite = Sprite;
})();