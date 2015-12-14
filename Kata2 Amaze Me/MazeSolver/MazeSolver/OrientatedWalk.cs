namespace MazeSolver
{
    public abstract class OrientatedWalk
    {
        public abstract OrientatedWalk TurnRight();
        public abstract OrientatedWalk TurnLeft();
        public abstract Point GetDesiredForwardPosition(Point currentPosition);
        public abstract bool CanSeeLeftTurning(Point currentPosition, MazeItem[][] grid);
        public bool CanMoveForward(MazeItem[][] grid, Point currentPosition)
        {
            var deiredPos = GetDesiredForwardPosition(currentPosition);
            return grid[deiredPos.Y][deiredPos.X].IsPath;
        }

    }

    public class NorthOrientatedWalk : OrientatedWalk
    {

        public override bool CanSeeLeftTurning(Point currentPosition, MazeItem[][] grid)
        {
            return grid[currentPosition.Y][currentPosition.X - 1].IsPath;
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
            return new Point(currentPosition.X, currentPosition.Y - 1);
        }

    }

    public class SouthOrientatedWalk : OrientatedWalk
    {

        public override bool CanSeeLeftTurning(Point currentPosition, MazeItem[][] grid)
        {
            return grid[currentPosition.Y][currentPosition.X + 1].IsPath;
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
            return new Point(currentPosition.X, currentPosition.Y + 1);
        }

    }

    public class EastOrientatedWalk : OrientatedWalk
    {
        public override bool CanSeeLeftTurning(Point currentPosition, MazeItem[][] grid)
        {
            return grid[currentPosition.Y - 1][currentPosition.X].IsPath;
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
            return new Point(currentPosition.X + 1, currentPosition.Y);
        }
    }

    public class WestOrientatedWalk : OrientatedWalk
    {
        public override bool CanSeeLeftTurning(Point currentPosition, MazeItem[][] grid)
        {
            return grid[currentPosition.Y + 1][currentPosition.X].IsPath;
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
            return new Point(currentPosition.X - 1, currentPosition.Y);
        }
    } 

}