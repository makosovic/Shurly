using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shurly.Core.Helpers;

namespace Shurly.Tests
{
    [TestClass]
    public class CustomNumberBaseShould
    {
        [TestMethod]
        public void EncodeProperly()
        {
            long number1 = 987512;
            long number2 = 54232313;
            long number3 = 2323187983;
            string expectedResult1 = "h5ap";
            string expectedResult2 = "jP26q";
            string expectedResult3 = "hAREkP";

            string result1 = CustomNumberBase.Encode(number1, Characters.AlphaNumeric.ToCharArray());
            string result2 = CustomNumberBase.Encode(number2, Characters.AlphaNumeric.ToCharArray());
            string result3 = CustomNumberBase.Encode(number3, Characters.AlphaNumeric.ToCharArray());

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
            Assert.AreEqual(expectedResult3, result3);
        }

        [TestMethod]
        public void DecodeProperly()
        {
            string base50Value1 = "h5ap";
            string base50Value2 = "jP26q";
            string base50Value3 = "hAREkP";

            long expectedResult1 = 987512;
            long expectedResult2 = 54232313;
            long expectedResult3 = 2323187983;

            long result1 = CustomNumberBase.Decode(base50Value1, Characters.AlphaNumeric.ToCharArray());
            long result2 = CustomNumberBase.Decode(base50Value2, Characters.AlphaNumeric.ToCharArray());
            long result3 = CustomNumberBase.Decode(base50Value3, Characters.AlphaNumeric.ToCharArray());

            Assert.AreEqual(expectedResult1, result1);
            Assert.AreEqual(expectedResult2, result2);
            Assert.AreEqual(expectedResult3, result3);
        }
    }
}
