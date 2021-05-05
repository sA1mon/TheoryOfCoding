using Coding.RleAndBurrowsWheeler.Resources;
using System.Collections.Generic;
using System.Text;

namespace Coding.RleAndBurrowsWheeler
{
    public class Decoder : BaseDecoder
    {
        /// <summary>
        /// Decode string that encoded by arithmetic coding.
        /// </summary>
        /// <param name="args">
        /// <para>arg[0]: TransformResult Object of encode result</para>
        /// </param>
        /// <remarks>
        /// Example input: {1, ['a', 'b', 'a'], [1, 2, 3]}
        /// </remarks>
        /// <returns>Decoded string.</returns>
        public override string Decode(params object[] args)
        {
            var transformResult = args[0] as TransformResult;
            var lastColumn = GetLastColumnAsString(transformResult);
            var sbComparer = new StringBuilderComparer();

            var len = lastColumn.Length;
            var shifts = new List<string>(new string[len]);
            for (var i = 0; i < len; ++i)
            {
                for (var j = 0; j < len; ++j)
                {
                    shifts[j] = lastColumn[j] + shifts[j];
                }
                shifts.Sort(new StringComparer());
            }

            return shifts[transformResult.Position - 1];
        }

        private static string GetLastColumnAsString(TransformResult transformResult)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < transformResult.Chars.Count; i++)
            {
                sb.Append(transformResult.Chars[i], transformResult.Count[i]);
            }

            return sb.ToString();
        }
    }
}