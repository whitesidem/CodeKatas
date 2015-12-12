using System;

namespace MazeSolver
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point sourcePoint)
        {
            X = sourcePoint.X;
            Y = sourcePoint.Y;
        }

        public override string ToString()
        {
            return String.Format("Point({0}, {1})", X, Y);
        }
    }
}