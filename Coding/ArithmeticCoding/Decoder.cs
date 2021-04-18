using Coding.ArithmeticCoding.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Coding.ArithmeticCoding
{
    public sealed class Decoder : ArithmeticBase
    {
        public Decoder(IDictionary<char, long> frequency)
        {
            Frequency = frequency;
            SetUpCumulativeFrequency();
        }

        public string Decode(BigInteger number, int power)
        {
            BigInteger powr = 10,
            enc = number * BigInteger.Pow(powr, power);
            var sumOfFrequencies = Frequency.Values.Sum();

            var reverseCumFreq = new Dictionary<long, char>();
            foreach (var key in CumulativeFreq.Keys)
            {
                var value = CumulativeFreq[key];
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
                BigInteger fv = Frequency[c],
                    cv = CumulativeFreq[c],
                    diff = enc - pow * cv;
                enc = diff / fv;
                decoded.Append(c);
            }

            return decoded.ToString();
        }
    }
}