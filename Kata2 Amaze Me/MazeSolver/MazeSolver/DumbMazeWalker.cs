namespace MazeSolver
{
    public class DumbMazeWalker
    {
        private readonly MazeGrid m_MazeGrid;
 //       private Orientation m_direc;
        private OrientatedWalk _orientatedWalk;

        public DumbMazeWalker(MazeGrid mazeGrid)
        {
            m_MazeGrid = mazeGrid;
            CurrentPosition = m_MazeGrid.StartPosition;
  //          m_direc = Orientation.South;
            _orientatedWalk = new SouthOrientatedWalk();
        }

        public bool CanTurnLeft()
        {
            return _orientatedWalk.CanSeeLeftTurning(CurrentPosition, m_MazeGrid.Grid);

            //switch (m_direc)
            //{
            //    case Orientation.North:
            //        pointToOurLeft.X -= 1;
            //        break;
            //    case Orientation.South:
            //        pointToOurLeft.X += 1;
            //        break;
            //    case Orientation.East:
            //        pointToOurLeft.Y -= 1;
            //        break;
            //    case Orientation.West:
            //        pointToOurLeft.Y += 1;
            //        break;
            //    default:
            //        throw new Exception();
            //}

            //return m_MazeGrid.Grid[pointToOurLeft.Y][pointToOurLeft.X];
        }

        public Point CurrentPosition { get; set; }

        public void TurnRight()
        {
            _orientatedWalk = _orientatedWalk.TurnRight();
            //switch (m_direc)
            //{
            //    case Orientation.North:
            //        m_direc = Orientation.East;
            //        break;
            //    case Orientation.East:
            //        m_direc = Orientation.South;
            //        break;
            //    case Orientation.South:
            //        m_direc = Orientation.West;
            //        break;
            //    case Orientation.West:
            //        m_direc = Orientation.North;
            //        break;
            //    default:
            //        throw new Exception();
            //}
        }

        public void TurnLeft()
        {
            _orientatedWalk = _orientatedWalk.TurnLeft();
            //switch (m_direc)
            //{
            //    case Orientation.North:
            //        m_direc = Orientation.West;
            //        break;
            //    case Orientation.West:
            //        m_direc = Orientation.South;
            //        break;
            //    case Orientation.South:
            //        m_direc = Orientation.East;
            //        break;
            //    case Orientation.East:
            //        m_direc = Orientation.North;C
            //        break;
            //    default:
            //        throw new Exception();
            //}
        }

        public bool CanMoveForward()
        {
            return _orientatedWalk.CanMoveForward(m_MazeGrid.Grid, CurrentPosition);
        }

        public void MoveForward()
        {
            CurrentPosition = new Point(_orientatedWalk.GetDesiredForwardPosition(CurrentPosition));
            
            //var desiredPoint = new Point(CurrentPosition.X, CurrentPosition.Y);

            //switch (m_direc)
            //{
            //    case Orientation.North:
            //        desiredPoint.Y -= 1;
            //        break;
            //    case Orientation.South:
            //        desiredPoint.Y += 1;
            //        break;
            //    case Orientation.East:
            //        desiredPoint.X += 1;
            //        break;
            //    case Orientation.West:
            //        desiredPoint.X -= 1;
            //        break;
            //    default:
            //        throw new Exception();
            //}

            //var canMoveForward = m_MazeGrid.Grid[desiredPoint.Y][desiredPoint.X];
            //if (canMoveForward) CurrentPosition = desiredPoint;
            //return canMoveForward;
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

        public virtual void TurnLeftIfPossible()
        {
            if (CanTurnLeft())
            {
                TurnLeft();
            }
        }

    }
}