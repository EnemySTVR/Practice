using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class MobileEntity : Entity
    {
        public MobileEntity() : base() { }
        public MobileEntity(Point coordinates) : base(coordinates) { }

        private Direction direction;

        public virtual Direction Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }
        internal virtual void MakeAStep()
        {
            switch (direction)
            {
                case Direction.Left:
                    Coordinates.X--;
                    break;
                case Direction.Up:
                    Coordinates.Y--;
                    break;
                case Direction.Right:
                    Coordinates.X++;
                    break;
                case Direction.Down:
                    Coordinates.Y++;
                    break;
            }
        }

        internal void GoBack()
        {
            switch (direction)
            {
                case Direction.Left:
                    Coordinates.X++;
                    break;
                case Direction.Up:
                    Coordinates.Y++;
                    break;
                case Direction.Right:
                    Coordinates.X--;
                    break;
                case Direction.Down:
                    Coordinates.Y--;
                    break;
            }
        }
    }
}
