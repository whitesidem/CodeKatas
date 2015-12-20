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



        [Test]
        public void SolveMaze3_WithCleverWalkerStartingAtSouthEntrance()
        {
            var mazeLines = new string[]
            {
                "# # # # # # #",
                "# # # . # # #",
                "# . . . . . #",
                "# . # # # . #",
                "# . # F # . #",
                "# . # . # . #",
                "# . . . . . #",
                "# # # S # # #"            
            };
            var expectedXPos = 3;
            var expectedYPos = 4;

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
        public void SolveMaze4_VeryComplex()
        {
            var mazeLines = new string[]
            {
                "# # # # # # # # # # # # # # #",
                "# . . . . . . # . . # . . . #",
                "# . # . # . # # # . # . # # #",
                "# . # . # . # . . . # . # # #" ,
                "# . # . # . # . # . # . . . #",
                "# . # . # . . . # . # . # F #",
                "# . # . # # # # # . # . # # #",
                "# . . . . . . . # . # . # # #",
                "# . # # # # # . # . . . # # #",
                "# . # . . . # . # # # # # # #",
                "# . # . # . . . . . . . # # #",
                "# # # S # # # # # # # # # # #"            
            };
            var expectedXPos = 13;
            var expectedYPos = 5;

            _mazeSolver.MazeStateChangeEvent += gameState =>
            {
                Assert.That(gameState.MoveCounter, Is.LessThanOrEqualTo(1000));
                Console.WriteLine(gameState);
                _latestgameState = gameState;
            };

            _mazeSolver.SolveMaze(mazeLines, new CleverMazeWalker());

            AssertGameState(expectedYPos, expectedXPos);
        }

        [TestCase(1,1)]
        [TestCase(1,2)]
        [TestCase(1,3)]
        [TestCase(1,4)]
        [TestCase(1,5)]
        [TestCase(1,6)]
        [TestCase(1,7)]
        [TestCase(1,7)]
        [TestCase(1,8)]
        [TestCase(1,13)]
        [TestCase(5,13)]
        [TestCase(8,11)]
        [TestCase(2,11)]
        [TestCase(10,1)]
        public void SolveMaze4_VeryComplex(int yFinish, int xFinish)
        {
            var mazeLines = new string[]
            {
                "# # # # # # # # # # # # # # #",
                "# . . . . . . . # # # . . . #",
                "# . # . # . # # # . . . # # #",
                "# . # . # . # . . . # # # # #" ,
                "# . # . # . # . # . # . . . #",
                "# . # . # . . . # # # . # . #",
                "# . # . # . # # . . . . # # #",
                "# . . . . . . . . # # # # # #",
                "# . # # # # # # . . . . # # #",
                "# . # . . . # # . # # # # # #",
                "# . # . # . . . . . . . # # #",
                "# # # S # # # # # # # # # # #"            
            };
            SetFinishPosition(yFinish, xFinish, mazeLines);

            var expectedXPos = xFinish;
            var expectedYPos = yFinish;

            _mazeSolver.MazeStateChangeEvent += gameState =>
            {
                Assert.That(gameState.MoveCounter, Is.LessThanOrEqualTo(10000));
                Console.WriteLine(gameState);
                _latestgameState = gameState;
            };

            _mazeSolver.SolveMaze(mazeLines, new CleverMazeWalker());

            AssertGameState(expectedYPos, expectedXPos);
        }

        private static void SetFinishPosition(int yFinish, int xFinish, string[] mazeLines)
        {
            var chars = mazeLines[yFinish].ToCharArray();
            chars[xFinish*2] = 'F';
            mazeLines[yFinish] = new String(chars);
        }

        private void AssertGameState(int expectedYPos, int expectedXPos)
        {
            Assert.That(_latestgameState, Is.Not.Null);
            Assert.That(_latestgameState.CurrentPosition.Y, Is.EqualTo(expectedYPos), "unexpecte end Y pos");
            Assert.That(_latestgameState.CurrentPosition.X, Is.EqualTo(expectedXPos), "unexpecte end X pos");
        }

    }
}



           
