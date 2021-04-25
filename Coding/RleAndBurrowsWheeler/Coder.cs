using Coding.RleAndBurrowsWheeler.Resources;
using System.Collections.Generic;

namespace Coding.RleAndBurrowsWheeler
{
    public class Coder : BaseCoder
    {
        public Coder(string initial) : base(initial)
        {
        }

        public override string Encode()
        {
            var shifts = new List<string>(Bw.GetShiftsOf(CurrentString));
            shifts.Sort();
            var position = Bw.GetIndexOf(CurrentString, shifts);

            return Rle.Transform(shifts, position).ToString();
        }
    }
}