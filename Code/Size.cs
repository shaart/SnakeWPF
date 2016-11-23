using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWPF.Code
{
    class Size
    {
        private double _height, _width;

        public Size()
        {
            _height = _width = 0;
        }

        public Size(double width, double height)
        {
            _width = width;
            _height = height;
        }

        public double Height { get { return _height; } set { _height = value; } }
        public double Width { get { return _width; } set { _width = value; } }
    }
}
