﻿using System;

namespace AdventOfCode2019.Puzzles.Intcode.Instructions.Arithmetic
{
    public class Multiply : TwoOperandInstructionBase
    {
        public Multiply(ProgramContext context)
            : base(context)
        {
        }

        protected override long ExecuteCore(long param1, long param2)
        {
            return param1 * param2;
        }
    }
}
