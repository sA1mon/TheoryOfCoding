using System.Collections.Generic;
using System.Text;

namespace Coding.ShannonFano
{
    public class Decoder : BaseDecoder
    {
        public Decoder(string encoded, IDictionary<string, char> codes) : base(codes)
        {
            Encoded = encoded;
        }

        public Decoder(string encoded, IDictionary<char, string> codes) : base(new Dictionary<string, char>())
        {
            Encoded = encoded;

            foreach (var code in codes)
            {
                Codes.Add(code.Value, code.Key);
            }
        }

        public override string Decode()
        {
            var buffer = new StringBuilder();
            var decodedBuilder = new StringBuilder();

            foreach (var symbol in Encoded)
            {
                buffer.Append(symbol);

                if (Codes.ContainsKey(buffer.ToString()))
                {
                    decodedBuilder.Append(Codes[buffer.ToString()]);
                    buffer.Clear();
                }
            }

            return decodedBuilder.ToString();
        }
    }
}