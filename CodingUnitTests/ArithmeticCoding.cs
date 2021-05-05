using Coding.ArithmeticCoding;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace CodingUnitTests
{
    [TestFixture]
    public class ArithmeticCoding
    {
        [TestCase("abcaa", "145 * 10^1")]
        [TestCase("aabcb", "39 * 10^1")]
        [TestCase("abcda", "693 * 10^0")]
        public void EncodingTest(string initial, string expected)
        {
            //arrange
            var coder = new Coder();

            //act
            var actual = coder.Encode(initial);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("abcaa")]
        [TestCase("aabcb")]
        [TestCase("abcda")]
        [TestCase("аываыв")]
        [TestCase("Безумно можно быть первым, безумно можно через стены, попасть туда, окунуться в даль...")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void DecodingTest(string expected)
        {
            //arrange
            var coder = new Coder();
            var code = coder.Encode(expected);
            var splitedCode = code
                .Split(new[] { " * 10^" }, StringSplitOptions.RemoveEmptyEntries);
            var actualCode = new
            {
                Number = BigInteger.Parse(splitedCode[0]),
                Power = int.Parse(splitedCode[1])
            };

            var freq = new Dictionary<char, long>();
            foreach (var c in expected)
            {
                if (!freq.ContainsKey(c))
                    freq[c] = 0;

                freq[c]++;
            }

            var decoder = new Decoder();

            //act
            var actual = decoder.Decode(freq, actualCode.Number, actualCode.Power);

            //assert
            Assert.AreEqual(expected, actual);
            TestContext.WriteLine($"Code: {code}\nExpected string: {expected}\nActual string: {actual}");
        }

        [Test]
        public void WarAndWorldTest()
        {
            //arrange
            var text = File.ReadAllText(@"D:\MyDownloads\WarAndWorld.txt");
            var coder = new Coder();

            //act
            var actual = coder.Encode(text);

            //assert
            Assert.True(true);
        }
    }
}