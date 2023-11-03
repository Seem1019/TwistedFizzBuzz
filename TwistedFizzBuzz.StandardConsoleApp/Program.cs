using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwistedFizzBuzz.Library.Implementations;
using TwistedFizzBuzz.Library.Interfaces;
using TwistedFizzBuzz.Library.Services;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();

        // Resuelve el servicio IRangeFizzBuzz y usa el método CalculateRange para obtener los resultados de FizzBuzz
        var fizzBuzzService = host.Services.GetRequiredService<IRangeFizzBuzz>();
        Console.WriteLine(fizzBuzzService.CalculateRange(1, 100));

        await host.RunAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                // Configura la inyección de dependencias aquí
                services.AddSingleton<IFizzBuzzService, FizzBuzzService>();
                services.AddTransient<IRangeFizzBuzz, RangeFizzBuzz>();
                // Asegúrate de que todas las dependencias necesarias estén registradas
            });
}
