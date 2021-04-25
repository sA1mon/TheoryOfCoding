using Coding.RleAndBurrowsWheeler.Resources;
using System.Collections.Generic;
using System.Text;

namespace Coding.RleAndBurrowsWheeler
{
    public class Decoder : BaseDecoder
    {
        public override string Decode(params object[] args)
        {
            var transformResult = args[0] as TransformResult;
            var lastColumn = GetLastColumnAsString(transformResult);
            var shifts = new List<StringBuilder>();
            var sbComparer = new StringBuilderComparer();

            for (var i = 0; i < lastColumn.Length; i++)
            {
                shifts.Add(new StringBuilder());
            }

            foreach (var _ in lastColumn)
            {
                for (var j = 0; j < lastColumn.Length; j++)
                {
                    shifts[j].Insert(0, lastColumn[j]);
                }

                shifts.Sort(sbComparer);
            }

            return shifts[transformResult.Position - 1].ToString();
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