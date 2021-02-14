using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Tank : MobileEntity
    {
        private readonly Bitmap leftStepSprite = new Bitmap(@"..\..\..\img\tank_left.png");
        private readonly Bitmap upStepSprite = new Bitmap(@"..\..\..\img\tank_up.png");
        private readonly Bitmap rightStepSprite = new Bitmap(@"..\..\..\img\tank_right.png");
        private readonly Bitmap downStepSprite = new Bitmap(@"..\..\..\img\tank_down.png");
        private int stepCounter;
        public Tank() : base()
        {
            SetRandomDirection();
            stepCounter = 0;
        }
        public Tank(Point coordinates) : base(coordinates)
        {
            SetRandomDirection();
            stepCounter = 0;
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
            stepCounter++;
            if (stepCounter == 15)
            {
                SetRandomDirection();
                stepCounter = 0;
            }
            
        }

        public void SetRandomDirection()
        {
            int random = new Random().Next(0, 4);
            var randomDirection = random switch
            {
                0 => Direction.Left,
                1 => Direction.Up,
                2 => Direction.Right,
                3 => Direction.Down,
                _ => Direction.Right,
            };
            Direction = randomDirection;
        }

        public void TurnAround()
        {
            switch (Direction)
            {
                case Direction.Left:
                    Direction = Direction.Right;
                    break;
                case Direction.Up:
                    Direction = Direction.Down;
                    break;
                case Direction.Right:
                    Direction = Direction.Left;
                    break;
                case Direction.Down:
                    Direction = Direction.Up;
                    break;
            }
        }
    }
}
