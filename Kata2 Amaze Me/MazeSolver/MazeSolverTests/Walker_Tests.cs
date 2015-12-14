using System;
using MazeSolver;
using NUnit.Framework;

namespace MazeSolverTests
{
    public class Walker_Tests
    {
        private MazeSolverProcess _mazeSolver;
        private GameState _latestgameState;

        [SetUp]
        public void SetUp()
        {
            _mazeSolver = new MazeSolverProcess();
            _latestgameState = null;
        }

        [Test]
        public void SolveSimpleMaze_WithDumbWalker()
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
                Assert.That(gameState.MoveCounter, Is.LessThanOrEqualTo(expectedMoveCount));
                Console.WriteLine(gameState.CurrentPosition);
                _latestgameState = gameState;
            };

            _mazeSolver.SolveMaze(mazeLines, new DumbMazeWalker());

            AssertGameState(expectedYPos, expectedXPos, expectedMoveCount);
        }

        [Test]
        public void SolveMaze1_WithDumbWalker()
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
            var expectedMoveCount = 13;

            _mazeSolver.MazeStateChangeEvent += gameState =>
            {
                Assert.That(gameState.MoveCounter, Is.LessThanOrEqualTo(expectedMoveCount));
                Console.WriteLine(gameState.CurrentPosition);
                _latestgameState = gameState;
            };

            _mazeSolver.SolveMaze(mazeLines, new DumbMazeWalker());

            AssertGameState(expectedYPos, expectedXPos, expectedMoveCount);
        }

        [Test]
        public void CanotSolveMaze2_WithDumbWalker()
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

        private void AssertGameState(int expectedYPos, int expectedXPos, int expectedMoveCount)
        {
            Assert.That(_latestgameState, Is.Not.Null);
            Assert.That(_latestgameState.CurrentPosition.Y, Is.EqualTo(expectedYPos));
            Assert.That(_latestgameState.CurrentPosition.X, Is.EqualTo(expectedXPos));
            Assert.That(_latestgameState.MoveCounter, Is.EqualTo(expectedMoveCount));
        }
    }
}
