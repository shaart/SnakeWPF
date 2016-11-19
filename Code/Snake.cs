using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWPF.Code
{
    public enum SnakeDirection : short {
        UP, RIGHT, DOWN, LEFT,
        LEFT_TO_UP, LEFT_TO_DOWN,
        RIGHT_TO_UP, RIGHT_TO_DOWN,
        UP_TO_LEFT, UP_TO_RIGHT,
        DOWN_TO_LEFT, DOWN_TO_RIGHT };

    class Snake
    {
        private DirPoint _head, _tail;
        private List<DirPoint> _body;

        public Snake()
        {
            _head = new DirPoint(0, 0);
            _tail = new DirPoint(-1, 0);
            _body = new List<DirPoint>();
        }

        public Snake(DirPoint head, DirPoint tail, List<DirPoint> body)
        {
            _head = head;
            _tail = tail;
            _body = body;
        }

        public Snake(DirPoint head, DirPoint tail)
        {
            _head = head;
            _tail = tail;
            _body = new List<DirPoint>();
        }

        public DirPoint Head { get { return _head; } set { _head = value; } }
        public DirPoint Tail { get { return _tail; } set { _tail = value; } }
        public SnakeDirection Direction { get { return _head.Direction; } set { _head.Direction = value; } }
        public List<DirPoint> BodyPoints { get { return _body; } set { _body = value; } }
        public int BodyLength { get { return _body.Count; } }

        public void AddBodyPointToEnd()
        {
            // Copy last point
            var newPoint = new DirPoint();
            newPoint.X = _body.Last<DirPoint>().X;
            newPoint.Y = _body.Last<DirPoint>().Y;
            newPoint.Direction = _body.Last<DirPoint>().Direction;
            // And add to the end
            _body.Add(newPoint);
        }

        public void Move(int distance, Size field, Size headSize)
        {
            var tempPoint = new DirPoint();
            tempPoint.X = _head.X;
            tempPoint.Y = _head.Y;
            tempPoint.Direction = _head.Direction;
            if (_body[0].Direction != _head.Direction)
            {
                switch (_body[0].Direction)
                {
                    case SnakeDirection.UP_TO_RIGHT:
                    case SnakeDirection.DOWN_TO_RIGHT:
                    case SnakeDirection.RIGHT:
                        if (tempPoint.Direction == SnakeDirection.UP)
                        {
                            tempPoint.Direction = SnakeDirection.RIGHT_TO_UP;
                        }
                        else if (tempPoint.Direction == SnakeDirection.RIGHT)
                        {
                            tempPoint.Direction = SnakeDirection.RIGHT;
                        }
                        else
                        {
                            tempPoint.Direction = SnakeDirection.RIGHT_TO_DOWN;
                        }
                        break;
                    case SnakeDirection.LEFT_TO_DOWN:
                    case SnakeDirection.RIGHT_TO_DOWN:
                    case SnakeDirection.DOWN:
                        if (tempPoint.Direction == SnakeDirection.LEFT)
                        {
                            tempPoint.Direction = SnakeDirection.DOWN_TO_LEFT;
                        }
                        else if (tempPoint.Direction == SnakeDirection.DOWN)
                        {
                            tempPoint.Direction = SnakeDirection.DOWN;
                        }
                        else
                        {
                            tempPoint.Direction = SnakeDirection.DOWN_TO_RIGHT;
                        }
                        break;
                    case SnakeDirection.UP_TO_LEFT:
                    case SnakeDirection.DOWN_TO_LEFT:
                    case SnakeDirection.LEFT:
                        if (tempPoint.Direction == SnakeDirection.UP)
                        {
                            tempPoint.Direction = SnakeDirection.LEFT_TO_UP;
                        }
                        else if (tempPoint.Direction == SnakeDirection.LEFT)
                        {
                            tempPoint.Direction = SnakeDirection.LEFT;
                        }
                        else
                        {
                            tempPoint.Direction = SnakeDirection.LEFT_TO_DOWN;
                        }
                        break;
                    case SnakeDirection.LEFT_TO_UP:
                    case SnakeDirection.RIGHT_TO_UP:
                    case SnakeDirection.UP:
                        if (tempPoint.Direction == SnakeDirection.LEFT)
                        {
                            tempPoint.Direction = SnakeDirection.UP_TO_LEFT;
                        }
                        else if (tempPoint.Direction == SnakeDirection.UP)
                        {
                            tempPoint.Direction = SnakeDirection.UP;
                        }
                        else
                        {
                            tempPoint.Direction = SnakeDirection.UP_TO_RIGHT;
                        }
                        break;
                    default:
                        break;
                }
            }
            _tail = _body.Last<DirPoint>();             // Tail = last body point
            switch (_tail.Direction)
            {
                case SnakeDirection.UP_TO_LEFT:
                case SnakeDirection.UP_TO_RIGHT:
                case SnakeDirection.DOWN_TO_LEFT:
                case SnakeDirection.DOWN_TO_RIGHT:
                case SnakeDirection.LEFT_TO_UP:
                case SnakeDirection.LEFT_TO_DOWN:
                case SnakeDirection.RIGHT_TO_UP:
                case SnakeDirection.RIGHT_TO_DOWN:
                    if (_body.Count < 2)
                    {
                        _tail.Direction = _head.Direction; // Pre-last point direction
                    }
                    else
                    {
                        _tail.Direction = _body[_body.Count - 2].Direction; // Pre-last point direction
                    }
                    break;
                default:
                    break;
            }
            _body.RemoveAt(_body.Count - 1);            // Remove last
            _body.Insert(0, tempPoint);                 // Old head position = first body point
            // Make new head
            switch (_head.Direction)
            {
                case SnakeDirection.UP:
                    _head.Y -= distance;
                    if (_head.Y - (headSize.Height / 2) < 0)
                    {
                        _head.Y = field.Height - headSize.Height;
                    }
                    break;
                case SnakeDirection.RIGHT:
                    _head.X += distance;
                    if (_head.X + (headSize.Width / 2) > field.Width)
                    {
                        _head.X = (headSize.Width / 2);
                    }
                    break;
                case SnakeDirection.DOWN:
                    _head.Y += distance;
                    if (_head.Y + (headSize.Height / 2) > field.Height)
                    {
                        _head.Y = headSize.Height / 2;
                    }
                    break;
                case SnakeDirection.LEFT:
                    _head.X -= distance;
                    if (_head.X - (headSize.Width / 2) < 0)
                    {
                        _head.X = field.Width - headSize.Width;
                    }
                    break;
                default:
                    break;
            }
        }
        /*
        /// <summary>
        /// OLD METHOD
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <param name="field"></param>
        private void MoveTo(SnakeDirection direction, int distance, Size field)
        {
            throw new Exception("OLD METHOD");

            var tempPoint = new DirPoint();
            tempPoint.X = _head.X;
            tempPoint.Y = _head.Y;
            tempPoint.Direction = _head.Direction;
            _body.Insert(0, tempPoint);             // Old head position = first body point
            // Make new head
            switch (direction)
            {
                case SnakeDirection.UP:
                    _head.Y -= distance;
                    if (_head.Y <= 0)
                    {
                        _head.Y = field.Height;
                    }
                    break;
                case SnakeDirection.RIGHT:
                    _head.X += distance;
                    if (_head.X >= field.Width)
                    {
                        _head.X = 0;
                    }
                    break;
                case SnakeDirection.DOWN:
                    _head.Y += distance;
                    if (_head.Y >= field.Height)
                    {
                        _head.Y = 0;
                    }
                    break;
                case SnakeDirection.LEFT:
                    _head.X -= distance;
                    if (_head.X <= 0)
                    {
                        _head.X = field.Width;
                    }
                    break;
                default:
                    break;
            }
            _tail = _body.Last<DirPoint>();     // Tail = last body point
            _body.RemoveAt(_body.Count - 1);    // Remove last
        }
        */
    }
}
