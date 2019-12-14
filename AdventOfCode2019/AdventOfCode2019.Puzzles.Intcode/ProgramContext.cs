using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode2019.Puzzles.Intcode
{
    public class ProgramContext
    {
        private long[] Memory { get; set; }
        public int InstructionPointer { get; set; }
        public int RelativeBase { get; set; }
        public long MemorySize => Memory.LongLength;

        public ProgramContext(long[] memory)
        {
            Memory = memory;
        }


        public long this[in long index]
        {
            get
            {
                bool retry = false;
                do
                {
                    try
                    {
                        retry = false;
                        return Memory[index];
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        var expanded = new long[index + 1];

                        for (long i = 0; i < Memory.LongLength; i++)
                        {
                            expanded[i] = Memory[i];
                        }

                        for (long i = Memory.LongLength; i < expanded.LongLength; i++)
                        {
                            expanded[i] = 0;
                        }

                        Memory = expanded;

                        retry = true;
                    }
                } while (retry);

                throw new InvalidOperationException();
            }

            set
            {
                bool retry = false;
                do
                {
                    try
                    {
                        retry = false;
                        Memory[index] = value;
                        return;
                    }
                    catch (Exception ex)
                    {
                        var expanded = new long[index + 1];

                        for (long i = 0; i < Memory.LongLength; i++)
                        {
                            expanded[i] = Memory[i];
                        }

                        for (long i = Memory.LongLength; i < expanded.LongLength; i++)
                        {
                            expanded[i] = 0;
                        }

                        Memory = expanded;

                        retry = true;
                    }
                } while (retry);

                throw new InvalidOperationException();
            }
        }
    }
}
