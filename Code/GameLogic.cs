using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnakeWPF.Code
{
    class GameLogic
    {
        public static void CheckSnakeCollisions(DrawableSnake snake, out bool gameOver)
        {
            gameOver = false;
            // Head collided with the tail
            if (snake.Head.X == snake.Tail.X && snake.Head.Y == snake.Tail.Y)
            {
                gameOver = true;
                return;
            }
            for (int i = 0; i < snake.BodyLength; i++)
            {
                if (snake.Head.X == snake.BodyPoints[i].X && snake.Head.Y == snake.BodyPoints[i].Y)
                {
                    gameOver = true;
                    return;
                }
            }
        }

        public static bool IsSnakeAteApple(Snake snake, Apple apple, ref int score, int appleScoreCost = 10)
        {
            bool result = false;
            if (apple.X == snake.Head.X && apple.Y == snake.Head.Y)
            {
                result = true;
                score += appleScoreCost;
            }
            return result;
        }

        public static void MakeNewApple(Snake snake, double areaWidth, double areaHeight, ref Apple apple)
        {
            Point coords;
            Random rand = new Random();
            bool goodPoint;
            for (;;)
            {
                coords = new Point(
                    rand.Next(0, Convert.ToInt32(areaWidth)),   // X
                    rand.Next(0, Convert.ToInt32(areaHeight))); // Y
                goodPoint = true;

                if ((coords.X == snake.Tail.X && coords.Y == snake.Tail.Y)
                    || (coords.X == snake.Head.X && coords.Y == snake.Head.Y))
                {
                    continue;       // Collision, next cycle
                }
                for (int i = 0; i < snake.BodyLength; i++)
                {
                    if (coords.X == snake.BodyPoints[i].X && coords.Y == snake.BodyPoints[i].Y)
                    {
                        goodPoint = false;
                        break;
                    }
                }
                if (goodPoint)
                {
                    break;
                }

            }
            apple.X = coords.X;
            apple.Y = coords.Y;  
        }
    }
}
