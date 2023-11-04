using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using TwistedFizzBuzz.Library.Interfaces;
using TwistedFizzBuzz.Library.Services;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();


        var fizzBuzzService = host.Services.GetRequiredService<IFizzBuzzService>();
        Console.WriteLine(fizzBuzzService.CalculateRange(1, 2000000));

        await host.RunAsync();
        


    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHttpClient();
                services.AddSingleton<IFizzBuzzService, FizzBuzzService>();
            });
}
