using Coding.ArithmeticCoding;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;

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

        [TestCase("abcaa")]
        [TestCase("aabcb")]
        [TestCase("abcda")]
        [TestCase("аываыв")]
        [TestCase("Безумно можно быть первым, безумно можно через стены, попасть туда, окунуться в даль...")]
        public void DecodingTest(string expected)
        {
            //arrange
            var coder = new Coder(expected);
            var code = coder.Encode();
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

            var decoder = new Decoder(freq);

            //act
            var actual = decoder.Decode(actualCode.Number, actualCode.Power);

            //assert
            Assert.AreEqual(expected, actual);
            TestContext.WriteLine($"Code: {code}\nExpected string: {expected}\nActual string: {actual}");
        }
    }
}