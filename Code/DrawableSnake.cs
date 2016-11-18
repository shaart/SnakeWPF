using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;

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
            Head = head;
            Tail = tail;
            BodyPoints = body;
            // Head
            _spriteHead = new Sprite(spriteMapUrl,
                new DirPoint(1, 35),                // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
            // Body
            _spriteBody = new Sprite(spriteMapUrl,
                new DirPoint(1, 1),                 // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
            _spriteBodyRotate = new Sprite(spriteMapUrl,
                new DirPoint(1, 18),                // Top left coords
                new int[] { 0 },                    // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                false);                             // Repeat
            // Tail
            _spriteTail = new Sprite(spriteMapUrl,
                new DirPoint(1, 52),                // Top left coords
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
                new DirPoint(1, 35),                // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
            // Body
            _spriteBody = new Sprite(spriteMapUrl,
                new DirPoint(1, 1),                 // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat                      
            _spriteBodyRotate = new Sprite(spriteMapUrl,
                new DirPoint(1, 18),                // Top left coords
                new int[] { 0 },                    // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
            // Tail
            _spriteTail = new Sprite(spriteMapUrl,
                new DirPoint(1, 52),                // Top left coords
                new int[] { 0, 1, 2, 1, 0 },        // Animation
                new Size(16, 16),                   // Frame size
                animationSpeed,                     // Speed
                true,                               // Horizontal animation
                true);                              // Repeat
        }

        public Image GetHeadImage()
        {
            const double half = 0.5;
            var rotate = new RotateTransform(0);
            var snakeHead = new Image();
            this.HeadSprite.Update();
            snakeHead.Source = this.HeadSprite.GetRenderedImage();
            switch (Head.Direction)
            {
                case SnakeDirection.UP:
                    rotate = new RotateTransform(270);
                    break;
                case SnakeDirection.RIGHT:
                    rotate = new RotateTransform(0);
                    break;
                case SnakeDirection.DOWN:
                    rotate = new RotateTransform(90);
                    break;
                case SnakeDirection.LEFT:
                    rotate = new RotateTransform(180);
                    break;
                default:
                    break;
            }
            snakeHead.RenderTransform = rotate;
            snakeHead.RenderTransformOrigin = new System.Windows.Point(half, half);

            return snakeHead;
        }

        public List<Image> GetBodyImages()
        {
            const double half = 0.5;
            var rotate = new RotateTransform(0);
            var snakeBody = new List<Image>();

            for (int i = 0; i < BodyLength; i++)
            {
                snakeBody.Add(new Image());
                BodySprite.Update();
                BodyRotateSprite.Update();
                switch (BodyPoints[i].Direction)
                {
                    case SnakeDirection.UP:
                        snakeBody[i].Source = BodySprite.GetRenderedImage();
                        rotate = new RotateTransform(270);
                        break;
                    case SnakeDirection.RIGHT:
                        snakeBody[i].Source = BodySprite.GetRenderedImage();
                        rotate = new RotateTransform(0);
                        break;
                    case SnakeDirection.DOWN:
                        snakeBody[i].Source = BodySprite.GetRenderedImage();
                        rotate = new RotateTransform(90);
                        break;
                    case SnakeDirection.LEFT:
                        snakeBody[i].Source = BodySprite.GetRenderedImage();
                        rotate = new RotateTransform(180);
                        break;
                    case SnakeDirection.RIGHT_TO_DOWN:
                    case SnakeDirection.UP_TO_LEFT:
                        snakeBody[i].Source = BodyRotateSprite.GetRenderedImage();
                        rotate = new RotateTransform(0);
                        break;
                    case SnakeDirection.UP_TO_RIGHT:
                    case SnakeDirection.LEFT_TO_DOWN:
                        snakeBody[i].Source = BodyRotateSprite.GetRenderedImage();
                        rotate = new RotateTransform(270);
                        break;
                    case SnakeDirection.DOWN_TO_LEFT:
                    case SnakeDirection.RIGHT_TO_UP:
                        snakeBody[i].Source = BodyRotateSprite.GetRenderedImage();
                        rotate = new RotateTransform(90);
                        break;
                    case SnakeDirection.DOWN_TO_RIGHT:
                    case SnakeDirection.LEFT_TO_UP:
                        snakeBody[i].Source = BodyRotateSprite.GetRenderedImage();
                        rotate = new RotateTransform(180);
                        break;
                    default:
                        break;
                }
                snakeBody[i].RenderTransform = rotate;
                snakeBody[i].RenderTransformOrigin = new System.Windows.Point(half, half);
            }
            return snakeBody;
        }

        public Image GetTailImage()
        {
            const double half = 0.5;
            var rotate = new RotateTransform(0);
            var snakeTail = new Image();
            this.TailSprite.Update();
            snakeTail.Source = TailSprite.GetRenderedImage();
            switch (Tail.Direction)
            {
                case SnakeDirection.UP:
                    rotate = new RotateTransform(270);
                    break;
                case SnakeDirection.RIGHT:
                    rotate = new RotateTransform(0);
                    break;
                case SnakeDirection.DOWN:
                    rotate = new RotateTransform(90);
                    break;
                case SnakeDirection.LEFT:
                    rotate = new RotateTransform(180);
                    break;
                default:
                    break;
            }
            snakeTail.RenderTransform = rotate;
            snakeTail.RenderTransformOrigin = new System.Windows.Point(half, half);

            return snakeTail;
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
