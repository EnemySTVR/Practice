using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Models
{
    class Lazer
    {
        private int counter;
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        private Color _color;
        public Color Color
        {
            get
            {
                counter++;
                return counter switch
                {
                    1 => Color.FromArgb(90, 200, 250),
                    2 => Color.FromArgb(20, 20, 250),
                    _ => Color.Black,
                };
            }
            set
            {
                _color = value;
            }
        }

        public Lazer(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            counter = 0;
        }
    }
}
