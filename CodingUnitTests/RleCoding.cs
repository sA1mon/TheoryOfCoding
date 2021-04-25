using Coding.RleAndBurrowsWheeler;
using Coding.RleAndBurrowsWheeler.Resources;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodingUnitTests
{
    [TestFixture]
    public class RleCoding
    {
        [TestCase("abracadabraabracadabra", "5 r2 d2 a2 r2 c2 a8 b4")]
        public void EncodingTest(string str, string expected)
        {
            //arrange
            var coder = new Coder();

            //act
            var actual = coder.Encode(str);

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
            var decoder = new Decoder();

            //act
            var actual = decoder.Decode(new TransformResult(5, chars, count));

            //assert
            Assert.AreEqual("abracadabraabracadabra", actual);
        }
    }
}