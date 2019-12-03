using AdventOfCode2018.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Infrastructure
{
    public interface IPuzzleSolverFactory
    {
        IPuzzleSolver Create(int day);
    }
}
