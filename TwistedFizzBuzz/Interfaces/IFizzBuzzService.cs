using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistedFizzBuzz.Library.Interfaces
{
    public interface IFizzBuzzService
    {
        Task<Dictionary<int, string>> GetTokensFromApiAsync(string apiUrl);
        string GetFizzBuzzValue(int number);
        string GetFizzBuzzValue(int number, Dictionary<int, string> customTokens);
        string CalculateRange(int start, int end);
        string CalculateRange(int start, int end, Dictionary<int, string> customTokens);
        string CalculateSet(IEnumerable<int> numbers, Dictionary<int, string> customTokens);
        string CalculateSet(IEnumerable<int> numbers);
    }

}
