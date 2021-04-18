using Coding.ArithmeticCoding;
using NUnit.Framework;

namespace CodingUnitTests
{
    public class ArithmeticCoding
    {
        [TestCase("abcaa", "145 * 10^1")]
        [TestCase("aabcb", "39 * 10^1")]
        [TestCase("abcda", "693 * 10^0")]
        public void EncodingTest(string initial, string expected)
        {
            //arrange
            var coder = new Coder(initial);

            //act
            var actual = coder.Encode();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}