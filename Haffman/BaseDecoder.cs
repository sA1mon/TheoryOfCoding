namespace Coding
{
    public class BaseDecoder
    {
        public string Encoded { get; protected set; }

        public virtual string Decode()
        {
            return Encoded;
        }
    }
}