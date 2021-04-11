using System.Collections.Generic;

namespace Coding.Haffman.Resources
{
    internal class CharComparer : IComparer<Symbol>
    {
        public int Compare(Symbol x, Symbol y)
        {
            var value = -x.Frequency.CompareTo(y.Frequency);

            return value == 0 ? 1 : value;
        }
    }
}
