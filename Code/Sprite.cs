using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace SnakeWPF.Code
{
    class Sprite
    {
        /// <summary>
        /// Index of current frameIndex
        /// </summary>
        private int _index;
        /// <summary>
        /// Path to sprite map
        /// </summary>
        private Uri _spritemapPath;
        /// <summary>
        /// X,Y coordinates of first frame
        /// </summary>
        private DirPoint _framePosition;
        /// <summary>
        /// Array with indexes of frames animation order
        /// </summary>
        private int[] _framesAnimation;
        /// <summary>
        /// Size of frameIndex in pix.
        /// </summary>
        private Size _frameSizePix;
        /// <summary>
        /// Animation speed
        /// </summary>
        private int _speed;
        /// <summary>
        /// Is animation order is horizontal on sprite map
        /// </summary>
        private bool _isHorizDir;
        /// <summary>
        /// If false, animation have one cycle
        /// </summary>
        private bool _repeat;

        /// <summary>
        /// Create animated sprite from sprite map
        /// </summary>
        /// <param name="mapPath">Path to sprite map</param>
        /// <param name="framePosition">X,Y coordinates of first frame</param>
        /// <param name="framesAnimation">Array with indexes of frames animation order</param>
        /// <param name="frameSizePix">Size of frameIndex in pix.</param>
        /// <param name="speed">Animation speed</param>
        /// <param name="isHorizDir">Is animation order is horizontal on sprite map</param>
        /// <param name="repeat">If false, animation have one cycle</param>
        public Sprite(Uri mapPath, DirPoint framePosition, int[] framesAnimation, Size frameSizePix, int speed, bool isHorizDir, bool repeat)
        {
            _index = 0;
            _spritemapPath = mapPath;
            _framePosition = framePosition;
            _framesAnimation = framesAnimation;
            _frameSizePix = frameSizePix;
            _speed = speed;
            _isHorizDir = isHorizDir;
            _repeat = repeat;
        }

        public int Index { get { return _index; } }
        public Uri Url { get { return _spritemapPath; } set { _spritemapPath = value; } }
        public DirPoint Position { get { return _framePosition; } set { _framePosition = value; } }
        public int[] FramesAnimation { get { return _framesAnimation; } set { _framesAnimation = value; } }
        public Size FrameSize { get { return _frameSizePix; } set { _frameSizePix = value; } }
        public int Speed { get { return _speed; } set { _speed = value; } }
        public bool IsHorizontalDirection { get { return _isHorizDir; } set { _isHorizDir = value; } }
        public bool Repeat { get { return _repeat; } set { _repeat = value; } }
        
        public void Update()
        {
            _index += _speed;
        }

        public void Update(int dt)
        {
            _index += _speed * dt;
        }

        public BitmapSource GetRenderedImage()
        {
            int frameIndex;

            if (_speed > 0)
            {
                var max = _framesAnimation.Length;
                frameIndex = _framesAnimation[_index % max];
            }
            else
            {
                frameIndex = 0;
            }

            var x = _framePosition.X;
            var y = _framePosition.Y;
            // Why +1 and +frameIndex? Map have borders: 1pix border, 16pix frame [1|16|1|16|1] 
            if (_isHorizDir)
            {
                x += frameIndex * _frameSizePix.Width + frameIndex; 
            }
            else
            {
                y += frameIndex * _frameSizePix.Height + frameIndex;
            }

            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = _spritemapPath;
            bi.EndInit();
            int bytesPerPix = bi.Format.BitsPerPixel / 8;
            int stride = bi.PixelWidth * bytesPerPix;

            var pixelBuffer = new byte[bi.PixelHeight * stride];

            bi.CopyPixels(new System.Windows.Int32Rect(Convert.ToInt32(x), Convert.ToInt32(y),
                              Convert.ToInt32(_frameSizePix.Width), Convert.ToInt32(_frameSizePix.Height)),
                          pixelBuffer, stride, 0);

            //bi.SourceRect = new System.Windows.Int32Rect(Convert.ToInt32(x), Convert.ToInt32(y), 
            //    Convert.ToInt32(_frameSizePix.Width), Convert.ToInt32(_frameSizePix.Height));

            var result = BitmapImage.Create(Convert.ToInt32(_frameSizePix.Width), Convert.ToInt32(_frameSizePix.Width),
                bi.DpiX, bi.DpiY, bi.Format, bi.Palette, pixelBuffer, stride);

            return result;
        }
    }
}
