using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Entity
    {
        public Point Coordinates;
        public virtual Bitmap Sprite { get; set; }

        public Entity()
        {
            Coordinates = new Point(0, 0);
        }

        public Entity(Point coordinates)
        {
            Coordinates = coordinates;
        }
    }
}
