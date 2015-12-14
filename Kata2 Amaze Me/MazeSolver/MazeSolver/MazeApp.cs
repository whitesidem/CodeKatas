using System;
using System.IO;

namespace MazeSolver
{
    public class MazeApp
    {
        public static void Main()
        {
            var mazeApp = new MazeApp();
            mazeApp.Run(@"..\..\..\..\maze1.txt");
        }

        private void Run(string mazeFilePath)
        {
            var mazeLines = LoadMazeFromFile(mazeFilePath);
            var mazeSolver = new MazeSolverProcess();

            mazeSolver.MazeStateChangeEvent += gameState =>
            {
                Console.WriteLine(gameState.CurrentPosition);
            };

            mazeSolver.SolveMaze(mazeLines, new DumbMazeWalker());
            Console.WriteLine("Reached end of maze! :)");
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