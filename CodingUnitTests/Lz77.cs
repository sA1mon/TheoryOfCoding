using Coding.Lz77;
using Coding.Lz77.Resources;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Decoder = Coding.Lz77.Decoder;

namespace CodingUnitTests
{
    [TestFixture]
    public class Lz77
    {
        [Test]
        public void EncodingTest()
        {
            //arrange
            var coder = new Coder();
            var nodes = new List<Node>
            {
                new Node(0, 0, 'a'),
                new Node(0, 0, 'b'),
                new Node(2, 1, 'c'),
                new Node(4, 7, 'd'),
                new Node(2, 1, 'c'),
                new Node(2, 1, '\0')
            };
            var sb = new StringBuilder();
            foreach (var node in nodes)
            {
                sb.Append(string.Concat(node, '\n'));
            }

            var expected = sb.ToString();

            //act
            var actual = coder.Encode("abacabacabadaca");

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DecodeTesting()
        {
            //arrange
            var coder = new Coder();
            var decoder = new Decoder();

            //act
            var actual = decoder.Decode(Parse(coder.Encode("acccabcabcab")));

            //assert
            Assert.AreEqual("acccabcabcab", actual);
        }

        private static IEnumerable<Node> Parse(string str)
        {
            const string pattern = @"((?<=\()\d+), (\d+), \'(.+)\'";
            var nodeRegex = new Regex(pattern, RegexOptions.Multiline);

            var matches = nodeRegex.Matches(str);

            return from Match match in matches 
                select match.Groups 
                into groups 
                let offset = int.Parse(groups["1"].Value) 
                let length = int.Parse(groups["2"].Value) 
                let next = groups["3"].Value == "eof" ? '\0' : groups["3"].Value[0] 
                select new Node(offset, length, next);
        }
    }
}