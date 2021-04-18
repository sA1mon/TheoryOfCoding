using System.Collections.Generic;
using System.Text;

namespace Coding.RleAndBurrowsWheeler.Resources
{
    public class TransformResult
    {
        public int Position { get; set; }
        public IList<char> Chars { get; set; }
        public IList<int> Count { get; set; }

        public TransformResult(int position, IList<char> chars, IList<int> count)
        {
            Position = position;
            Chars = chars;
            Count = count;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(Position.ToString());

            for (var i = 0; i < Chars.Count; i++)
            {
                sb.Append(string.Concat(Chars[i], Count[i].ToString()));
            }

            return sb.ToString();
        }
    }
}