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
    }
}