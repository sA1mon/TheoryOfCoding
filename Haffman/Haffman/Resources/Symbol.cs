﻿namespace Coding.Haffman.Resources
{
    public class Symbol
    {
        public string Current { get; set; }
        public int Frequency { get; set; }
        public Symbol Left { get; set; }
        public Symbol Right { get; set; }

        public Symbol(string str, int frequency)
        {
            Current = str;
            Frequency = frequency;
        }
    }
}