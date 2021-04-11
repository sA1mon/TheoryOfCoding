using System.Collections.Generic;
using Coding.Haffman.Resources;

namespace Coding
{
    public class BaseCoder
    {
        public string CurrentString { get; set; }
        protected List<Symbol> Frequency;

        public virtual string Encode()
        {
            return CurrentString;
        }
    }
}