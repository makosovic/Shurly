using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shurly.Core.Helpers;

namespace Shurly.Tests
{
    [TestClass]
    public class ConvertLongShould
    {
        [TestMethod]
        public void ConvertCorrectly()
        {
            long number1 = 124512;
            long number2 = 98712;
            long number3 = 54232313;
            string expectedResult1 = "9Yp";
            string expectedResult2 = "XDp";
            string expectedResult3 = "jP26q";

            string result1 = ConvertLong.ToCustomBase(number1, Characters.AlphaNumeric.ToCharArray());
            string result2 = ConvertLong.ToCustomBase(number2, Characters.AlphaNumeric.ToCharArray());
            string result3 = ConvertLong.ToCustomBase(number3, Characters.AlphaNumeric.ToCharArray());

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
            Assert.AreEqual(expectedResult3, result3);
        }
    }
}
