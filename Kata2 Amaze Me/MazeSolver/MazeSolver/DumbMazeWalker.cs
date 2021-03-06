﻿namespace MazeSolver
{
    public class DumbMazeWalker : IWalkTheMaze
    {
        private  MazeGrid _mazeGrid;
        private OrientatedWalk _orientatedWalk;
        public Point CurrentPosition { get; set; }

        public void SetMazeGrid(MazeGrid mazegrid)
        {
            _mazeGrid = mazegrid;
            CurrentPosition = _mazeGrid.StartPosition;
            _orientatedWalk = new SouthOrientatedWalk();
        }

        public void MakeMove()
        {
            if (CanMoveForward() == false)
            {
                TurnRight();
                return;
            }
            MoveForward();
            TurnLeftIfPossible();
        }


        private bool CanTurnLeft()
        {
            return _orientatedWalk.CanSeeLeftTurning(CurrentPosition, _mazeGrid);
        }


        private void TurnRight()
        {
            _orientatedWalk = _orientatedWalk.TurnRight();
        }

        private void TurnLeft()
        {
            _orientatedWalk = _orientatedWalk.TurnLeft();
        }

        private bool CanMoveForward()
        {
            return _orientatedWalk.CanMoveForward(_mazeGrid, CurrentPosition);
        }

        private void MoveForward()
        {
            CurrentPosition = new Point(_orientatedWalk.GetDesiredForwardPosition(CurrentPosition));
        }


        private void TurnLeftIfPossible()
        {
            if (CanTurnLeft())
            {
                TurnLeft();
            }
        }

    }
}