using System.Collections.Generic;
using System.Numerics;

namespace Coding.ArithmeticCoding
{
    public sealed class Coder : BaseCoder
    {
        private IDictionary<char, long> _frequency;
        private IDictionary<char, long> _cumulativeFreq;

        public override string Encode(string str)
        {
            FillFrequencyList(str, false);
            BigInteger baseValue = str.Length,
                lowerValue = 0,
                productFreq = 1;

            foreach (var c in str)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>IDictionary&lt;char, long&gt;</returns>
        public override object GetCodes()
        {
            return _frequency;
        }

        protected override void FillFrequencyList(string str, bool needsSort)
        {
            _frequency = new Dictionary<char, long>();

            foreach (var c in str)
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
            for (var i = 0; i < 4096 * 4; i++)
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
    }
}