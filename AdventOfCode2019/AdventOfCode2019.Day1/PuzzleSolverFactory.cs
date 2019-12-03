using AdventOfCode2018.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AdventOfCode2019.Infrastructure
{
    public class PuzzleSolverFactory : IPuzzleSolverFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PuzzleSolverFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPuzzleSolver Create(int day)
        {
            var type = Assembly.GetExecutingAssembly()
                .GetTypes()
                .SingleOrDefault(t =>
                {
                    var attribute = t.GetCustomAttribute<DayAttribute>();
                    return attribute != null && attribute.Day == day && typeof(IPuzzleSolver).IsAssignableFrom(t);
                });

            return (IPuzzleSolver)_serviceProvider.GetRequiredService(type);
        }
    }
}
