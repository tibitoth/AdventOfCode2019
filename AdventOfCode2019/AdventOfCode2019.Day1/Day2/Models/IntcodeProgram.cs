using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public class IntcodeProgram
    {
        private readonly ProgramMemory _programMemory;

        public IntcodeProgram(ProgramMemory programMemory)
        {
            _programMemory = programMemory;
        }

        public void Run()
        {
            foreach (var instruction in Load(_programMemory))
            {
                instruction.Operate(_programMemory);
            }
        }

        public int this[int index]
        {
            get => _programMemory.Registers[index];
        }

        private IEnumerable<InstructionBase> Load(ProgramMemory memory)
        {
            for (int i = 0; i < memory.Registers.Length;)
            {
                var instruction = CreateInstruction(i);
                if (instruction is Halt)
                {
                    yield break;
                }
                else
                {
                    yield return instruction;
                    i += instruction.InstructionLength;
                }
            }
        }

        private InstructionBase CreateInstruction(int address)
        {
            return _programMemory.Registers[address] switch
            {
                99 => new Halt(),
                1 => new Add(_programMemory.Registers.AsSpan().Slice(address)),
                2 => new Multiply(_programMemory.Registers.AsSpan().Slice(address)),
            };
        }
    }
}
