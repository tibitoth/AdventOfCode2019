using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private ChannelReader<long> _inputReader;
        private ChannelWriter<long> _outputWriter;

        private readonly ProgramContext _programContext;

        public IntcodeProgram(long[] registers)
        {
            _programContext = new ProgramContext() { Memory = registers.ToList() };
        }

        public async Task RunAsync(Channel<long> input, Channel<long> output)
        {
            _inputReader = input.Reader;
            _outputWriter = output.Writer;

            foreach (var instruction in Load())
            {
                _programContext.InstructionPointer = await instruction.ExecuteAsync();
            }
        }

        public long this[int index] => _programContext.Memory[index];

        private IEnumerable<InstructionBase> Load()
        {
            while (_programContext.InstructionPointer < _programContext.Memory.Count)
            {
                var instruction = CreateInstruction(_programContext.InstructionPointer);
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
            return (_programContext.Memory[address] % 100) switch
            {
                99 => new Halt(_programContext),
                1 => new Add(_programContext),
                2 => new Multiply(_programContext),
                3 => new Input(_programContext, _inputReader),
                4 => new Output(_programContext, _outputWriter),
                5 => new JumpIfTrue(_programContext),
                6 => new JumpIfFalse(_programContext),
                7 => new LessThan(_programContext),
                8 => new Equals(_programContext),
                9 => new RelativeBaseOffset(_programContext),
            };
        }
    }
}
