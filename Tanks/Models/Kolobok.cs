using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Kolobok : MobileEntity
    {
        private readonly Bitmap leftStepsSprite = new Bitmap(@"..\..\..\img\kolobok_left.png");
        private readonly Bitmap upAfterLeftStepsSprite = new Bitmap(@"..\..\..\img\kolobok_up_after_left.png");
        private readonly Bitmap downAfterLeftStepsSprite = new Bitmap(@"..\..\..\img\kolobok_down_after_left.png");
        private readonly Bitmap rightStepsSprite = new Bitmap(@"..\..\..\img\kolobok_right.png");
        private readonly Bitmap upAfterRightStepsSprite = new Bitmap(@"..\..\..\img\kolobok_up_after_right.png");
        private readonly Bitmap downAfterRightStepsSprite = new Bitmap(@"..\..\..\img\kolobok_down_after_right.png");

        public Kolobok() : base()
        {
            Direction = Direction.Right;
        }
        public Kolobok(Point coordinates) : base(coordinates) { }

        public override Direction Direction
        {
            get => base.Direction;
            set
            {
                switch (value)
                {
                    case Direction.Left:
                        Sprite = leftStepsSprite;
                        break;
                    case Direction.Up:
                        if (Direction == Direction.Left || Sprite == downAfterLeftStepsSprite)
                        {
                            Sprite = upAfterLeftStepsSprite;
                        }
                        if (Direction == Direction.Right || Sprite == downAfterRightStepsSprite)
                        {
                            Sprite = upAfterRightStepsSprite;
                        }
                        break;
                    case Direction.Right:
                        Sprite = rightStepsSprite;
                        break;
                    case Direction.Down:
                        if (Direction == Direction.Left || Sprite == upAfterLeftStepsSprite)
                        {
                            Sprite = downAfterLeftStepsSprite;
                        }
                        if (Direction == Direction.Right || Sprite == upAfterRightStepsSprite)
                        {
                            Sprite = downAfterRightStepsSprite;
                        }
                        break;
                }
                base.Direction = value;
            }
        }
    }
}
