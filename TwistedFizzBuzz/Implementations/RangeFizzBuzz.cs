using System.Text;
using TwistedFizzBuzz.Library.Interfaces;

namespace TwistedFizzBuzz.Library.Implementations
{
    public class RangeFizzBuzz : IRangeFizzBuzz
    {
        private readonly IFizzBuzzService _fizzBuzzService;

        public RangeFizzBuzz(IFizzBuzzService fizzBuzzService)
        {
            _fizzBuzzService = fizzBuzzService;
        }

        public string CalculateRange(int start, int end)
        {
            var sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                sb.AppendLine(_fizzBuzzService.GetFizzBuzzValue(i));
            }
            return sb.ToString();
        }
    }

}
