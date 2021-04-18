using Coding.ArithmeticCoding.Resources;
using System.Collections.Generic;
using System.Numerics;

namespace Coding.ArithmeticCoding
{
    public sealed class Coder : ArithmeticBase
    {
        public Coder(string initial)
        {
            CurrentString = initial;
            FillFrequencyList(false);
        }

        protected override void FillFrequencyList(bool needsSort)
        {
            Frequency = new Dictionary<char, long>();

            foreach (var c in CurrentString)
            {
                if (!Frequency.ContainsKey(c))
                    Frequency[c] = 0;

                Frequency[c]++;
            }

            SetUpCumulativeFrequency();
        }

        public string Encode()
        {
            BigInteger baseValue = CurrentString.Length,
                lowerValue = 0,
                productFreq = 1;

            foreach (var c in CurrentString)
            {
                BigInteger currentCumFreq = CumulativeFreq[c];
                lowerValue = lowerValue * baseValue + currentCumFreq * productFreq;
                productFreq *= Frequency[c];
            }

            BigInteger upper = lowerValue + productFreq,
                bigRadix = 10;
            var power = 0;

            while (true)
            {
                productFreq /= bigRadix;
                if (productFreq == 0) break;
                power++;
            }

            var diff = (upper - 1) / (BigInteger.Pow(bigRadix, power));

            return $"{diff} * 10^{power}";
        }
    }
}