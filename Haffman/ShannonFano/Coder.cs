using System.Collections.Generic;
using Coding.Haffman.Resources;

namespace Coding.ShannonFano
{
    public class Coder : BaseCoder
    {
        public Coder(string initial)
        {
            CurrentString = initial;
            Frequency = new List<Symbol>();
        }

        public override string Encode()
        {
            return base.Encode();
        }


    }
}