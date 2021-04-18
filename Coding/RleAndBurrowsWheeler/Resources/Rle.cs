using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding.RleAndBurrowsWheeler.Resources
{
    public class Rle
    {
        public static TransformResult Transform(ICollection<string> shifts, int position)
        {
            char? lastChar = null;
            var charCounter = 1;
            var chars = new List<char>();
            var count = new List<int>();

            foreach (var shift in shifts)
            {
                var lastSymbol = shift.Last();
                if (lastChar == null)
                {
                    lastChar = lastSymbol;
                    continue;
                }

                if (lastSymbol == lastChar)
                    charCounter++;
                else
                {
                    chars.Add((char)lastChar);
                    count.Add(charCounter);

                    charCounter = 1;
                    lastChar = lastSymbol;
                }
            }

            if (lastChar == null)
                throw new NullReferenceException();

            chars.Add((char)lastChar);
            count.Add(charCounter);

            return new TransformResult(position, chars, count);
        }
    }
}