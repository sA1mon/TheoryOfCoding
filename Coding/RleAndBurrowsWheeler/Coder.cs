using Coding.RleAndBurrowsWheeler.Resources;
using System.Collections.Generic;

namespace Coding.RleAndBurrowsWheeler
{
    public class Coder : BaseCoder
    {
        public override string Encode(string str)
        {
            var shifts = new List<string>(Bw.GetShiftsOf(str));
            shifts.Sort();
            var position = Bw.GetIndexOf(str, shifts);

            return Rle.Transform(shifts, position).ToString();
        }
    }
}