using System.Collections.Generic;

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
    }
}