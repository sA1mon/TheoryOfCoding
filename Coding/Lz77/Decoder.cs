using System.Collections.Generic;
using System.Text;
using Coding.Lz77.Resources;

namespace Coding.Lz77
{
    public class Decoder : BaseDecoder
    {
        private new IEnumerable<Node> Codes;

        public Decoder(IEnumerable<Node> codes) : base(null)
        {
            Codes = codes;
        }

        public override string Decode()
        {
            var decodeBuilder = new StringBuilder();

            foreach (var node in Codes)
            {
                if (node.Length > 0)
                {
                    var start = decodeBuilder.Length - node.Offset;

                    for (var i = 0; i < node.Length; i++)
                    {
                        decodeBuilder.Append(decodeBuilder[start + i]);
                    }
                }

                if (node.Next != '\0')
                    decodeBuilder.Append(node.Next);
            }

            return decodeBuilder.ToString();
        }
    }
}