using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bowling.Tests
{
    [TestClass]
    public class ThrowTest
    {
        [TestMethod]
        public void TestScoreNoThrows()
        {
            Frame f = new Frame();
            Assert.AreEqual(0, f.Score);
        }

        [TestMethod]
        public void TestAddOneThrow()
        {
            Frame f = new Frame();
            f.Add(5);
            Assert.AreEqual(5, f.Score);
        }



    }

    public class Frame
    {
        private int score;
        public int Score
        {
            get { return score; }
        }

        public void Add(int pins)
        {
            score += pins;
        }
    }
}