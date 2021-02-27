using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Explosion : Entity
    {
        private readonly Bitmap smallExplosion = Properties.Resources.smallExplosion;
        private readonly Bitmap mediumExplosion = Properties.Resources.mediumExplosion;
        private readonly Bitmap bigExplosion = Properties.Resources.bigExplosion;
        private int counter;

        public override Bitmap Sprite
        {
            get
            {
                counter++;
                return counter switch
                {
                    0 => smallExplosion,
                    1 => mediumExplosion,
                    2 => bigExplosion,
                    _ => null,
                };
            }
            set => base.Sprite = value;
        }
        public Explosion() : base()
        {
            counter = -1;
        }
        public Explosion(Point coordinates) : base(coordinates)
        {
            counter = 0;
        }
    }
}
