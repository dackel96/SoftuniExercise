using System;
using System.Threading;

namespace SnakeCreateFromViktorDakovGuide
{
    class Program
    {
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            snake.SnakeElements.AddFirst(new Node<SnakeElement>(new SnakeElement()
            {
                Character = '#',
                Position = new Position(13, 16)
            }));
            snake.SnakeElements.AddFirst(new Node<SnakeElement>(new SnakeElement()
            {
                Character = '#',
                Position = new Position(12, 16)
            }));
            snake.SnakeElements.AddFirst(new Node<SnakeElement>(new SnakeElement()
            {
                Character = '#',
                Position = new Position(11, 16)
            }));

            Direction direction = Direction.Up;
            while (true)
            {
                direction = ChangeDirection(direction);
                Console.Clear();
                snake.DrawSnake();
                snake.MoveSnake(direction);
                Thread.Sleep(100);
            }
        }

        private static Direction ChangeDirection(Direction direction)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey readKey = Console.ReadKey(false).Key;
                switch (readKey)
                {
                    case ConsoleKey.LeftArrow:
                        return Direction.Left;
                    case ConsoleKey.UpArrow:
                        return Direction.Up;
                    case ConsoleKey.RightArrow:
                        return Direction.Right;
                    case ConsoleKey.DownArrow:
                        return Direction.Down;
                }
            }
            return direction;
        }
    }
}
