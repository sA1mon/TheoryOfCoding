﻿using System.Collections.Generic;
using System.Globalization;

namespace Coding.AlgebraicCoding
{
    public class Decoder
    {
        public string Encoded { get; set; }
        private IDictionary<string, int> _frequency;

        public Decoder(string encoded, IDictionary<string, int> frequency)
        {
            Encoded = encoded;
            _frequency = frequency;
        }

        public string Decode()
        {
            var code = double.Parse("0." + Encoded, new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            });

            double left = 0d, right = 1d;


        }
    }
}