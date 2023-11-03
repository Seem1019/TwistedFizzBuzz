using System.Text;
using TwistedFizzBuzz.Library.Interfaces;

namespace TwistedFizzBuzz.Library.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        public string GetFizzBuzzValue(int number)
        {
            return GetFizzBuzzValue(number, new Dictionary<int, string>
        {
            { 3, "Fizz" },
            { 5, "Buzz" }
        });
        }

        public string GetFizzBuzzValue(int number, Dictionary<int, string> customTokens)
        {
            var result = new StringBuilder();

            foreach (var token in customTokens)
            {
                if (number % token.Key == 0)
                {
                    result.Append(token.Value);
                }
            }

            return result.Length > 0 ? result.ToString() : number.ToString();
        }
    }

}
