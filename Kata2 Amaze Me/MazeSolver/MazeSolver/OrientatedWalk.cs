namespace MazeSolver
{
    public abstract class OrientatedWalk
    {
        public abstract OrientatedWalk TurnRight();
        public abstract OrientatedWalk TurnLeft();
        public abstract Point GetDesiredForwardPosition(Point currentPosition);
        public abstract bool CanSeeLeftTurning(Point currentPosition, MazeGrid grid);
        public abstract bool CanSeeRightTurning(Point currentPosition, MazeGrid grid);
        public abstract Point GetDesiredRightTurnPosition(MazeGrid mazeGrid, Point currentPosition);
        public bool CanMoveForward(MazeGrid grid, Point currentPosition)
        {
            var deiredPos = GetDesiredForwardPosition(currentPosition);
            if(grid.IsValidPosition(deiredPos) == false)
            {
                return false;
            }
            return grid.Grid[deiredPos.Y][deiredPos.X].IsPath;
        }

    }

    public class NorthOrientatedWalk : OrientatedWalk
    {

        public override bool CanSeeLeftTurning(Point currentPosition, MazeGrid grid)
        {
            return grid.Grid[currentPosition.Y][currentPosition.X - 1].IsPath;
        }

        public override bool CanSeeRightTurning(Point currentPosition, MazeGrid grid)
        {
            return grid.Grid[currentPosition.Y][currentPosition.X + 1].IsPath;
        }

        public override Point GetDesiredRightTurnPosition(MazeGrid mazeGrid, Point currentPosition)
        {
            return new Point(currentPosition.Y,currentPosition.X + 1);
        }

        public override OrientatedWalk TurnRight()
        {
            return new EastOrientatedWalk();
        }

        public override OrientatedWalk TurnLeft()
        {
            return new WestOrientatedWalk();
        }

        public override Point GetDesiredForwardPosition(Point currentPosition)
        {
            return new Point(currentPosition.Y - 1, currentPosition.X);
        }

    }

    public class SouthOrientatedWalk : OrientatedWalk
    {

        public override bool CanSeeLeftTurning(Point currentPosition, MazeGrid grid)
        {
            return grid.Grid[currentPosition.Y][currentPosition.X + 1].IsPath;
        }

        public override bool CanSeeRightTurning(Point currentPosition, MazeGrid grid)
        {
            return grid.Grid[currentPosition.Y][currentPosition.X - 1].IsPath;
        }

        public override Point GetDesiredRightTurnPosition(MazeGrid mazeGrid, Point currentPosition)
        {
            return new Point(currentPosition.Y, currentPosition.X - 1);
        }

        public override OrientatedWalk TurnRight()
        {
            return new WestOrientatedWalk();
        }

        public override OrientatedWalk TurnLeft()
        {
            return new EastOrientatedWalk();
        }

        public override Point GetDesiredForwardPosition(Point currentPosition)
        {
            return new Point(currentPosition.Y + 1, currentPosition.X);
        }

    }

    public class EastOrientatedWalk : OrientatedWalk
    {
        public override bool CanSeeLeftTurning(Point currentPosition, MazeGrid grid)
        {
            return grid.Grid[currentPosition.Y - 1][currentPosition.X].IsPath;
        }

        public override bool CanSeeRightTurning(Point currentPosition, MazeGrid grid)
        {
            return grid.Grid[currentPosition.Y + 1][currentPosition.X].IsPath;
        }

        public override Point GetDesiredRightTurnPosition(MazeGrid mazeGrid, Point currentPosition)
        {
            return new Point(currentPosition.Y + 1, currentPosition.X);
        }

        public override OrientatedWalk TurnRight()
        {
            return new SouthOrientatedWalk();
        }

        public override OrientatedWalk TurnLeft()
        {
            return new NorthOrientatedWalk();
        }

        public override Point GetDesiredForwardPosition(Point currentPosition)
        {
            return new Point(currentPosition.Y, currentPosition.X + 1);
        }
    }

    public class WestOrientatedWalk : OrientatedWalk
    {
        public override bool CanSeeLeftTurning(Point currentPosition, MazeGrid grid)
        {
            return grid.Grid[currentPosition.Y + 1][currentPosition.X].IsPath;
        }

        public override bool CanSeeRightTurning(Point currentPosition, MazeGrid grid)
        {
            return grid.Grid[currentPosition.Y - 1][currentPosition.X].IsPath;
        }

        public override Point GetDesiredRightTurnPosition(MazeGrid mazeGrid, Point currentPosition)
        {
            return new Point(currentPosition.Y - 1, currentPosition.X);
        }

        public override OrientatedWalk TurnRight()
        {
            return new NorthOrientatedWalk();
        }

        public override OrientatedWalk TurnLeft()
        {
            return new SouthOrientatedWalk();
        }

        public override Point GetDesiredForwardPosition(Point currentPosition)
        {
            return new Point(currentPosition.Y, currentPosition.X - 1);
        }
    } 

}