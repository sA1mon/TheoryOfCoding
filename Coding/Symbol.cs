namespace Coding
{
    public class Symbol
    {
        public string Current { get; set; }
        public long Frequency { get; set; }
        public Symbol Left { get; set; }
        public Symbol Right { get; set; }

        public Symbol(string str, long frequency)
        {
            Current = str;
            Frequency = frequency;
        }
    }
}