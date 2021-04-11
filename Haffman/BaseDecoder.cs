using System.Collections.Generic;

namespace Coding
{
    public class BaseDecoder
    {
        protected readonly IDictionary<string, char> Codes;

        public BaseDecoder(IDictionary<string, char> codes)
        {
            Codes = codes;
        }

        public string Encoded { get; protected set; }

        public virtual string Decode()
        {
            return Encoded;
        }
    }
}