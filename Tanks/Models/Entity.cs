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
        private static int idGenerator = 0;

        public int Id { get; private set; }

        public Point Coordinates;

        public virtual Bitmap Sprite { get; set; }

        public Entity()
        {
            Id = idGenerator;
            Coordinates = new Point(0, 0);
            idGenerator++;
        }

        public Entity(Point coordinates)
        {
            Id = idGenerator;
            Coordinates = coordinates;
            idGenerator++;
        }
    }
}
