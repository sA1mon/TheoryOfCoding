using Coding.RleAndBurrowsWheeler;
using Coding.RleAndBurrowsWheeler.Resources;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodingUnitTests
{
    [TestFixture]
    public class RleCoding
    {
        [TestCase("abracadabraabracadabra", "5r2d2a2r2c2a8b4")]
        public void EncodingTest(string str, string expected)
        {
            //arrange
            var coder = new Coder(str);

            //act
            var actual = coder.Encode().ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DecodingTest()
        {
            //arrange
            var chars = new List<char>
            {
                'r', 'd', 'a', 'r', 'c', 'a', 'b'
            };
            var count = new List<int>
            {
                2, 2, 2, 2, 2, 8, 4
            };
            var decoder = new Decoder(new TransformResult(5, chars, count));

            //act
            var actual = decoder.Decode();

            //assert
            Assert.AreEqual("abracadabraabracadabra", actual);
        }
    }
}