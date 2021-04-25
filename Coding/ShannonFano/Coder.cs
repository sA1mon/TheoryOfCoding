using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coding.ShannonFano
{
    public sealed class Coder : BaseCoder
    {
        public override string Encode(string str)
        {
            var encodeBuilder = new StringBuilder();
            var codes = GetCodes(str);

            foreach (var symbol in str)
            {
                encodeBuilder.Append(codes[symbol]);
            }

            return encodeBuilder.ToString();
        }

        public IDictionary<char, string> GetCodes(string str)
        {
            FillFrequencyList(str, true);
            var stringBuilder = new StringBuilder();
            foreach (var symbol in Frequency.Select(x => x.Current))
            {
                stringBuilder.Append(symbol);
            }

            var frequencyDictionary = Frequency
                .ToDictionary(x => x.Current,
                    x => x.Frequency);

            var tree = GetSplitedFrequencyRepresentation(stringBuilder.ToString(), frequencyDictionary);
            var codes = new Dictionary<char, string>();
            ByPass(tree, codes, new StringBuilder());

            return codes;
        }

        private static void ByPass(Symbol tree, IDictionary<char, string> codes, StringBuilder sb)
        {
            while (true)
            {
                if (tree == null) return;

                if (tree.Current.Length == 1)
                {
                    var code = sb.ToString();
                    codes.Add(tree.Current[0], code == string.Empty ? "0" : code);
                }

                ByPass(tree.Left, codes, new StringBuilder(sb + "0"));
                tree = tree.Right;
                sb = new StringBuilder(sb + "1");
            }
        }

        private static Symbol GetSplitedFrequencyRepresentation(string frequencyString, IDictionary<string, long> frequencyDictionary)
        {
            if (frequencyString.Length == 1)
                return new Symbol(frequencyString, frequencyDictionary[frequencyString]);

            var leftIterator = 0;
            var rightIterator = frequencyString.Length - 1;
            long leftSum = frequencyDictionary[frequencyString[leftIterator].ToString()],
                rightSum = frequencyDictionary[frequencyString[rightIterator].ToString()];

            leftIterator++; rightIterator--;

            while (leftIterator <= rightIterator)
            {
                var leftValue = frequencyDictionary[frequencyString[leftIterator].ToString()];
                var rightValue = frequencyDictionary[frequencyString[rightIterator].ToString()];
                if (leftSum < rightSum)
                {
                    leftSum += leftValue;
                    leftIterator++;
                }
                else
                {
                    rightSum += rightValue;
                    rightIterator--;
                }
            }

            var tree = new Symbol(frequencyString, leftSum + rightSum)
            {
                Left = new Symbol(frequencyString.Substring(0, leftIterator), leftSum),
                Right = new Symbol(frequencyString.Substring(leftIterator,
                        frequencyString.Length - leftIterator),
                    rightSum)
            };

            tree.Left = GetSplitedFrequencyRepresentation(tree.Left.Current, frequencyDictionary);
            tree.Right = GetSplitedFrequencyRepresentation(tree.Right.Current, frequencyDictionary);

            return tree;
        }
    }
}