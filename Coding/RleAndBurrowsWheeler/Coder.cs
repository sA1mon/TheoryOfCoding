using Coding.RleAndBurrowsWheeler.Resources;
using System.Collections.Generic;

namespace Coding.RleAndBurrowsWheeler
{
    public class Coder
    {
        public string CurrentString { get; set; }

        public Coder(string initial)
        {
            CurrentString = initial;
        }

        public TransformResult Encode()
        {
            var shifts = new SortedSet<string>(Bw.GetShiftsOf(CurrentString));
            var position = Bw.GetIndexOf(CurrentString, shifts);

            return Rle.Transform(shifts, position);
        }
    }
}