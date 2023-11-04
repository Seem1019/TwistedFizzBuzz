using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedFizzBuzz.Library.Interfaces;
using TwistedFizzBuzz.Library.Services;

namespace CustomFizzBuzzApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            // Resolver el servicio IFizzBuzzService usando el contenedor de inyección de dependencias
            var fizzBuzzService = host.Services.GetRequiredService<IFizzBuzzService>();

            // Definir los tokens personalizados
            var customTokens = new Dictionary<int, string>
        {
            { 5, "Fizz" },
            { 9, "Buzz" },
            { 27, "Bar" }
        };
            var apiUrl = "https://rich-red-cocoon-veil.cyclic.app/random";


            Console.WriteLine("API");
            Console.WriteLine(fizzBuzzService.CalculateRange(1, 50, await fizzBuzzService.GetTokensFromApiAsync(apiUrl)));

            var nonSequentialNumbers = new[] { -5, 6, 300, 12, 15 };
            Console.WriteLine(fizzBuzzService.CalculateSet(nonSequentialNumbers));
            Console.WriteLine("Custom: ");
            Console.WriteLine(fizzBuzzService.CalculateRange(-20,127,customTokens));

            await host.RunAsync();
        }


        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddSingleton<IFizzBuzzService, FizzBuzzService>();

                });
    }
}
