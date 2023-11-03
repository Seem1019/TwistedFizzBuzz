using System.Text;
using TwistedFizzBuzz.Library.Interfaces;

namespace TwistedFizzBuzz.Library.Implementations
{
    public class CustomFizzBuzz : ICustomFizzBuzz
    {
        public string CalculateCustom(int number, Dictionary<int, string> customTokens)
        {
            var result = customTokens
                .Where(token => number % token.Key == 0)
                .Aggregate(new StringBuilder(), (sb, kvp) => sb.Append(kvp.Value), sb => sb.ToString());

            return string.IsNullOrEmpty(result) ? number.ToString() : result;
        }
    }
}

