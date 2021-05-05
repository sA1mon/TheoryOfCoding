using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Coding;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Coding.Haffman;
using Coding.Lz77.Resources;
using Coding.RleAndBurrowsWheeler;
using Coding.RleAndBurrowsWheeler.Resources;
using Compress;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace TheoryOfCoding
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string TypeOfCoding = "Haffmen";
        public static string InitialText = "";
        private object _dictionary;

        private BaseCoder[] _coders = new BaseCoder[]
        {
            new Coding.Haffman.Coder(),
            new Coding.ShannonFano.Coder(),
            new Coding.ArithmeticCoding.Coder(),
            new Coding.RleAndBurrowsWheeler.Coder(),
            new Coding.Lz77.Coder()
        };

        //private BaseDecoder[] _decoders = new BaseDecoder[]
        //{
        //    new Coding.Haffman.Decoder(),
        //    new Coding.ShannonFano.Decoder(),
        //    new Coding.ArithmeticCoding.Decoder(),
        //    new Coding.RleAndBurrowsWheeler.Decoder(),
        //    new Coding.Lz77.Decoder()
        //};

        private readonly Dictionary<string, int> _codes = new Dictionary<string, int>()
        {
            { "Haffmen", 0 },
            { "Fano-Shannon", 1 },
            { "Arithmetic", 2 },
            { "BWT + RLE", 3 },
            { "LZ77", 4 }
        };



        private void TypeOfCodingSelected(object sender, RoutedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            ComboBoxItem selected = (ComboBoxItem)box.SelectedItem;
            if (selected.Content != null)
            {
                TextBlock tb = (TextBlock)selected.Content;
                TypeOfCoding = tb.Text;
            }
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Multiselect = false,
                Title = "Choose the file"
            };

            if (ofd.ShowDialog() == true)
            {
                string filePath = ofd.FileName;
                FilePathLabel.Content = ofd.FileName;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    InitialText = sr.ReadToEnd();
                }
                InitialAvalon.Clear();
                InitialAvalon.Text = InitialText;
            }
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            InitialText = InitialAvalon.Text;
            if (InitialText.Length == 0)
            {
                MessageBox.Show("Text in left field is empty!", "Warning!", MessageBoxButton.OK);
                return;
            }
            int codingIndex = _codes[TypeOfCoding];
            string encoded = _coders[codingIndex].Encode(InitialText);
            _dictionary = _coders[codingIndex].GetCodes();
            double ratio;
            switch (codingIndex)
            {
                case 2:
                    ratio = CompressGetter.GetCompress(InitialText, ParseBigInteger(encoded), ParsePower(encoded));
                    break;
                default:
                    ratio = CompressGetter.GetCompress(InitialText, encoded);
                    break;
            }
            CompRatio.Content = ratio;
            //encoded = ReplaceEscapes(encoded);
            TransformedAvalon.Clear();
            TransformedAvalon.Text = encoded;
        }

        private string ReplaceEscapes(string input)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '\n':
                        result.Append("\\n");
                        break;
                    case '\r':
                        result.Append("\\r");
                        break;
                    default:
                        result.Append(input[i]);
                        break;
                }
            }

            return result.ToString();
        }

        private BigInteger ParseBigInteger(string input)
        {
            string[] nums = input.Split(' ');
            return BigInteger.Parse(nums[0]);
        }

        private int ParsePower(string input)
        {
            string[] nums = input.Split('^');
            return int.Parse(nums[1]);
        }

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            string EncodedText = TransformedAvalon.Text;
            if (EncodedText.Length == 0)
            {
                MessageBox.Show("Text in right field is empty!", "Warning!", MessageBoxButton.OK);
                return;
            }

            int codingIndex = _codes[TypeOfCoding];
            string decoded = "";
            switch (codingIndex)
            {
                case 0:
                    Coding.Haffman.Decoder decoderHaffmen = new Coding.Haffman.Decoder();
                    decoded = decoderHaffmen.Decode(EncodedText, (Dictionary<string, char>)_dictionary);
                    break;
                case 1:
                    Coding.ShannonFano.Decoder decoderFanoShannon = new Coding.ShannonFano.Decoder();
                    decoded = decoderFanoShannon.Decode(EncodedText, (Dictionary<string, char>)_dictionary);
                    break;
                case 2:
                    Coding.ArithmeticCoding.Decoder decoderArythmetic = new Coding.ArithmeticCoding.Decoder();
                    decoded = decoderArythmetic.Decode((Dictionary<char, long>)_dictionary,
                        ParseBigInteger(EncodedText), ParsePower(EncodedText));
                    break;
                case 3:
                    Coding.RleAndBurrowsWheeler.Decoder decoderBWT = new Coding.RleAndBurrowsWheeler.Decoder();
                    decoded = decoderBWT.Decode(ParseTransformResult(EncodedText));
                    break;
                case 4:
                    Coding.Lz77.Decoder decoderLZ = new Coding.Lz77.Decoder();
                    decoded = decoderLZ.Decode(ParseNodes(EncodedText));
                    break;

            }
            InitialAvalon.Clear();
            InitialAvalon.Text = decoded;
        }

        private IEnumerable<Node> ParseNodes(string input)
        {
            List<Node> Result = new List<Node>();
            string pattern = "(?<=\\()(\\d+), (\\d+), '(.+)(?=')";
            Regex regex = new Regex(pattern);
            MatchCollection collection = regex.Matches(input);
            foreach (Match cum in collection)
            {
                int offset = Convert.ToInt32(cum.Groups[1].Value);
                int length = Convert.ToInt32(cum.Groups[2].Value);
                char nextBoliNet = cum.Groups[3].Value == "eof" ? '\0' : Convert.ToChar(cum.Groups[3].Value);
                Result.Add(new Node(offset, length, nextBoliNet));
            }

            return Result;
        }

        private TransformResult ParseTransformResult(string input)
        {
            string pattern = "(?<=\\d)\\s";
            Regex regex = new Regex(pattern);
            string[] parts = regex.Split(input);
            int position = Convert.ToInt32(parts[0]);
            List<char> chars = new List<char>();
            List<int> ints = new List<int>();
            for (int i = 1; i < parts.Length; i++)
            {
                chars.Add(Convert.ToChar(parts[i][0]));
                ints.Add(Convert.ToInt32(parts[i].Substring(1)));
            }

            return new TransformResult(position, chars, ints);
        }
    }
}
