using System.Collections.Generic;
using System.Numerics;

namespace Coding.ArithmeticCoding
{
    public sealed class Coder : BaseCoder
    {
        private IDictionary<char, long> _frequency;
        private IDictionary<char, long> _cumulativeFreq;

        public Coder(string initial) : base(initial)
        {
            CurrentString = initial;
            FillFrequencyList(false);
        }

        protected override void FillFrequencyList(bool needsSort)
        {
            _frequency = new Dictionary<char, long>();

            foreach (var c in CurrentString)
            {
                if (!_frequency.ContainsKey(c))
                    _frequency[c] = 0;

                _frequency[c]++;
            }

            SetUpCumulativeFrequency();
        }

        private void SetUpCumulativeFrequency()
        {
            _cumulativeFreq = new Dictionary<char, long>();

            var total = 0L;
            for (var i = 0; i < 256; i++)
            {
                var c = (char)i;
                if (_frequency.ContainsKey(c))
                {
                    var currentFreq = _frequency[c];
                    _cumulativeFreq[c] = total;
                    total += currentFreq;
                }
            }
        }

        public override string Encode()
        {
            BigInteger baseValue = CurrentString.Length, 
                lowerValue = 0, 
                productFreq = 1;

            foreach (var c in CurrentString)
            {
                BigInteger currentCumFreq = _cumulativeFreq[c];
                lowerValue = lowerValue * baseValue + currentCumFreq * productFreq;
                productFreq *= _frequency[c];
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