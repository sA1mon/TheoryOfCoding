using Coding.RleAndBurrowsWheeler;
using Coding.RleAndBurrowsWheeler.Resources;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodingUnitTests
{
    [TestFixture]
    public class RleCoding
    {
        [TestCase("aaa&&&aaa", "6 a1 &2 a5 &1")]
        [TestCase("abracadabraabracadabra", "5 r2 d2 a2 r2 c2 a8 b4")]
        [TestCase("never gonna give you up\r\nnever gonna give you up\r\nnever gonna give you up\r\n",
            "43 \r3 p3 a3 r3 u3 e3 n3 v6 n3  6 g3 n3 \n3 o3 g3 y3 u3 e3 o3  3 i3 e3  3")]
        public void EncodingTest(string str, string expected)
        {
            //arrange
            var coder = new Coder();

            //act
            var actual = coder.Encode(str);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("fsadfsad'''sfaf][][[][")]
        [TestCase("jsdkfjkask;lks;a 's'afds ^^^%f655")]
        [TestCase("never gonna give you up\r\nnever gonna give you up\r\nnever gonna give you up\r\n")]
        public void DecodingTest(string input)
        {
            //arrange
            var decoder = new Decoder();
            var code = new Coder().Encode(input);
            var regex = new Regex(@"(?<=\d )(.)(\d+)", RegexOptions.Singleline);

            var c = new List<char>();
            var i = new List<int>();
            foreach (Match match in regex.Matches(code))
            {
                c.Add(char.Parse(match.Groups[1].Value));
                i.Add(int.Parse(match.Groups[2].Value));
            }
            var pos = int.Parse(code.Split(' ').First());

            //act
            var actual = decoder.Decode(new TransformResult(pos, c, i));

            //assert
            Assert.AreEqual(input, actual);
        }
    }
}