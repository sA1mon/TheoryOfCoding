using System.Collections.Generic;

namespace Coding.ArithmeticCoding.Resources
{
    public class ArithmeticBase : BaseCoder
    {
        protected new IDictionary<char, long> Frequency;
        protected IDictionary<char, long> CumulativeFreq;

        public ArithmeticBase(string initial) : base(initial)
        {
        }

        protected new virtual void FillFrequencyList(bool needsSort)
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

        protected virtual void SetUpCumulativeFrequency()
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