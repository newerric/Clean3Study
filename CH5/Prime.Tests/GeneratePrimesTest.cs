using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prime
{
    [TestClass]
    public class GeneratePrimesTest
    {
        [TestMethod]
        public void TestPrimes()
        {
            int[] nullArray = PrimeGenerator.GeneratePrimeNumbers(0);
            Assert.AreEqual(nullArray.Length, 0);

            //int[] minArray = PrimeGenerator.GeneratePrimeNumbers(2);
            //Assert.AreEqual(minArray.Length, 1);
            //Assert.AreEqual(minArray[0], 2);

            int[] threeArray = PrimeGenerator.GeneratePrimeNumbers(3);
            //Assert.AreEqual(threeArray.Length, 2);
            //Assert.AreEqual(threeArray[0], 2);
            //Assert.AreEqual(threeArray[1], 3);

            int[] centArray = PrimeGenerator.GeneratePrimeNumbers(100);
            //Assert.AreEqual(centArray.Length, 25);
            Assert.AreEqual(centArray[24], 97);
        }
    }
}