using AdventOfCode2018.Infrastructure;
using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Runner;
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
                .ConfigureServices((ctx, services) =>
                {
                    var option = services.RegisterOption<AdventOfCodeOptions>(ctx.Configuration);

                    services.AddHttpClient<PuzzleClient>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri(option.BaseUrl))
                        .ConfigurePrimaryHttpMessageHandler<PuzzleClientCookieHandler>();
                });
    }
}
