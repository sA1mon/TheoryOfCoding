namespace Coding
{
    public class BaseCoder
    {
        public string CurrentString { get; set; }

        public virtual string Encode()
        {
            return CurrentString;
        }
    }
}