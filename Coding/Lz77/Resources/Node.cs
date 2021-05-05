namespace Coding.Lz77.Resources
{
    /// <summary>
    /// Contains nodes {Offset, Length, Next}. <see href="https://neerc.ifmo.ru/wiki/index.php?title=Алгоритмы_LZ77_и_LZ78">Details</see>
    /// </summary>
    public class Node
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public char Next { get; set; }

        public Node(int offset, int length, char next)
        {
            Offset = offset;
            Length = length;
            Next = next;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Node;

            return Offset == other.Offset && 
                   Length == other.Length && 
                   Next == other.Next;
        }

        public override string ToString()
        {
            var eof = Next == '\0' ? "eof" : Next.ToString();

            return $"({Offset}, {Length}, \'{eof}\')";
        }
    }
}