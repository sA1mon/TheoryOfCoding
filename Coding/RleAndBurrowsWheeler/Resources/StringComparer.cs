using System.Collections.Generic;
using System.Text;

namespace Coding.RleAndBurrowsWheeler.Resources
{
    public class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            for (var i = 0; i < x.Length; i++)
            {
                if (x[i] < y[i])
                    return -1;
                if (x[i] > y[i])
                    return 1;
            }

            return 0;
        }
    }

    public class StringBuilderComparer : IComparer<StringBuilder>
    {
        public int Compare(StringBuilder x, StringBuilder y)
        {
            var sc = new StringComparer();
            return sc.Compare(x.ToString(), y.ToString());
        }
    }
}