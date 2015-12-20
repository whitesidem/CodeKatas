using System;

namespace MazeSolver
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int y, int x)
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
            return String.Format("Point(Y={0}, X={1})",  Y, X);
        }
    }
}