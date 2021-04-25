using System;
using System.Numerics;

namespace Compress
{
    public static class CompressGetter
    {
        private const int AlphaSize = 16;
        private static bool _isArithmetic;
        public static double GetCompress(string initial, string compressed)
        {
            double initSize = initial.Length * AlphaSize;
            var compressedSize = GetCompressedSize(compressed);

            return Math.Round(initSize / compressedSize, 2);
        }

        public static double GetCompress(string initial, BigInteger number, int power)
        {
            _isArithmetic = true;
            var result = GetCompress(initial, string.Concat(number.ToString(), new string('0', power)));
            _isArithmetic = false;

            return result;
        }

        private static double GetCompressedSize(string compressed)
        {
            return compressed.Length * (_isArithmetic ? 4 : 1);
        }
    }
}