using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MazeSolver
{
    public class MazeApp
    {
        public static void Main()
        {
            var mazeApp = new MazeApp();
            mazeApp.Run(@"..\..\..\..\maze6.txt");
        }

        private void Run(string mazeFilePath)
        {
            Console.Clear();

            var mazeLines = LoadMazeFromFile(mazeFilePath);
            
            var tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() =>
            {
                CleverMaze(mazeLines);
            });
            tasks[1] = Task.Factory.StartNew(() =>
            {
                DumbMaze(mazeLines);
            });
            
            Task.WaitAll(tasks); 

            Console.SetCursorPosition(0,30);
            Console.WriteLine("Reached end of maze! :)");
        }

        private void DumbMaze(string[] mazeLines)
        {
            MazePresenter mazePresenter = new MazePresenter(mazeLines, 50, "Dumb Walker");
            mazePresenter.DisplayMaze();
            var mazeSolver = new MazeSolverProcess();
            mazeSolver.MazeStateChangeEvent += mazePresenter.DisplayMove;
            mazeSolver.SolveMaze(mazeLines, new DumbMazeWalker());
        }

        private void CleverMaze(string[] mazeLines)
        {
            MazePresenter mazePresenter = new MazePresenter(mazeLines, 5, "Smart Walker");
            mazePresenter.DisplayMaze();
            var mazeSolver = new MazeSolverProcess();
            mazeSolver.MazeStateChangeEvent += mazePresenter.DisplayMove;
            mazeSolver.SolveMaze(mazeLines, new CleverMazeWalker());
        }

        private static string[] LoadMazeFromFile(string mazeFilePath)
        {
            var lines =
                new StreamReader(new FileStream(mazeFilePath, FileMode.Open)).ReadToEnd()
                    .Replace(" ", "")
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            return lines;
        }

    }
}