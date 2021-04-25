using System.Collections.Generic;
using System.Text;

namespace Coding.Haffman
{
    public class Decoder : BaseDecoder
    {
        public override string Decode(params object[] args)
        {
            var sb = new StringBuilder();
            var resultSb = new StringBuilder();

            if (!(args[0] is string encoded)) return resultSb.ToString();

            foreach (var symbol in encoded)
            {
                sb.Append(symbol);

                if (!(args[1] is IDictionary<string, char> codes) || 
                    !codes.ContainsKey(sb.ToString())) 
                    continue;

                resultSb.Append(codes[sb.ToString()]);
                sb.Clear();
            }

            return resultSb.ToString();
        }
    }
}