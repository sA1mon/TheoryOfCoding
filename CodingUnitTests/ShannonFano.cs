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
            var coder = new Coder(input);

            //act
            var actual = coder.Encode();

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

        [TestCase("aaabbcde", "000101011011101111")]
        [TestCase("a", "0")]
        [TestCase("aaaaaaaa", "00000000")]
        public void DecodeTest(string input, string decoded)
        {
            //arrange
            var coder = new Coder(input);
            var decoder = new Decoder(decoded, coder.GetCodes());

            //act
            var actual = decoder.Decode();

            //assert
            Assert.AreEqual(input, actual);
        }
    }
}