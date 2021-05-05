using System.Collections.Generic;
using System.Linq;

namespace Coding.RleAndBurrowsWheeler.Resources
{
    /// <summary>
    /// Contains encode result.
    /// </summary>
    public class TransformResult
    {
        /// <summary>
        /// Position in shift list.
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// List of chars.
        /// </summary>
        public IList<char> Chars { get; set; }
        /// <summary>
        /// Number of consecutive characters.
        /// </summary>
        public IList<int> Count { get; set; }

        public TransformResult(int position, IList<char> chars, IList<int> count)
        {
            Position = position;
            Chars = chars;
            Count = count;
        }

        public override string ToString()
        {
            var sb = new List<string>
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