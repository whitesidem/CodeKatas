using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MazeSolver
{
    public class MazePresenter
    {
        private readonly string[] _mazeLines;
        private readonly int _xOffset;
        private readonly string _name;
        private Point _lastPoint = null;

        public MazePresenter(string[] mazeLines, int xOffset, string name)
        {
            _mazeLines = mazeLines;
            _xOffset = xOffset;
            _name = name;
        }

        public void DisplayMaze()
        {
            ShowItemInGrid(new Point(-2, 0), _name, _xOffset);

            var yPos = 0;
            foreach (var row in _mazeLines)
            {
                var xPos = 0;
                foreach (var col in row.Where(l => l != ' '))
                {
                    ShowItemInGrid(new Point(yPos,xPos), col.ToString(), _xOffset);
                    xPos++;
                }
                yPos++;
            }
        }

        public void DisplayMove(GameState gameState)
        {
            RemoveLastPosition();
            _lastPoint = gameState.CurrentPosition;
            ShowNewPosition(gameState);
        }

        private void ShowNewPosition(GameState gameState)
        {
            var point = gameState.CurrentPosition;
            var item = 'O';
            ShowItemInGrid(point, item.ToString(), _xOffset);
            Thread.Sleep(TimeSpan.FromMilliseconds(300));
        }

        private void RemoveLastPosition()
        {
            if (_lastPoint == null)
            {
                return;
            }
            var point = _lastPoint;
            var item = ' ';
            ShowItemInGrid(point, item.ToString(), _xOffset);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void ShowItemInGrid(Point point, string item, int xOffset)
        {
            Console.SetCursorPosition(point.X + xOffset, point.Y+4);
            Console.Write(item);
        }
    }
}