namespace Coding.Lz77.Resources
{
    public class Buffer
    {
        private const int Size = 5;
        public int Position { get; set; }
        public int Length { get; set; }

        public Buffer()
        {
            Position = default;
            Length = default;
        }

        public string GetBufferedString(string str)
        {
            return str.Substring(Position, Length);
        }

        public void Shift(int length)
        {
            if (Length + length <= Size)
            {
                Length += length;
            }
            else
            {
                length -= Size - Length;
                Length = Size;
                Position += length;
            }
        }
    }
}