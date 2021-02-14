using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Bullet : MobileEntity
    {
        private readonly Bitmap leftStepSprite = new Bitmap(@"..\..\..\img\bullet_left.png");
        private readonly Bitmap upStepSprite = new Bitmap(@"..\..\..\img\bullet_up.png");
        private readonly Bitmap rightStepSprite = new Bitmap(@"..\..\..\img\bullet_right.png");
        private readonly Bitmap downStepSprite = new Bitmap(@"..\..\..\img\bullet_down.png");

        public Bullet(Direction direction) : base()
        {
            Direction = direction;
        }
        public Bullet(Direction direction, Point coordinates) : base(coordinates)
        {
            Direction = direction;
        }

        public override Direction Direction
        {
            get => base.Direction;
            set
            {
                switch (value)
                {
                    case Direction.Left:
                        Sprite = leftStepSprite;
                        break;
                    case Direction.Up:
                        Sprite = upStepSprite;
                        break;
                    case Direction.Right:
                        Sprite = rightStepSprite;
                        break;
                    case Direction.Down:
                        Sprite = downStepSprite;
                        break;
                }
                base.Direction = value;
            }
        }

        internal override void MakeAStep()
        {
            base.MakeAStep();
            base.MakeAStep();
        }
    }
}
