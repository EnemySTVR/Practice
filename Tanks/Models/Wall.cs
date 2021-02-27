using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Wall : Entity
    {
        public Wall() : base()
        {
            Sprite = Properties.Resources.wall;
        }
        public Wall(Point coordinates) : base(coordinates)
        {
            Sprite = Properties.Resources.wall;
        }
    }
}
