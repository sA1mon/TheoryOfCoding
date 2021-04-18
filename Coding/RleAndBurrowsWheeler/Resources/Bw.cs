using System;
using System.Collections.Generic;

namespace Coding.RleAndBurrowsWheeler.Resources
{
    public static class Bw
    {
        public static ICollection<string> GetShiftsOf(string currentString)
        {
            var shifts = new List<string> { currentString };
            var length = currentString.Length;

            for (var i = 1; i < length; i++)
            {
                var shiftedStr = string
                    .Concat(str0: currentString.Substring(i, length - i), 
                            str1: currentString.Substring(0, i));

                shifts.Add(shiftedStr);
            }

            return shifts;
        }

        public static int GetIndexOf(string currentString, ICollection<string> shifts)
        {
            var strHash = currentString.GetHashCode();
            var position = 1;

            foreach (var shift in shifts)
            {
                var currentHash = shift.GetHashCode();

                if (strHash == currentHash)
                    return position;

                position++;
            }

            throw new ArgumentException($"\"{currentString}\" doesn't exist in shifts collection",
                nameof(currentString));
        }
    }
}