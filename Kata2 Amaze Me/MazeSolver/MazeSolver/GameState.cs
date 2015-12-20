using System;

namespace MazeSolver
{
    public class GameState
    {
        public int MoveCounter { get; private set; }   
        public Point CurrentPosition { get; private set; }

        public GameState(int moveCounter, Point currentPosition)
        {
            MoveCounter = moveCounter;
            CurrentPosition = currentPosition;
        }

        public override string ToString()
        {
            return string.Format("Move: {0}, YPos = {1}, Xpos = {2}", MoveCounter, CurrentPosition.Y, CurrentPosition.X);
        }
    }
}