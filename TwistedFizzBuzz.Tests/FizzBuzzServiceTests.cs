using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TwistedFizzBuzz.Library.Services;


namespace TwistedFizzBuzz.Tests
{
    public class FizzBuzzServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _mockHttpClient;
        private readonly string _apiUrl;

        public FizzBuzzServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"multiple\": 3, \"word\": \"Fizz\"}", Encoding.UTF8, "application/json")
                })
                .Verifiable();

            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _apiUrl = "https://example.com/api/tokens";
        }

        [Fact]
        public async Task GetTokensFromApiAsync_ShouldMakeCorrectApiCall()
        {
            // Arrange
            var service = new FizzBuzzService(_mockHttpClient);

            // Act
            var tokens = await service.GetTokensFromApiAsync(_apiUrl);

            // Assert
            _mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new Uri(_apiUrl)),
                ItExpr.IsAny<CancellationToken>()
            );
            Assert.NotNull(tokens);
            Assert.True(tokens.ContainsKey(3) && tokens[3] == "Fizz");
        }
        [Fact]
        public async Task GetTokensFromApiAsync_ShouldFailOnErrorResponse()
        {
            // Arrange
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            var service = new FizzBuzzService(_mockHttpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => service.GetTokensFromApiAsync(_apiUrl));
        }

        [Fact]
        public async Task GetTokensFromApiAsync_ShouldFailOnMalformedResponse()
        {
            // Arrange
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{ bad json }", Encoding.UTF8, "application/json")
                });

            var service = new FizzBuzzService(_mockHttpClient);

            // Act & Assert
            await Assert.ThrowsAsync<JsonException>(() => service.GetTokensFromApiAsync(_apiUrl));
        }







        [Theory]
        [InlineData(1, 15)]
        [InlineData(1, 2000000)]
        [InlineData(-2, -37)]
        [InlineData(0, 0)]
        [InlineData(-3, 3)]
        [InlineData(14, 16)]
        [InlineData(99, 101)]
        [InlineData(49, 51)]
        public void CalculateRange_ShouldReturnCorrectFizzBuzzOutput(int start, int end)
        {
            // Arrange
            var service = new FizzBuzzService(_mockHttpClient);

            // Act
            string result = service.CalculateRange(start, end);

            // Assert
            Assert.True(IsValidFizzBuzzForAnyRange(result, start, end));
        }

        private bool IsValidFizzBuzzForAnyRange(string result, int start, int end)
        {
            var results = result.Split(new[] { "\r\n" }, StringSplitOptions.None);
            int index = 0; // Index to track the position in the results array.
            int step = start <= end ? 1 : -1;
            for (int i = start; (step == 1) ? i <= end : i >= end; i += step)
            {
                bool isMultipleOf3 = i % 3 == 0;
                bool isMultipleOf5 = i % 5 == 0;
                bool isZero = i == 0;

                string expected = isMultipleOf3 && isMultipleOf5 ? "FizzBuzz" :
                                  isZero ? "0" :
                                  isMultipleOf3 ? "Fizz" :
                                  isMultipleOf5 ? "Buzz" :
                                  i.ToString();

                if (results[index++] != expected)
                {
                    return false;
                }
            }

            return true;
        }

        [Theory]
        [InlineData(new[] { -5, 6, 300, 12, 15 }, "Buzz\r\nFizz\r\nFizzBuzz\r\nFizz\r\nFizzBuzz\r\n")]
        public  void CalculateSet_ShouldReturnCorrectFizzBuzzOutput(int[] numbers, string expectedOutput)
        {
            // Arrange
            var service = new FizzBuzzService(_mockHttpClient);

            // Act
            string result = service.CalculateSet(numbers);

            // Assert
            Assert.Equal(expectedOutput, result);
        }

        [Fact] 
        public void CalculateSet_WithCustomTokens_ShouldReturnCorrectFizzBuzzOutput()
        {
            // Arrange
            var customTokens = new Dictionary<int, string>
        {
            { 3, "College" },
            { 7, "Poem" },
            { 17, "Writer" },
            
        };

            var service = new FizzBuzzService(_mockHttpClient);
            var numbers = new[] { 21, 51, 119, 357 };
            var expectedResults = new[] { "CollegePoem", "CollegeWriter", "PoemWriter", "CollegePoemWriter" };

            // Act
            var results = numbers.Select(number => service.GetFizzBuzzValue(number, customTokens)).ToArray();

            // Assert
            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.Equal(expectedResults[i], results[i]);
            }
        }
    }
}
