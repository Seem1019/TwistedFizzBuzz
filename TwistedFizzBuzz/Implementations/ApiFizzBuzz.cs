using System.Text;
using System.Text.Json;
using TwistedFizzBuzz.Library.Interfaces;

namespace TwistedFizzBuzz.Library.Implementations
{
    public class ApiFizzBuzz : IApiFizzBuzz
    {
        private readonly HttpClient _httpClient;
        private readonly IFizzBuzzService _fizzBuzzService;

        public ApiFizzBuzz(HttpClient httpClient, IFizzBuzzService fizzBuzzService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _fizzBuzzService = fizzBuzzService ?? throw new ArgumentNullException(nameof(fizzBuzzService));
        }

        public async Task<string> CalculateFromApiAsync(string apiUrl)
        {
            var response = await _httpClient.GetStringAsync(apiUrl);
            if (string.IsNullOrEmpty(response))
            {
                throw new InvalidOperationException("The API response is empty or null");
            }

            var tokens = JsonSerializer.Deserialize<List<ApiTokenResponse>>(response);
            if (tokens == null || !tokens.Any())
            {
                throw new InvalidOperationException("The API response is invalid or null");
            }

            var customTokens = tokens
                .Where(t => t != null && !string.IsNullOrEmpty(t.Word))
                .ToDictionary(token => token.Multiple, token => token.Word);

            var sb = new StringBuilder();
            for (int i = 1; i <= 100; i++)
            {
                sb.AppendLine(_fizzBuzzService.GetFizzBuzzValue(i, customTokens));
            }

            return sb.ToString();
        }
    }

    public class ApiTokenResponse
    {
        public int Multiple { get; set; }
        public string Word { get; set; }
    }


}
