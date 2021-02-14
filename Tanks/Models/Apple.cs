using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Apple : Entity
    {
        public Apple() : base()
        {
            Sprite = new Bitmap(@"..\\..\\..\\img\\apple.png");
        }
        public Apple(Point coordinates) : base(coordinates)
        {
            Sprite = new Bitmap(@"..\\..\\..\\img\\apple.png");
        }
    }
}
