using System.Collections.Generic;
using System.Text;
using Coding.Lz77.Resources;

namespace Coding.Lz77
{
    public class Decoder : BaseDecoder
    {
        public override string Decode(params object[] args)
        {
            var decodeBuilder = new StringBuilder();

            if (!(args[0] is IEnumerable<Node> codes)) 
                return decodeBuilder.ToString();

            foreach (var node in codes)
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