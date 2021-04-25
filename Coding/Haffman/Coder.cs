using Coding.Haffman.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding.Haffman
{
    public sealed class Coder : BaseCoder
    {
        public override string Encode(string str)
        {
            var codes = GetCodes(str);
            var sb = new StringBuilder();

            foreach (var key in str)
            {
                sb.Append(codes[key]);
            }

            return sb.ToString();
        }

        public IDictionary<char, string> GetCodes(string str)
        {
            FillFrequencyList(str, true);
            var codes = new Dictionary<char, string>();
            var frequency = new List<Symbol>(Frequency);
            var charComparer = new CharComparer();

            while (frequency.Count > 1)
            {
                var first = frequency.Last();
                frequency.Remove(first);
                var second = frequency.Last();
                frequency.Remove(second);

                var unionSymbols = new Symbol(first.Current + second.Current,
                    first.Frequency + second.Frequency)
                {
                    Left = first,
                    Right = second
                };

                frequency.Add(unionSymbols);
                frequency.Sort(charComparer);
            }

            FillCodesDictionary(frequency.First(), codes);

            return codes;
        }

        private static void FillCodesDictionary(Symbol symbol, IDictionary<char, string> codes)
        {
            void ByPass(Symbol current, StringBuilder sb)
            {
                while (true)
                {
                    if (current.Current.Length == 1)
                    {
                        codes.Add(current.Current[0], string.IsNullOrEmpty(sb.ToString()) ? "0" : sb.ToString());
                        return;
                    }

                    ByPass(current.Left, new StringBuilder(sb + "0"));
                    current = current.Right;
                    sb = new StringBuilder(sb + "1");
                }
            }

            ByPass(symbol, new StringBuilder());
        }
    }
}