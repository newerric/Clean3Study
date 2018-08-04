using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bowling.Tests
{
    [TestClass]
    public class GameTest
    {
        private Game game;

        #region setup and clean
        [TestInitialize]
        public void SetUp()
        {
            game = new Game();
        }

        [TestCleanup]
        public void Clean()
        {
            game = null;
        }
        #endregion


        [TestMethod]
        public void TestOneThrows()
        {
            game.Add(5);

            Assert.AreEqual(5, game.Score);
            Assert.AreEqual(1, game.CurrentFrame);
        }

        [TestMethod]
        public void TestTwoThrowNoMark()
        {
            game.Add(5);
            game.Add(4);

            Assert.AreEqual(9, game.Score);
            Assert.AreEqual(1, game.CurrentFrame);
        }

        [TestMethod]
        public void TestFourThrowNoMark()
        {
            game.Add(5);
            game.Add(4);
            game.Add(7);
            game.Add(2);

            Assert.AreEqual(18, game.Score);
            Assert.AreEqual(9, game.ScoreForFrame(1));
            Assert.AreEqual(18, game.ScoreForFrame(2));
            Assert.AreEqual(2, game.CurrentFrame);
        }

        [TestMethod]
        public void TestSimpleSpare()
        {
            game.Add(3);
            game.Add(7);
            game.Add(3);

            Assert.AreEqual(13, game.ScoreForFrame(1));
        }

        [TestMethod]
        public void TestSimpleFrameAfterSpare()
        {
            game.Add(3);
            game.Add(7);
            game.Add(3);
            game.Add(2);

            Assert.AreEqual(13, game.ScoreForFrame(1));
            Assert.AreEqual(18, game.ScoreForFrame(2));
        }



    }

    public class Game
    {
        private int currentFrame;
        private bool isFirstThrow = true;
        private int score;
        private int[] throws = new int[21];
        private int currentThrow;


        public int Score
        {
            get { return score; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        public void Add(int pins)
        {
            throws[currentThrow++] = pins;
            score += pins;

            if (isFirstThrow)
            {
                isFirstThrow = false;
                currentFrame++;
            }
            else
            {
                isFirstThrow = true;
            }
        }

        public int ScoreForFrame(int theFrame)
        {
            int ball = 0;
            int score = 0;

            for (int currentFrame = 0; currentFrame < theFrame; currentFrame++)
            {
                int firstThrow = throws[ball++];
                int secondThrow = throws[ball++];

                int frameScore = firstThrow + secondThrow;

                // spare
                if (frameScore == 10)
                {
                    score += frameScore + throws[ball];
                }
                else
                {
                    score += frameScore;
                }
            }

            return score;
        }
    }
}