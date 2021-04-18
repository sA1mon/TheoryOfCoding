using Coding.RleAndBurrowsWheeler.Resources;
using System.Collections.Generic;
using System.Text;

namespace Coding.RleAndBurrowsWheeler
{
    public class Decoder
    {
        private TransformResult _transformResult;

        public Decoder(TransformResult tr)
        {
            _transformResult = tr;
        }

        public string Decode()
        {
            var lastColumn = GetLastColumnAsString();
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

            return shifts[_transformResult.Position - 1].ToString();
        }

        private string GetLastColumnAsString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < _transformResult.Chars.Count; i++)
            {
                sb.Append(_transformResult.Chars[i], _transformResult.Count[i]);
            }

            return sb.ToString();
        }
    }
}