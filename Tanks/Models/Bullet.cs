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
        private readonly Bitmap leftStepSprite =  Properties.Resources.bullet_left;
        private readonly Bitmap upStepSprite = Properties.Resources.bullet_up;
        private readonly Bitmap rightStepSprite = Properties.Resources.bullet_right;
        private readonly Bitmap downStepSprite = Properties.Resources.bullet_down;

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
            switch (direction)
            {
                case Direction.Left:
                    Coordinates.X -= 2;
                    break;
                case Direction.Up:
                    Coordinates.Y -= 2;
                    break;
                case Direction.Right:
                    Coordinates.X += 2;
                    break;
                case Direction.Down:
                    Coordinates.Y += 2;
                    break;
            }
        }
    }
}
