using System;
using System.Collections.Generic;
using System.Text;

namespace Coding.RleAndBurrowsWheeler.Resources
{
    public class StringBuilderComparer : IComparer<StringBuilder>
    {
        public int Compare(StringBuilder x, StringBuilder y)
        {
            return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
        }
    }
}