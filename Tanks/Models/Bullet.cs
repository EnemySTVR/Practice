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
            step = step * 2;
        }
        public Bullet(Direction direction, Point coordinates) : base(coordinates)
        {
            Direction = direction;
            step = step * 2;
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
    }
}
