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
        private Point _lastPoint = null;

        public MazePresenter(string[] mazeLines, int xOffset)
        {
            _mazeLines = mazeLines;
            _xOffset = xOffset;
        }

        public void DisplayMaze()
        {
            var yPos = 0;
            foreach (var row in _mazeLines)
            {
                var xPos = 0;
                foreach (var col in row.Where(l => l != ' '))
                {
                    ShowItemInGrid(new Point(yPos,xPos), col, _xOffset);
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
            ShowItemInGrid(point, item, _xOffset);
            Thread.Sleep(TimeSpan.FromMilliseconds(500));
        }

        private void RemoveLastPosition()
        {
            if (_lastPoint == null)
            {
                return;
            }
            var point = _lastPoint;
            var item = ' ';
            ShowItemInGrid(point, item, _xOffset);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void ShowItemInGrid(Point point, char item, int xOffset)
        {
            Console.SetCursorPosition(point.X + xOffset, point.Y);
            Console.Write(item);
        }
    }
}