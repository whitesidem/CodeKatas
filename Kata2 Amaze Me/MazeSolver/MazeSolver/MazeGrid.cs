using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver
{
    public class MazeGrid
    {
        // +--------> +ve X
        // |
        // |
        // |
        // |
        // v
        // +ve Y
        private readonly MazeItem[][] _grid; //bool[y][x]
        private readonly Point _start;
        private readonly Point _finish;
        private readonly int _rows;
        private readonly int _cols;

        public MazeGrid(MazeItem[][] grid)
        {
            _grid = grid;
            _rows = grid.GetUpperBound(0)+1;
            _cols = grid[0].Length;
            _start = CalculateStartPosition();
            _finish = CalculateFinishPosition();
        }

        private Point CalculateStartPosition()
        {
            var start = FindMazeItemByType<StartPosMazeItem>();
            if (start == null) throw new Exception("Maze should have a finish position set.");
            return start;
        }

        private Point CalculateFinishPosition()
        {
            var finish = FindMazeItemByType<FinishPosMazeItem>();
            if (finish == null) throw new Exception("Maze should have a finish position set.");
            return finish;
        }

        public Point StartPosition
        {
            get { return _start; }
        }

        public bool AtFinish(Point currentPosition)
        {
            return Finish.X == currentPosition.X && Finish.Y == currentPosition.Y;
        }

        public Point Finish
        {
            get { return _finish; }
        }

        public MazeItem[][] Grid
        {
            get { return _grid; }
        }

        private Point FindMazeItemByType<T>()
        {
            var yLimit = Enumerable.Range(0, _rows);
            var xLimit = Enumerable.Range(0, _cols);
            var item = xLimit.SelectMany(x => yLimit.Where(y => _grid[y][x] is T).Select(y => new Point(x, y))).FirstOrDefault();
            return item;
        }

        //For debugging purposes only so far
        public void ConsoleOutMaze()
        {
            var yLimit = Enumerable.Range(0, _rows);
            var xLimit = Enumerable.Range(0, _cols);
            var all = xLimit.SelectMany(x => yLimit.Select(y => new { item = _grid[y][x], x, y }));
            all.ToList().ForEach(a => { Console.WriteLine("coord{0},{1}:{2}", a.x, a.y, a); });
        }
    }

    public class MazeItem
    {
        public bool IsPath { get; set; }

        public MazeItem(bool isPath)
        {
            IsPath = isPath;
        }
    }

    public class StartPosMazeItem : MazeItem
    {
        public StartPosMazeItem() : base(true) { }
    }

    public class FinishPosMazeItem : MazeItem
    {
        public FinishPosMazeItem() : base(true) { }
    }


}