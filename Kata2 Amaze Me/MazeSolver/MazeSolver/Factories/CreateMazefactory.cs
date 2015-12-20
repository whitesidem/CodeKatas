using System;
using System.Linq;

namespace MazeSolver.Factories
{
    public static class CreateMazeFactory
    {

        public static MazeGrid FromLines(string[] lines)
        {
            var grid = new MazeItem[lines.Length][];
            int currentRow = 0;

            foreach (var line in lines)
            {
                grid[currentRow] = new MazeItem[line.Count(l => l !=' ')];
                int currentCol = 0;

                foreach (var point in line.Where(l => l !=' '))
                {
                    switch (point)
                    {
                        case '#':
                            grid[currentRow][currentCol] = new MazeItem(false);
                            break;
                        case '.':
                            grid[currentRow][currentCol] = new MazeItem(true);
                            break;
                        case 'S':
                            grid[currentRow][currentCol] = new StartPosMazeItem();
                            break;
                        case 'F':
                            grid[currentRow][currentCol] = new FinishPosMazeItem();
                            break;
                        default:
                            throw new Exception("Maze input string contains invalid characters");
                    }

                    currentCol++;
                }

                currentRow++;
            }
            var maze = new MazeGrid(grid);
            return maze;
        }




    }
}
