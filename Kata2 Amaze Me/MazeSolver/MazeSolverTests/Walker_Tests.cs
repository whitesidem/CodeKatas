using System;
using MazeSolver;
using NUnit.Framework;

namespace MazeSolverTests
{
    public class Walker_Tests
    {
        private MazeSolverProcess _mazeSolver;
        private GameState _latestgameState;
        private static readonly IWalkTheMaze[] AllWalkers =
        {
            new DumbMazeWalker(),
            new CleverMazeWalker()
        };

        [SetUp]
        public void SetUp()
        {
            _mazeSolver = new MazeSolverProcess();
            _latestgameState = null;
        }

        [Test][TestCaseSource("AllWalkers")]
        public void SolveSimpleMaze_WithWalker(IWalkTheMaze mazeWalker)
        {
            var mazeLines = new string[]
            {
                "#S#",
                "#.#",
                "#F#"
            };
            var expectedYPos = 2;
            var expectedXPos = 1;
            var expectedMoveCount = 2;

            _mazeSolver.MazeStateChangeEvent += gameState =>
            {
                Assert.That(gameState.MoveCounter, Is.LessThanOrEqualTo(1000));
                Console.WriteLine(gameState);
                _latestgameState = gameState;
            };

            _mazeSolver.SolveMaze(mazeLines, mazeWalker);

            AssertGameState(expectedYPos, expectedXPos);
        }

        [Test][TestCaseSource("AllWalkers")]
        public void SolveMaze1_WithWalker(IWalkTheMaze mazeWalker)
        {
            var mazeLines = new string[]
            {
                "# S # # # #",
                "# . . . . #",
                "# # . # # #",
                "# . . . . F",
                "# # # . # #",
                "# # # # # #"
            };
            var expectedYPos = 3;
            var expectedXPos = 5;

            _mazeSolver.MazeStateChangeEvent += gameState =>
            {
                Assert.That(gameState.MoveCounter, Is.LessThanOrEqualTo(1000));
                Console.WriteLine(gameState);
                _latestgameState = gameState;
            };

            _mazeSolver.SolveMaze(mazeLines, mazeWalker);

            AssertGameState(expectedYPos, expectedXPos);
        }

        [Test]
        public void SolveMaze2_WithCleverWalker()
        {
            var mazeLines = new string[]
            {
                "# # # # # # #",
                "# # # S # # #",
                "# # # . # # #",
                "# . . . . . #",
                "# . # # # . #",
                "# . # F # . #",
                "# . # . # . #",
                "# . . . . . #",
                "# # # # # # #"            
            };
            var expectedXPos = 3;
            var expectedYPos = 5;
            var expectedMoveCount = 14;

            _mazeSolver.MazeStateChangeEvent += gameState =>
            {
                Assert.That(gameState.MoveCounter, Is.LessThanOrEqualTo(1000));
                Console.WriteLine(gameState);
                _latestgameState = gameState;
            };

            _mazeSolver.SolveMaze(mazeLines, new CleverMazeWalker());

            AssertGameState(expectedYPos, expectedXPos);
        }

        [Test]
        public void CannotSolveMaze2_WithDumbWalker()
        {
            var mazeLines = new string[]
            {
                "# # # # # # #",
                "# # # S # # #",
                "# # # . # # #",
                "# . . . . . #",
                "# . # # # . #",
                "# . # F # . #",
                "# . # . # . #",
                "# . . . . . #",
                "# # # # # # #"            
            };

            _mazeSolver.MazeStateChangeEvent += gameState =>
            {
                if (gameState.MoveCounter > 100)
                {
                    Assert.Pass("Could not solve this maze :(");
                }
            };

            _mazeSolver.SolveMaze(mazeLines, new DumbMazeWalker());
            Assert.Fail("Should not reach here as it cannot solve this maze");
        }

        private void AssertGameState(int expectedYPos, int expectedXPos)
        {
            Assert.That(_latestgameState, Is.Not.Null);
            Assert.That(_latestgameState.CurrentPosition.Y, Is.EqualTo(expectedYPos), "unexpecte end Y pos");
            Assert.That(_latestgameState.CurrentPosition.X, Is.EqualTo(expectedXPos), "unexpecte end X pos");
        }



    }
}
