using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode2019.Puzzles.Day2.Models
{
    public class IntcodeProgram : IDisposable
    {
        private readonly ProgramMemory _programMemory;

        public StreamWriter _outputStreamWriter;
        public StreamReader _inputStreamReader;

        public IntcodeProgram(ProgramMemory programMemory)
        {
            _programMemory = programMemory;
        }

        public void Run(Stream input, Stream output)
        {
            _inputStreamReader = new StreamReader(input);
            _outputStreamWriter = new StreamWriter(output);

            foreach (var instruction in Load(_programMemory))
            {
                instruction.Execute(_programMemory);
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
                    _outputStreamWriter.Flush();
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
            return (_programMemory.Registers[address] % 100) switch
            {
                99 => new Halt(),
                1 => new Add(_programMemory.Registers.AsSpan(), address),
                2 => new Multiply(_programMemory.Registers.AsSpan(), address),
                3 => new Input(_programMemory.Registers.AsSpan(), address, _inputStreamReader),
                4 => new Output(_programMemory.Registers.AsSpan(), address, _outputStreamWriter),
            };
        }

        public void Dispose()
        {
            _inputStreamReader.Dispose();
            _outputStreamWriter.Dispose();
        }
    }
}
