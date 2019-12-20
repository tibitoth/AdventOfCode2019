using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Puzzles.Day1;
using AdventOfCode2019.Puzzles.Day2;
using AdventOfCode2019.Puzzles.Day3;
using AdventOfCode2019.Puzzles.Day4;
using AdventOfCode2019.Puzzles.Day5;
using AdventOfCode2019.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Day10;
using AdventOfCode2019.Puzzles.Day11;
using AdventOfCode2019.Puzzles.Day6;
using AdventOfCode2019.Puzzles.Day7;
using AdventOfCode2019.Puzzles.Day8;
using AdventOfCode2019.Puzzles.Day9;

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
                    services.AddTransient<CrossedWires>();
                    services.AddTransient<SecureContainer>();
                    services.AddTransient<SunnyWithAChanceOfAsteroids>();
                    services.AddTransient<UniversalOrbitMap>();
                    services.AddTransient<AmplificationCircuit>();
                    services.AddTransient<SpaceImageFormat>();
                    services.Configure<SpaceImageFormatParameters>(ctx.Configuration.GetSection(nameof(SpaceImageFormatParameters)));
                    services.AddTransient<SensorBoost>();
                    services.AddTransient<MonitoringStation>();
                    services.AddTransient<SpacePolice>();

                    services.AddHttpClient<PuzzleClient>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(option.BaseUrl))
                        .ConfigurePrimaryHttpMessageHandler<PuzzleClientCookieHandler>();
                });
    }
}
