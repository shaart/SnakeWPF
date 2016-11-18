using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWPF.Code
{
    public enum SnakeDirection : short { UP, RIGHT, DOWN, LEFT, UP_LEFT, UP_RIGHT,  DOWN_RIGHT, DOWN_LEFT };

    class Snake
    {
        //private SnakeDirection _headDirection;
        private DirPoint _head, _tail;
        private List<DirPoint> _body;

        public Snake()
        {
            //_headDirection = SnakeDirection.RIGHT;
            _head = new DirPoint(0, 0);
            _tail = new DirPoint(-1, 0);
            _body = new List<DirPoint>();
        }

        public Snake(DirPoint head, DirPoint tail, List<DirPoint> body)
        {
            //_headDirection = SnakeDirection.RIGHT;
            _head = head;
            _tail = tail;
            _body = body;
        }

        public Snake(DirPoint head, DirPoint tail)
        {
            //_headDirection = SnakeDirection.RIGHT;
            _head = head;
            _tail = tail;
            _body = new List<DirPoint>();
        }

        public DirPoint Head { get { return _head; } set { _head = value; } }
        public DirPoint Tail { get { return _tail; } set { _tail = value; } }
        public SnakeDirection Direction { get { return _head.Direction; } set { _head.Direction = value; } }
        public List<DirPoint> BodyPoints { get { return _body; } set { _body = value; } }
        public int BodyLength { get { return _body.Count; } }

        public void Move(int distance, Size field)
        {
            var tempPoint = new DirPoint();
            tempPoint.X = _head.X;
            tempPoint.Y = _head.Y;
            tempPoint.Direction = _head.Direction;
            if (_body[0].Direction != _head.Direction)
            {
                switch (_body[0].Direction)
                {
                    case SnakeDirection.RIGHT:
                        if (tempPoint.Direction == SnakeDirection.UP)
                        {
                            tempPoint.Direction = SnakeDirection.UP_RIGHT;
                        }
                        else
                        {
                            tempPoint.Direction = SnakeDirection.DOWN_RIGHT;
                        }
                        break;
                    case SnakeDirection.DOWN:
                        if (tempPoint.Direction == SnakeDirection.LEFT)
                        {
                            tempPoint.Direction = SnakeDirection.DOWN_LEFT;
                        }
                        else
                        {
                            tempPoint.Direction = SnakeDirection.DOWN_RIGHT;
                        }
                        break;
                    case SnakeDirection.LEFT:
                        if (tempPoint.Direction == SnakeDirection.UP)
                        {
                            tempPoint.Direction = SnakeDirection.UP_LEFT;
                        }
                        else
                        {
                            tempPoint.Direction = SnakeDirection.DOWN_LEFT;
                        }
                        break;
                    case SnakeDirection.UP:
                        if (tempPoint.Direction == SnakeDirection.LEFT)
                        {
                            tempPoint.Direction = SnakeDirection.UP_LEFT;
                        }
                        else
                        {
                            tempPoint.Direction = SnakeDirection.UP_RIGHT;
                        }
                        break;
                    default:
                        break;
                }
            }
            _tail = _body.Last<DirPoint>();             // Tail = last body point
            switch (_tail.Direction)
            {
                case SnakeDirection.UP_LEFT:
                case SnakeDirection.UP_RIGHT:
                case SnakeDirection.DOWN_LEFT:
                case SnakeDirection.DOWN_RIGHT:
                    _tail.Direction = _body[_body.Count - 2].Direction; // Pre-last point direction
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
        }

        public void MoveTo(SnakeDirection direction, int distance, Size field)
        {
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
            _tail = _body.Last<DirPoint>();        // Tail = last body point
            _body.RemoveAt(_body.Count - 1);    // Remove last
        }
    }
}
