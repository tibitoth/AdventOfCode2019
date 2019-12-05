using AdventOfCode2019.Infrastructure;
using AdventOfCode2019.Runner;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Infrastructure
{
    public class PuzzleClient
    {
        private readonly HttpClient _client;
        private readonly IPuzzleSolverFactory _puzzleSolverFactory;
        private readonly IOptions<AdventOfCodeOptions> _optionsAccessor;
        private readonly ILogger<PuzzleClient> _logger;

        public PuzzleClient(HttpClient client, IPuzzleSolverFactory puzzleSolverFactory, IOptions<AdventOfCodeOptions> optionsAccessor, ILogger<PuzzleClient> logger)
        {
            _client = client;
            _puzzleSolverFactory = puzzleSolverFactory;
            _optionsAccessor = optionsAccessor;
            _logger = logger;
        }

        public async Task SolveAndSendAsync(int day, int part)
        {
            var response = await _client.GetAsync($"/{_optionsAccessor.Value.Year}/day/{day}/input");
            var input = await response.Content.ReadAsStreamAsync();

            var puzzleSolver = _puzzleSolverFactory.Create(day);

            input = await puzzleSolver.PrepareInputAsync(input);
            var answer = part == 1 ? await puzzleSolver.SolvePart1Async(input) : await puzzleSolver.SolvePart2Async(input);

            response = await _client.PostAsync(
                $"/{_optionsAccessor.Value.Year}/day/{day}/answer",
                new FormUrlEncodedContent(
                    new Dictionary<string, string>()
                    {
                        { "level", part.ToString() },
                        { "answer", answer },
                    }));

            _logger.LogInformation("Sending [{answer}] answer for {day} day {part} part", answer, day, part);

            response.EnsureSuccessStatusCode();

            var html = await response.Content.ReadAsStringAsync();
            if (!html.Contains("That's the right answer"))
            {
                throw new Exception(html);
            }

            _logger.LogInformation("{day} day {part} part has been succeeded", day, part);
        }
    }
}
