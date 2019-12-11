using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2019.Puzzles.Intcode.Instructions;
using AdventOfCode2019.Puzzles.Intcode.Instructions.Arithmetic;
using AdventOfCode2019.Puzzles.Intcode.Instructions.Boolean;
using AdventOfCode2019.Puzzles.Intcode.Instructions.Control;
using AdventOfCode2019.Puzzles.Intcode.Instructions.IO;

namespace AdventOfCode2019.Puzzles.Intcode
{
    public class IntcodeProgram : IDisposable
    {
        private readonly ProgramMemory _programMemory;

        private StreamReader _inputStreamReader;
        private StreamWriter _outputStreamWriter;

        private int _instructionPointer;

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
                _instructionPointer = instruction.Execute(_programMemory);
            }
        }

        public int this[int index]
        {
            get => _programMemory.Registers[index];
        }

        private IEnumerable<InstructionBase> Load(ProgramMemory memory)
        {
            while (_instructionPointer < memory.Registers.Length)
            {
                var instruction = CreateInstruction(_instructionPointer);
                if (instruction is Halt)
                {
                    _outputStreamWriter.Flush();
                    yield break;
                }
                else
                {
                    yield return instruction;
                }
            }
        }

        private InstructionBase CreateInstruction(int address)
        {
            return (_programMemory.Registers[address] % 100) switch
            {
                99 => new Halt(address),
                1 => new Add(_programMemory.Registers.AsSpan(), address),
                2 => new Multiply(_programMemory.Registers.AsSpan(), address),
                3 => new Input(_programMemory.Registers.AsSpan(), address, _inputStreamReader),
                4 => new Output(_programMemory.Registers.AsSpan(), address, _outputStreamWriter),
                5 => new JumpIfTrue(_programMemory.Registers.AsSpan(), address),
                6 => new JumpIfFalse(_programMemory.Registers.AsSpan(), address),
                7 => new LessThan(_programMemory.Registers.AsSpan(), address),
                8 => new Equals(_programMemory.Registers.AsSpan(), address),
            };
        }

        public void Dispose()
        {
            _inputStreamReader.Dispose();
            _outputStreamWriter.Dispose();
        }
    }
}
