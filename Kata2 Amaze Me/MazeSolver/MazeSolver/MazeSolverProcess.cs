using System;
using MazeSolver.Factories;

namespace MazeSolver
{
    public class MazeSolverProcess
    {

        public event Action<GameState> MazeStateChangeEvent;

        protected void OnMazeStateChange(
           GameState gameState
        )
        {
            if (MazeStateChangeEvent != null)
            {
                MazeStateChangeEvent(gameState);
            }
        }


        public void SolveMaze(string[] lines, IWalkTheMaze mazeWalker)         {

            var maze = CreateMazeFactory.FromLines(lines);
            mazeWalker.SetMazeGrid(maze);

//            maze.ConsoleOutMaze();

            var gameState = new GameState(0, mazeWalker.CurrentPosition);

            bool endOfMazeReached = false;

            while (!endOfMazeReached)
            {
                mazeWalker.MakeMove();
                gameState = new GameState(gameState.MoveCounter+1, mazeWalker.CurrentPosition);
                OnMazeStateChange(gameState);
                endOfMazeReached = maze.AtFinish(mazeWalker.CurrentPosition);
            }
        }

    }
}
