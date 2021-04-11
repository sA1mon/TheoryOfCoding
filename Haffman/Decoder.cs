using System.Collections.Generic;
using System.Text;

namespace Haffman
{
    public class Decoder
    {
        public string Encoded { get; }
        private readonly IDictionary<string, char> _codes;

        public Decoder(string encoded, IDictionary<string, char> codes)
        {
            Encoded = encoded;
            _codes = codes;
        }

        public Decoder(string encoded, IDictionary<char, string> codes)
        {
            Encoded = encoded;

            _codes = new Dictionary<string, char>();
            foreach (var code in codes)
            {
                _codes.Add(code.Value, code.Key);
            }
        }

        public string Decode()
        {
            var sb = new StringBuilder();
            var resultSb = new StringBuilder();

            foreach (var symbol in Encoded)
            {
                sb.Append(symbol);

                if (_codes.ContainsKey(sb.ToString()))
                {
                    resultSb.Append(_codes[sb.ToString()]);
                    sb.Clear();
                }
            }

            return resultSb.ToString();
        }
    }
}