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


        // [TestMethod]
        // public void TestOneThrows()
        // {
        //     game.Add(5);

        //     Assert.AreEqual(5, game.Score);
        //     Assert.AreEqual(1, game.CurrentFrame);
        // }

        [TestMethod]
        public void TestTwoThrowNoMark()
        {
            game.Add(5);
            game.Add(4);

            Assert.AreEqual(9, game.Score);
            Assert.AreEqual(2, game.CurrentFrame);
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
            Assert.AreEqual(3, game.CurrentFrame);
        }

        [TestMethod]
        public void TestSimpleSpare()
        {
            game.Add(3);
            game.Add(7);
            game.Add(3);

            Assert.AreEqual(13, game.ScoreForFrame(1));
            Assert.AreEqual(2, game.CurrentFrame);
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
            Assert.AreEqual(3, game.CurrentFrame);
        }

        [TestMethod]
        public void TestSimpleStrike()
        {
            game.Add(10);
            game.Add(3);
            game.Add(6);

            Assert.AreEqual(19, game.ScoreForFrame(1));
            Assert.AreEqual(28, game.Score);
            Assert.AreEqual(3, game.CurrentFrame);
        }

        [TestMethod]
        public void TestPerfectGame()
        {
            for (int i = 0; i < 12; i++)
            {
                game.Add(10);
            }

            Assert.AreEqual(300, game.Score);
            Assert.AreEqual(11, game.CurrentFrame);
        }

    }

    public class Game
    {
        private int currentFrame = 1;
        private bool isFirstThrow = true;
        private int score;
        private int[] throws = new int[21];
        private int currentThrow;

        public int Score
        {
            get
            {
                return ScoreForFrame(currentFrame - 1);
            }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        public void Add(int pins)
        {
            throws[currentThrow++] = pins;
            score += pins;

            AdjustCurrentFrame(pins);
        }

        private void AdjustCurrentFrame(int pins)
        {
            if (isFirstThrow)
            {
                if (pins == 10) // Strike
                {
                    currentFrame++;
                }
                else
                {
                    isFirstThrow = false;
                }
            }
            else
            {
                isFirstThrow = true;
                currentFrame++;
            }

            if (currentFrame > 11)
            {
                currentFrame = 11;
            }
        }

        public int ScoreForFrame(int theFrame)
        {
            int ball = 0;
            int score = 0;

            for (int currentFrame = 0; currentFrame < theFrame; currentFrame++)
            {
                int firstThrow = throws[ball++];
                if (firstThrow == 10)   // Strike
                {
                    score += 10 + throws[ball] + throws[ball + 1];
                }
                else
                {
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
            }

            return score;
        }
    }
}