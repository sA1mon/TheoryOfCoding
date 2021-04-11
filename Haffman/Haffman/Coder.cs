using Coding.Haffman.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding.Haffman
{
    public class Coder
    {
        public string CurrentString { get; set; }
        private readonly List<Symbol> _frequency;

        public Coder(string currentString)
        {
            CurrentString = currentString;
            _frequency = new List<Symbol>();
            FillFrequencyList();
        }

        public string Encode()
        {
            var codes = GetCodes();
            var sb = new StringBuilder();

            foreach (var key in CurrentString)
            {
                sb.Append(codes[key]);
            }

            return sb.ToString();
        }

        public IDictionary<char, string> GetCodes()
        {
            var codes = new Dictionary<char, string>();
            var frequency = new List<Symbol>(_frequency);
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

        private void FillFrequencyList()
        {
            var freq = new Dictionary<string, int>();
            foreach (var symbol in CurrentString.Select(x => x.ToString()))
            {
                if (!freq.ContainsKey(symbol))
                    freq[symbol] = 0;

                freq[symbol]++;
            }

            foreach (var pair in freq)
            {
                _frequency.Add(new Symbol(pair.Key, pair.Value));
            }

            _frequency.Sort(new CharComparer());
        }
    }
}
