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
        private readonly Bitmap leftStepsSprite =Properties.Resources.kolobok_left;
        private readonly Bitmap upAfterLeftStepsSprite =Properties.Resources.kolobok_up_after_left;
        private readonly Bitmap downAfterLeftStepsSprite =Properties.Resources.kolobok_down_after_left;
        private readonly Bitmap rightStepsSprite =Properties.Resources.kolobok_right;
        private readonly Bitmap upAfterRightStepsSprite =Properties.Resources.kolobok_up_after_right;
        private readonly Bitmap downAfterRightStepsSprite =Properties.Resources.kolobok_down_after_right;
        private Direction targetDirection = Direction.Right;

        public Kolobok() : base()
        {
            Direction = Direction.Right;
        }
        public Kolobok(Point coordinates) : base(coordinates)
        {
            Direction = Direction.Right;
        }

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

        public void TurnAround()
        {
            switch (Direction)
            {
                case Direction.Left:
                    Direction = Direction.Right;
                    targetDirection = Direction.Right;
                    break;
                case Direction.Up:
                    Direction = Direction.Down;
                    targetDirection = Direction.Down;
                    break;
                case Direction.Right:
                    Direction = Direction.Left;
                    targetDirection = Direction.Left;
                    break;
                case Direction.Down:
                    Direction = Direction.Up;
                    targetDirection = Direction.Up;
                    break;
            }
        }

        internal void ChangeDirection(Direction direction)
        {
            targetDirection = direction;
        }

        internal override void MakeAStep()
        {

            if (targetDirection != Direction)
            {
                if (((Direction == Direction.Down || Direction == Direction.Up) && Coordinates.Y % 15 == 0) ||
                    ((Direction == Direction.Left || Direction == Direction.Right) && Coordinates.X % 15 == 0))
                {
                        Direction = targetDirection;
                }
            }
            base.MakeAStep();
        }
    }
}
