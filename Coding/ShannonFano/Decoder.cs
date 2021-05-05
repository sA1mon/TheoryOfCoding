using System.Collections.Generic;
using System.Text;

namespace Coding.ShannonFano
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