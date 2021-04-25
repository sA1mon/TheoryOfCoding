using Coding.ShannonFano;
using NUnit.Framework;

namespace CodingUnitTests
{
    [TestFixture]
    public class ShannonFano
    {
        [TestCase("aaabbcde", "000101011011101111")]
        [TestCase("a", "0")]
        [TestCase("aaaaaaaa", "00000000")]
        public void EncodeTesting(string input, string expected)
        {
            //arrange
            var coder = new Coder();

            //act
            var actual = coder.Encode(input);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("aaabbcde", "000101011011101111", 7.11d)]
        [TestCase("a", "0", 16d)]
        [TestCase("aaaaaaaa", "00000000", 16d)]
        public void CompressCoefficient(string initial, string compressed, double expectedValue)
        {
            //arrange, act
            var actual = Compress.CompressGetter.GetCompress(initial, compressed);

            //assert
            Assert.AreEqual(expectedValue, actual);
        }

        [Test]
        public void DoublePenetration()
        {
            //arrange
            var coder = new Coder();

            //act
            var actual = coder.Encode("aaabbcde");
            actual = coder.Encode("aaabbcde");

            //assert
            Assert.AreEqual("000101011011101111", actual);
        }
    }
}