using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class River : Entity
    {
        public River() : base()
        {
            Sprite = Properties.Resources.river;
        }
        public River(Point coordinates) : base(coordinates)
        {
            Sprite = Properties.Resources.river;
        }
    }
}
