using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace SnakeWPF.Code
{
    class DrawableSnake : Snake
    {
        private Sprite _spriteHead;
        private Sprite _spriteBody;
        private Sprite _spriteBodyRotate;
        private Sprite _spriteTail;

        public Sprite HeadSprite { get { return _spriteHead; } }
        public Sprite BodySprite { get { return _spriteBody; } }
        public Sprite BodyRotateSprite { get { return _spriteBodyRotate; } }
        public Sprite TailSprite { get { return _spriteTail; } }

        public DrawableSnake(DirPoint head, DirPoint tail, List<DirPoint> body, Uri spriteMapUrl, 
            int animationSpeed = 1)
        {
            Image img = new Image();
            //_snake = snake;
            Head = head;
            Tail = tail;
            BodyPoints = body;
            // Head
            _spriteHead = new Sprite(spriteMapUrl,
                new DirPoint(1, 35),                   // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
            // Body
            _spriteBody = new Sprite(spriteMapUrl,
                new DirPoint(1, 1),                    // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
            _spriteBodyRotate = new Sprite(spriteMapUrl,
                new DirPoint(1, 18),                   // Top left coords
                new int[] { 0 },                    // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                false);                             // Repeat
            // Tail
            _spriteTail = new Sprite(spriteMapUrl,
                new DirPoint(1, 52),                   // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
        }

        public DrawableSnake(DirPoint head, DirPoint tail, List<DirPoint> body,
            Sprite headSprite, Sprite bodySprite, Sprite bodyRotateSprite, Sprite tailSprite)
        {
            Image img = new Image();
            //_snake = snake;
            Head = head;
            Tail = tail;
            BodyPoints = body;
            // Head
            _spriteHead = headSprite;
            // Body
            _spriteBody = bodySprite;
            _spriteBodyRotate = bodyRotateSprite;
            // Tail
            _spriteTail = tailSprite;
        }

        public DrawableSnake(DirPoint head, DirPoint tail, Uri spriteMapUrl,
            int animationSpeed = 1)
        {
            Image img = new Image();
            //_snake = snake;
            Head = head;
            Tail = tail;
            BodyPoints = new List<DirPoint>();
            // Head
            _spriteHead = new Sprite(spriteMapUrl,
                new DirPoint(1, 35),                   // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                                  // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
            // Body
            _spriteBody = new Sprite(spriteMapUrl,
                new DirPoint(1, 1),                    // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                                  // Speed
                true,                               // Horizontal animation
                true);                              // Repeat                      // Repeat
            _spriteBodyRotate = new Sprite(spriteMapUrl,
                new DirPoint(1, 18),                   // Top left coords
                new int[] { 0 },                    // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                false);                             // Repeat
            // Tail
            _spriteTail = new Sprite(spriteMapUrl,
                new DirPoint(1, 52),                   // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                                  // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
        }

        public void Update(int dt = 1)
        {
            HeadSprite.Update(dt);         
            BodySprite.Update(dt);
            BodyRotateSprite.Update(dt);
            TailSprite.Update(dt);
        }
    }
}
