using Coding.Lz77.Resources;
using System.Collections.Generic;
using System.Text;
using Buffer = Coding.Lz77.Resources.Buffer;

namespace Coding.Lz77
{
    public class Coder : BaseCoder
    {
        public Coder(string currentString) : base(string.Concat(currentString, '\0'))
        {
        }

        public override string Encode()
        {
            var nodes = new List<Node>();
            var buffer = new Buffer();
            var position = 0;

            while (position < CurrentString.Length)
            {
                var pair = FindMatching(buffer, position);
                var offset = pair.Key;
                var length = pair.Value;

                buffer.Shift(length + 1);

                position += length;
                nodes.Add(new Node(offset, length, CurrentString[position]));
                position++;
            }

            return BuildNodes(nodes);
        }

        private static string BuildNodes(IEnumerable<Node> nodes)
        {
            var sb = new StringBuilder();

            foreach (var node in nodes)
            {
                sb.Append(string.Concat(node, '\n'));
            }

            return sb.ToString();
        }

        private KeyValuePair<int, int> FindMatching(Buffer buffer, int position)
        {
            int mod = buffer.Length, pos = buffer.Length;

            var buffered = buffer.GetBufferedString(CurrentString);
            var optimal = new KeyValuePair<int, int>(0, 0);

            while (pos > 0)
            {
                pos--;
                var start = FindStartPosition(buffered, position, pos);
                pos = start;

                if (start < 0)
                    continue;

                var currentLen = 0;
                var currentPosition = position;
                while (currentPosition < CurrentString.Length && buffered[start] == CurrentString[currentPosition])
                {
                    start = (start + 1) % mod;
                    currentPosition++;
                    currentLen++;
                }

                if (currentLen > optimal.Value)
                {
                    var offset = position - (buffer.Position + pos);
                    optimal = new KeyValuePair<int, int>(offset, currentLen);
                }
            }

            return optimal;
        }

        private int FindStartPosition(string buffered, int headPosition, int pos)
        {
            var firstChar = CurrentString[headPosition];

            while (pos > 0)
            {
                if (buffered[pos] == firstChar)
                    return pos;

                pos--;
            }

            return pos;
        }
    }
}