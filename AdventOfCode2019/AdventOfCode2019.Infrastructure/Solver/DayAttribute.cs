using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Infrastructure
{
    public class DayAttribute : Attribute
    {
        public DayAttribute(int day)
        {
            Day = day;
        }

        public int Day { get; set; }
    }
}
