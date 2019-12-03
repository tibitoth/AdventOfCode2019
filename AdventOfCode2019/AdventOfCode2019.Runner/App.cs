using AdventOfCode2018.Infrastructure;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Runner
{
    public class App
    {
        [Option("-d", Description = "Number of the day")]
        [Required]
        [Range(1, 28)]
        public int Day { get; set; }

        private readonly PuzzleClient _puzzleClient;

        public App(PuzzleClient puzzleClient)
        {
            _puzzleClient = puzzleClient;
        }

        public async Task OnExecute()
        {
            await _puzzleClient.SolveAndSendAsync(Day);
        }
    }
}
