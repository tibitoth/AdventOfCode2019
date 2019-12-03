using AdventOfCode2019.Runner;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AdventOfCode2019.Infrastructure
{
    public class PuzzleClientCookieHandler : HttpClientHandler
    {
        private readonly IOptions<AdventOfCodeOptions> _optionsAccessor;

        public PuzzleClientCookieHandler(IOptions<AdventOfCodeOptions> optionsAccessor)
        {
            CookieContainer = new CookieContainer();
            CookieContainer.Add(new Uri(optionsAccessor.Value.BaseUrl), new Cookie("session", optionsAccessor.Value.Cookie));
        }
    }
}
