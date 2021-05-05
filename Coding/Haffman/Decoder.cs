using System.Collections.Generic;
using System.Text;

namespace Coding.Haffman
{
    public class Decoder : BaseDecoder
    {
        /// <summary>
        /// Decode string that encoded by arithmetic coding.
        /// </summary>
        /// <param name="args">
        /// <para>arg[0]: string Encoded string</para>
        /// <para>arg[1]: IDictionary&lt;string, char&gt; codes</para>
        /// </param>
        /// <remarks>
        /// Example input: "0100010100", [{"01", 'a'}, {"00", 'b'}]
        /// </remarks>
        /// <returns>Decoded string.</returns>
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