using Coding.ShannonFano;
using NUnit.Framework;

namespace CodingUnitTests
{
    public class ShannonFano
    {
        [Test]                            
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

        //[Test]
        [TestCase("aaabbcde", "000101011011101111", 3.56d)]
        [TestCase("a", "0", 8d)]
        [TestCase("aaaaaaaa", "00000000", 8d)]
        public void CompressCoefficient(string initial, string compressed, double expectedValue)
        {
            //arrange, act
            var actual = Compress.CompressGetter.GetCompress(initial, compressed);

            //assert
            Assert.AreEqual(actual, expectedValue);
        }

        [Test]
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