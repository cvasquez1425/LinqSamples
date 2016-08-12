namespace LinqSamples.ExtensionMethods
{
    public static class StringExtensions
    {
        static public double ToDouble(this string data)
        {
            double result = double.Parse(data);
            return result;
        }
    }
}
