using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Day1;
using AdventOfCode2019.Puzzles.Day2;
using AdventOfCode2019.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AdventOfCode2018.Day1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHost(args).RunCommandLineApplicationAsync<App>(args);
        }

        private static IHostBuilder CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, builder) =>
                {
                    builder.AddUserSecrets<Program>();
                })
                .ConfigureServices((ctx, services) =>
                {
                    var option = services.RegisterOption<AdventOfCodeOptions>(ctx.Configuration);
                    services.AddTransient<PuzzleClientCookieHandler>();
                    services.AddTransient<IPuzzleSolverFactory, PuzzleSolverFactory>();

                    services.AddTransient<TheTyrannyOfTheRocketEquation>();
                    services.AddTransient<ProgramAlarm1202>();

                    services.AddHttpClient<PuzzleClient>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(option.BaseUrl))
                        .ConfigurePrimaryHttpMessageHandler<PuzzleClientCookieHandler>();
                });
    }
}
