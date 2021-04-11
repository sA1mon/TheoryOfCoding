﻿using Coding.AlgebraicCoding;
using NUnit.Framework;

namespace CodingUnitTests
{
    public class AlgebraicCoding
    {
        [TestCase("abcaa", "46")]
        [TestCase("aabcb", "121")]
        [TestCase("abcda", "221")]
        public void EncodingTest(string initial, string expected)
        {
            //arrange
            var coder = new Coder(initial);

            //act
            var actual = coder.Encode();

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}