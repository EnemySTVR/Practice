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
        private readonly Bitmap leftStepSprite = Properties.Resources.tank_left;
        private readonly Bitmap upStepSprite = Properties.Resources.tank_up;
        private readonly Bitmap rightStepSprite = Properties.Resources.tank_right;
        private readonly Bitmap downStepSprite = Properties.Resources.tank_down;
        private int changeDirectionDelay;
        private int changeDirectionCounter  = 0;
        private Random random = new Random();
        private int shotDelay;
        private int shotTimeCounter = 0;

        public bool ReadyToShot
        {
            get
            {
                if (shotTimeCounter == shotDelay)
                {
                    shotDelay = random.Next(30, 150);
                    shotTimeCounter = 0;
                    return true;
                }
                else
                {
                    shotTimeCounter++;
                    return false;
                }
            }
        }
        public Tank() : base()
        {
            shotDelay = random.Next(30, 100);
            changeDirectionDelay = random.Next(10, 30);
            SetRandomDirection();
        }
        public Tank(Point coordinates) : base(coordinates)
        {
            shotDelay = random.Next(1000, 3000);
            SetRandomDirection();
            changeDirectionCounter = 0;
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
            if ((direction == Direction.Down || direction == Direction.Up) && Coordinates.Y % Sprite.Size.Height == 0)
            {
                changeDirectionCounter++;
            }
            if ((direction == Direction.Left || direction == Direction.Right) && Coordinates.X % Sprite.Size.Width == 0)
            {
                changeDirectionCounter++;
            }
            if (changeDirectionCounter == changeDirectionDelay)
            {
                SetRandomDirection();
                changeDirectionDelay = random.Next(1, 10);
                changeDirectionCounter = 0;
            }
            base.MakeAStep();
        }

        public void SetRandomDirection()
        {
            var randomDirection = random.Next(0, 4) switch
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
