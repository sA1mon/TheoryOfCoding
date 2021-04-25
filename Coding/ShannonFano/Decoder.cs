using System.Collections.Generic;
using System.Text;

namespace Coding.ShannonFano
{
    public class Decoder : BaseDecoder
    {
        public override string Decode(params object[] args)
        {
            var buffer = new StringBuilder();
            var decodedBuilder = new StringBuilder();

            if (!(args[0] is string encoded)) 
                return decodedBuilder.ToString();

            foreach (var symbol in encoded)
            {
                buffer.Append(symbol);

                if (!(args[1] is IDictionary<string, char> codes) || 
                    !codes.ContainsKey(buffer.ToString()))
                    continue;

                decodedBuilder.Append(codes[buffer.ToString()]);
                buffer.Clear();
            }

            return decodedBuilder.ToString();
        }
    }
}