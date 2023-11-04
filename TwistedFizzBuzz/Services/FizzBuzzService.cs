using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TwistedFizzBuzz.Library.ApiResponses;
using TwistedFizzBuzz.Library.Interfaces;

namespace TwistedFizzBuzz.Library.Services
{
    public class FizzBuzzService : IFizzBuzzService

    {

        private readonly HttpClient _httpClient;

        public FizzBuzzService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
         public async Task<Dictionary<int, string>> GetTokensFromApiAsync(string apiUrl)
         {
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<ApiTokenResponse>();

                if(content == null)
                {
                    throw new Exception("Token by the API is NULL");
                }
            var value = new Dictionary<int, string>
            {
                { content.Multiple, content.Word }
            };

            return value;
         }


        public string GetFizzBuzzValue(int number)
        {
            return GetFizzBuzzValue(number, new Dictionary<int, string>
            {
                { 3, "Fizz" },
                { 5, "Buzz" }
            });
        }

        // Método para obtener el valor de FizzBuzz para un número con tokens personalizados
        public string GetFizzBuzzValue(int number, Dictionary<int, string> customTokens)
        {
            var result = new StringBuilder();

            foreach (var token in customTokens.OrderBy(kvp => kvp.Key))
            {
                if (number % token.Key == 0)
                {
                    result.Append(token.Value);
                }
            }

            return result.Length > 0 ? result.ToString() : number.ToString();
        }

        
        public string CalculateRange(int start, int end, Dictionary<int, string> customTokens)
        {
            var sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                sb.AppendLine(GetFizzBuzzValue(i, customTokens));
            }
            return sb.ToString();
        }

        
        public string CalculateSet(IEnumerable<int> numbers, Dictionary<int, string> customTokens)
        {
            var sb = new StringBuilder();
            foreach (var number in numbers)
            {
                sb.AppendLine(GetFizzBuzzValue(number, customTokens));
            }
            return sb.ToString();
        }

        public string CalculateRange(int start, int end)
        {
            int step = start <= end ? 1 : -1;

            var sb = new StringBuilder();
            for (int i = start; (step == 1) ? i <= end : i >= end; i += step)
            {
                sb.AppendLine(GetFizzBuzzValue(i));
            }
            return sb.ToString();
        }

        public string CalculateSet(IEnumerable<int> numbers)
        {
            var sb = new StringBuilder();
            foreach (var number in numbers)
            {
                sb.AppendLine(GetFizzBuzzValue(number));
            }
            return sb.ToString();
        }
    }
}
