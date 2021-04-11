using System.Collections.Generic;
using System.Text;

namespace Coding.Haffman
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
            var sb = new StringBuilder();
            var resultSb = new StringBuilder();

            foreach (var symbol in Encoded)
            {
                sb.Append(symbol);

                if (Codes.ContainsKey(sb.ToString()))
                {
                    resultSb.Append(Codes[sb.ToString()]);
                    sb.Clear();
                }
            }

            return resultSb.ToString();
        }
    }
}