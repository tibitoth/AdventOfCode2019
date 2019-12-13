using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode2019.Puzzles.Intcode.Instructions;
using AdventOfCode2019.Puzzles.Intcode.Instructions.Arithmetic;
using AdventOfCode2019.Puzzles.Intcode.Instructions.Boolean;
using AdventOfCode2019.Puzzles.Intcode.Instructions.Control;
using AdventOfCode2019.Puzzles.Intcode.Instructions.IO;

namespace AdventOfCode2019.Puzzles.Intcode
{
    public class IntcodeProgram
    {
        private readonly int[] _registers;

        private ChannelReader<int> _inputReader;
        private ChannelWriter<int> _outputWriter;

        private int _instructionPointer;

        public IntcodeProgram(int[] registers)
        {
            _registers = registers;
        }

        public async Task RunAsync(Channel<int> input, Channel<int> output)
        {
            _inputReader = input.Reader;
            _outputWriter = output.Writer;

            foreach (var instruction in Load())
            {
                _instructionPointer = await instruction.ExecuteAsync(_registers);
            }
        }

        public int this[int index]
        {
            get => _registers[index];
        }

        private IEnumerable<InstructionBase> Load()
        {
            while (_instructionPointer < _registers.Length)
            {
                var instruction = CreateInstruction(_instructionPointer);
                if (instruction is Halt)
                {
                    _outputWriter.Complete();
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
            return (_registers[address] % 100) switch
            {
                99 => new Halt(address),
                1 => new Add(_registers.AsSpan(), address),
                2 => new Multiply(_registers.AsSpan(), address),
                3 => new Input(_registers.AsSpan(), address, _inputReader),
                4 => new Output(_registers.AsSpan(), address, _outputWriter),
                5 => new JumpIfTrue(_registers.AsSpan(), address),
                6 => new JumpIfFalse(_registers.AsSpan(), address),
                7 => new LessThan(_registers.AsSpan(), address),
                8 => new Equals(_registers.AsSpan(), address),
            };
        }
    }
}
