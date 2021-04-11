﻿using Coding.AlgebraicCoding.Resources;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Coding.AlgebraicCoding
{
    public sealed class Coder : BaseCoder
    {
        private Dictionary<char, int> _indexes;

        public Coder(string initial) : base(initial)
        {
            _indexes = new Dictionary<char, int>();
            FillFrequencyList();
        }

        public override string Encode()
        {
            return GetCodingNumber();
        }

        protected override void FillFrequencyList()
        {
            base.FillFrequencyList();

            for (var i = 0; i < Frequency.Count; i++)
            {
                _indexes.Add(Frequency[i].Current[0], i);
            }
        }

        private string GetCodingNumber()
        {
            var leftCorner = 0d;
            var rightCorner = 1d;

            SetLeftAndRightCorners(ref leftCorner, ref rightCorner);

            return GetClosestNumber(leftCorner, rightCorner);
        }

        private static string GetClosestNumber(double leftCorner, double rightCorner)
        {
            var left = leftCorner.ToString(CultureInfo.InvariantCulture);
            var right = rightCorner.ToString(CultureInfo.InvariantCulture);
            var isMatchFound = false;
            var iterator = 2;
            var codingNumberBuilder = new StringBuilder();

            while (!isMatchFound)
            {
                if (left[iterator] == right[iterator])
                {
                    codingNumberBuilder.Append(left[iterator]);
                    iterator++;
                }
                else
                {
                    codingNumberBuilder.Append((char)((left[iterator] + 1) % 10 + '0'));
                    isMatchFound = true;
                }
            }

            return codingNumberBuilder.ToString();
        }

        private void SetLeftAndRightCorners(ref double leftCorner, ref double rightCorner)
        {
            foreach (var character in CurrentString)
            {
                var intervals = new List<Interval>();
                var iterator = leftCorner;
                var len = rightCorner - leftCorner;
                var countOfSymbols = Frequency.Count;

                AddIntervalsInList(len, countOfSymbols, iterator, intervals);

                var index = _indexes[character];
                var currentInterval = intervals[index];

                leftCorner = currentInterval.LeftCorner;
                rightCorner = currentInterval.RightCorner;
            }
        }

        private void AddIntervalsInList(double len, int countOfSymbols, double iterator, ICollection<Interval> intervals)
        {
            foreach (var symbol in Frequency)
            {
                var sectionLen = len * ((double)symbol.Frequency / countOfSymbols);

                var interval = new Interval(iterator, iterator + sectionLen);
                intervals.Add(interval);
                iterator += sectionLen;
            }
        }
    }
}