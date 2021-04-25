using System.Collections.Generic;
using System.Linq;

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
            var sb = new List<string>()
            {
                Position.ToString()
            };

            sb.AddRange(Chars
                    .Select((t, i) =>
                        string.Concat(t, Count[i].ToString())));

            return string.Join(" ", sb);
        }
    }
}