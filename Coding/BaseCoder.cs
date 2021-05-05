using Coding.Haffman.Resources;
using System.Collections.Generic;
using System.Linq;

namespace Coding
{
    public abstract class BaseCoder
    {
        protected List<Symbol> Frequency;

        public virtual string Encode(string str)
        {
            return str;
        }

        public abstract object GetCodes();

        protected virtual void FillFrequencyList(string str, bool needsSort)
        {
            var freq = new Dictionary<string, int>();
            Frequency = new List<Symbol>();
            foreach (var symbol in str.Select(x => x.ToString()))
            {
                if (!freq.ContainsKey(symbol))
                    freq[symbol] = 0;

                freq[symbol]++;
            }

            foreach (var pair in freq)
            {
                Frequency.Add(new Symbol(pair.Key, pair.Value));
            }

            if (needsSort)
                Frequency.Sort(new CharComparer());
        }
    }
}