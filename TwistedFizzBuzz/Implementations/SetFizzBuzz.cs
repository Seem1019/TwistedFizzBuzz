using TwistedFizzBuzz.Library.Interfaces;

namespace TwistedFizzBuzz.Library.Implementations
{
    public class SetFizzBuzz : ISetFizzBuzz
    {
        private readonly IFizzBuzzService _fizzBuzzService;

        public SetFizzBuzz(IFizzBuzzService fizzBuzzService)
        {
            _fizzBuzzService = fizzBuzzService;
        }

        public string CalculateSet(IEnumerable<int> numbers)
        {
            return string.Join(Environment.NewLine, numbers.Select(_fizzBuzzService.GetFizzBuzzValue));
        }
    }

}
