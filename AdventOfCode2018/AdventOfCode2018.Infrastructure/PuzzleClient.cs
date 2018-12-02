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
        private readonly IPuzzleSolver _puzzleSolver;

        public PuzzleClient(IPuzzleSolver puzzleSolver)
        {
            _puzzleSolver = puzzleSolver;
        }

        public async Task SolveAndSendAsync()
        {
            var baseUrl = new Uri("https://adventofcode.com");
            using (var handler = new HttpClientHandler() { CookieContainer = new CookieContainer() })
            using (var client = new HttpClient(handler))
            {
                handler.CookieContainer.Add(baseUrl, new Cookie("session", "[your session id]"));

                var response = await client.GetAsync(new Uri(baseUrl, $"/2018/day/{_puzzleSolver.Day}/input"));
                var input = await response.Content.ReadAsStreamAsync();

                var answer = await _puzzleSolver.SolveAsync(input);

                response = await client.PostAsync(
                    new Uri(baseUrl, $"/2018/day/{_puzzleSolver.Day}/answer"),
                    new FormUrlEncodedContent(
                        new Dictionary<string, string>()
                        {
                            { "level", _puzzleSolver.Day.ToString() },
                            { "answer", answer }
                        }));

                response.EnsureSuccessStatusCode();

                var html = await response.Content.ReadAsStringAsync();
                if (!html.Contains("That's the right answer"))
                {
                    throw new Exception(html);
                }

                Console.WriteLine($"Day {_puzzleSolver.Day} has been succeded");
            }
        }
    }
}
