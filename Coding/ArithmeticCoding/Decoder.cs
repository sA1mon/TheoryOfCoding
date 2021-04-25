using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Coding.ArithmeticCoding
{
    public sealed class Decoder : BaseDecoder
    {
        private BigInteger _number;
        private int _power;
        private IDictionary<char, long> _codes;
        private IDictionary<char, long> _cumulativeFreq;

        public override string Decode(params object[] args)
        {
            SetupDecoder(args);
            BigInteger powr = 10,
            enc = _number * BigInteger.Pow(powr, _power);
            var sumOfFrequencies = _codes.Values.Sum();

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
                BigInteger fv = _codes[c],
                    cv = _cumulativeFreq[c],
                    diff = enc - pow * cv;
                enc = diff / fv;
                decoded.Append(c);
            }

            return decoded.ToString();
        }

        private void SetupDecoder(IReadOnlyList<object> args)
        {
            _codes = args[0] as IDictionary<char, long>;
            _number = (BigInteger) args[1];
            _power = (int) args[2];

            SetUpCumulativeFrequency();
        }

        private void SetUpCumulativeFrequency()
        {
            _cumulativeFreq = new Dictionary<char, long>();

            var total = 0L;
            for (var i = 0; i < 2048; i++)
            {
                var c = (char)i;
                if (_codes.ContainsKey(c))
                {
                    var currentFreq = _codes[c];
                    _cumulativeFreq[c] = total;
                    total += currentFreq;
                }
            }
        }
    }
}