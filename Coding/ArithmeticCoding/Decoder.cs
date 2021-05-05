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

        /// <summary>
        /// Decode string that encoded by arithmetic coding.
        /// </summary>
        /// <param name="args">
        /// <para>arg[0]: IDictionary&lt;char, long&gt; codes</para>
        /// <para>arg[1]: BigInteger number</para>
        /// arg[2]: int power
        /// </param>
        /// <remarks>
        /// Example input: [{'a', 4L}, {'b', 1L}, {'c', 1L}], (BigInteger)1472783434299, 16
        /// </remarks>
        /// <returns>Decoded string.</returns>
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
            for (var i = 0; i < 4096 * 4; i++)
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