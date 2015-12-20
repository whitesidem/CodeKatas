using System.Collections.Generic;
using System.Linq;

namespace MazeSolver
{
    public class CleverMazeWalker : IWalkTheMaze
    {
        private  MazeGrid _mazeGrid;
        private OrientatedWalk _orientatedWalk;
        public Point CurrentPosition { get; set; }
        private List<Point> _visitedPositions = new List<Point>();


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

            if(CanTurnRight())
            {
                var desiredRightTurnPosition = new Point(_orientatedWalk.GetDesiredRightTurnPosition(_mazeGrid, CurrentPosition));
                if (IsVisitedPoint(desiredRightTurnPosition)==false)
                {
                    TurnRight();
                    return;
                }
            }
            
            TurnLeftIfPossible();
        }

        private bool IsVisitedPoint(Point desiredPoint)
        {
            return _visitedPositions.Any( p => p.X == desiredPoint.X && p.Y == desiredPoint.Y);
        }


        private bool CanTurnLeft()
        {
            return _orientatedWalk.CanSeeLeftTurning(CurrentPosition, _mazeGrid);
        }

        private bool CanTurnRight()
        {
            return _orientatedWalk.CanSeeRightTurning(CurrentPosition, _mazeGrid);
        }

        private void TurnLeft()
        {
            _orientatedWalk = _orientatedWalk.TurnLeft();
        }

        private void TurnRight()
        {
            _orientatedWalk = _orientatedWalk.TurnRight();
        }

        private bool CanMoveForward()
        {
            return _orientatedWalk.CanMoveForward(_mazeGrid, CurrentPosition);
        }

        private void MoveForward()
        {
            CurrentPosition = new Point(_orientatedWalk.GetDesiredForwardPosition(CurrentPosition));
            _visitedPositions.Add(CurrentPosition);
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