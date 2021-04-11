using System;

namespace Compress
{
    public static class CompressGetter
    {
        private const int AlphaSize = 8;
        public static double GetCompress(string initial, string compressed)
        {
            double initLen = initial.Length * AlphaSize;
            double compressedLen = compressed.Length;

            return Math.Round(initLen / compressedLen, 2);
        }
    }
}