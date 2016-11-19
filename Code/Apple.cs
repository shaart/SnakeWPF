using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace SnakeWPF.Code
{
    class Apple
    {
        private Sprite _appleSprite;
        double _x, _y;

        public Sprite Sprite { get { return _appleSprite; } }
        public double X { get { return _x; } set { _x = value; } }
        public double Y { get { return _y; } set { _y = value; } }

        public Apple(double x, double y, Uri spriteMapUrl, int animationSpeed = 1)
        {
            _x = x;
            _y = y;

            Image img = new Image();
            _appleSprite = new Sprite(spriteMapUrl,
                new DirPoint(1, 1+(1+16)*4),          // Top left coords
                //new DirPoint(1, 69),              // Top left coords
                new int[] { 0, 1, 2 },              // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
        }

        public Image GetImage()
        {
            var appleImage = new Image();
            this.Sprite.Update();
            appleImage.Source = this.Sprite.GetRenderedImage();

            return appleImage;
        }
    }
}
