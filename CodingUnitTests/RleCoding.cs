using Coding.RleAndBurrowsWheeler;
using NUnit.Framework;

namespace CodingUnitTests
{
    [TestFixture]
    public class RleCoding
    {
        [TestCase("abracadabraabracadabra", "3r1d1a1r1c1a4b2")]
        public void EncodingTest(string str, string expected)
        {
            //arrange
            var coder = new Coder(str);

            //act
            var actual = coder.Encode().ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}