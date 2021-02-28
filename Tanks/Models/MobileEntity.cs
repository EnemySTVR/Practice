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
        protected int step = 1;
        public MobileEntity() : base() { }
        public MobileEntity(Point coordinates) : base(coordinates) { }

        protected Direction direction;

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
                    Coordinates.X -= step;
                    break;
                case Direction.Up:
                    Coordinates.Y -= step;
                    break;
                case Direction.Right:
                    Coordinates.X += step;
                    break;
                case Direction.Down:
                    Coordinates.Y += step;
                    break;
            }
        }

        internal void GoBack()
        {
            switch (direction)
            {
                case Direction.Left:
                    Coordinates.X += step;
                    break;
                case Direction.Up:
                    Coordinates.Y += step;
                    break;
                case Direction.Right:
                    Coordinates.X -= step;
                    break;
                case Direction.Down:
                    Coordinates.Y -= step;
                    break;
            }
        }
    }
}
