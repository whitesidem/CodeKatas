namespace MazeSolver
{
    public interface IWalkTheMaze
    {
        void MakeMove();
        Point CurrentPosition { get; set; }
        void SetMazeGrid(MazeGrid mazegrid);
    }
}