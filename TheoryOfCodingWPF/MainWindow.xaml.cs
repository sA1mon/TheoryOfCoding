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
using System.Windows.Forms;
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

        private BaseCoder[] _coders = new BaseCoder[]
        {
            new Coding.Haffman.Coder(), 
            new Coding.ShannonFano.Coder(),
            new Coding.ArithmeticCoding.Coder(), 
            new Coding.RleAndBurrowsWheeler.Coder(),
            new Coding.Lz77.Coder()
        };

        private BaseDecoder[] _decoders = new BaseDecoder[]
        {
            new Coding.Haffman.Decoder(),
            new Coding.ShannonFano.Decoder(),
            new Coding.ArithmeticCoding.Decoder(),
            new Coding.RleAndBurrowsWheeler.Decoder(),
            new Coding.Lz77.Decoder()
        };

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
            ComboBox box = (ComboBox) sender;
            ComboBoxItem selected = (ComboBoxItem) box.SelectedItem;
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
            InitialText = InitialAvalon.Text.TrimEnd('\r', '\n');
            if (InitialText.Length == 0)
            {
                MessageBox.Show("ПУСТО!");
                return;
            }
            int codingIndex = _codes[TypeOfCoding];
            string encoded = _coders[codingIndex].Encode(InitialText);
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
            TransformedAvalon.Clear();
            TransformedAvalon.Text = encoded;
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
            MessageBox.Show("ВОТ БЛИН, ДЕКОДИРОВАНИЯ НЕТУ :(");
        }
    }
}
