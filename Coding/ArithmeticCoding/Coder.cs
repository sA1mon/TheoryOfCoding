using System.Collections.Generic;
using System.Numerics;

namespace Coding.ArithmeticCoding
{
    public sealed class Coder : BaseCoder
    {
        private new Dictionary<char, long> Frequency;
        private IDictionary<char, long> CumulativeFreq;

        public Coder(string initial) : base(initial)
        {
            FillFrequencyList(false);
        }

        public override string Encode()
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

        private void SetUpCumulativeFrequency()
        {
            CumulativeFreq = new Dictionary<char, long>();

            var total = 0L;
            for (var i = 0; i < 2048; i++)
            {
                var c = (char)i;
                if (Frequency.ContainsKey(c))
                {
                    var currentFreq = Frequency[c];
                    CumulativeFreq[c] = total;
                    total += currentFreq;
                }
            }
        }
    }
}