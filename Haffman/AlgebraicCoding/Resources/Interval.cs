namespace Coding.AlgebraicCoding.Resources
{
    public class Interval
    {
        public double LeftCorner { get; set; }
        public double RightCorner { get; set; }

        public Interval(double left, double right)
        {
            LeftCorner = left;
            RightCorner = right;
        }

        public bool Contains(double value)
        {
            return value > LeftCorner && value <= RightCorner;
        }
    }
}