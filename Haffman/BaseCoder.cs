using Coding.Haffman.Resources;
using System.Collections.Generic;
using System.Linq;

namespace Coding
{
    public class BaseCoder
    {
        public string CurrentString { get; protected set; }
        protected List<Symbol> Frequency;

        public BaseCoder(string initial)
        {
            CurrentString = initial;
        }

        public virtual string Encode()
        {
            return CurrentString;
        }

        protected void FillFrequencyList()
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
                Frequency.Add(new Symbol(pair.Key, pair.Value));
            }

            Frequency.Sort(new CharComparer());
        }
    }
}