using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Coding.ArithmeticCoding
{
    public sealed class Decoder : BaseDecoder
    {
        private readonly BigInteger _number;
        private readonly int _power;
        private new readonly IDictionary<char, long> Codes;
        private IDictionary<char, long> _cumulativeFreq;

        public Decoder(IDictionary<char, long> codes, BigInteger number, int power) : base(null)
        {
            Codes = codes;
            _number = number;
            _power = power;

            SetUpCumulativeFrequency();
        }

        public override string Decode()
        {
            BigInteger powr = 10,
            enc = _number * BigInteger.Pow(powr, _power);
            var sumOfFrequencies = Codes.Values.Sum();

            var reverseCumFreq = new Dictionary<long, char>();
            foreach (var key in _cumulativeFreq.Keys)
            {
                var value = _cumulativeFreq[key];
                reverseCumFreq[value] = key;
            }

            var freq = -1L;
            for (long i = 0; i < sumOfFrequencies; i++)
            {
                if (reverseCumFreq.ContainsKey(i))
                {
                    freq = reverseCumFreq[i];
                }
                else if (freq != -1)
                {
                    reverseCumFreq[i] = (char)freq;
                }
            }

            var decoded = new StringBuilder((int)sumOfFrequencies);
            BigInteger bigBase = sumOfFrequencies;
            for (var i = sumOfFrequencies - 1; i >= 0; --i)
            {
                BigInteger pow = BigInteger.Pow(bigBase, (int)i),
                    div = enc / pow;
                var c = reverseCumFreq[(long)div];
                BigInteger fv = Codes[c],
                    cv = _cumulativeFreq[c],
                    diff = enc - pow * cv;
                enc = diff / fv;
                decoded.Append(c);
            }

            return decoded.ToString();
        }

        private void SetUpCumulativeFrequency()
        {
            _cumulativeFreq = new Dictionary<char, long>();

            var total = 0L;
            for (var i = 0; i < 2048; i++)
            {
                var c = (char)i;
                if (Codes.ContainsKey(c))
                {
                    var currentFreq = Codes[c];
                    _cumulativeFreq[c] = total;
                    total += currentFreq;
                }
            }
        }
    }
}