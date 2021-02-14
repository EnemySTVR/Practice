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
            Sprite = new Bitmap(@"..\\..\\..\\img\\river.png");
        }
        public River(Point coordinates) : base(coordinates)
        {
            Sprite = new Bitmap(@"..\\..\\..\\img\\river.png");
        }
    }
}
