using Common.OperationStrategy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class IntCodeProgram
    {
        public Queue<long> Input { get; }
        public Queue<long> Output { get; }
        private long instructionPointer;
        private readonly long[] _initialMemory;
        private long[] _memory;
        private long relativeBase;

        public IntCodeProgram(long[] memory)
        {
            _initialMemory = memory.ToArray();
            _memory = _initialMemory.ToArray();
            Input = new Queue<long>();
            Output = new Queue<long>();
            instructionPointer = 0;
            relativeBase = 0;
        }

        public IntCodeProgram(int[] memory) : this(memory.Select(x => (long)x).ToArray())
        {
        }

        public IntCodeProgram(long[] memory, long inputValue) : this(memory)
        {
            Input.Enqueue(inputValue);
        }

        public IntCodeProgram(int[] memory, int inputValue) : this(memory)
        {
            Input.Enqueue(inputValue);
        }

        public IntCodeProgram(long[] memory, Queue<long> input) : this(memory)
        {
            Input = input;
        }

        public IntCodeProgram(int[] memory, Queue<long> input) : this(memory)
        {
            Input = input;
        }

        public long GetFirstPosition()
        {
            return _memory[0];
        }

        public Halt Run()
        {
            var operations = GetOperations();
            while (true)
            {
                var opCode = _memory[instructionPointer];
                if ((OpCode)opCode == OpCode.Halt)
                {
                    return Halt.Terminated;
                }

                var singleOpCode = GetOpCode(opCode);

                if (singleOpCode == OpCode.Input && Input.Count == 0)
                {
                    return Halt.NeedInput;
                }

                operations[singleOpCode].Execute(ref _memory, ref instructionPointer, GetParameterModes(opCode), ref relativeBase);
            }
        }

        private Dictionary<OpCode, IOperationStrategy> GetOperations()
        {
            return new Dictionary<OpCode, IOperationStrategy>()
            {
                { OpCode.Add, new AddOperation() },
                { OpCode.Multiply, new MultiplyOperation() },
                { OpCode.Input, new InputOperation(Input) },
                { OpCode.Output, new OutputOperation(Output) },
                { OpCode.JumpIfTrue, new JumpIfTrueOperation() },
                { OpCode.JumpIfFalse, new JumpIfFalseOperation() },
                { OpCode.LessThan, new LessThanOperation() },
                { OpCode.Equals, new EqualsOperation() },
                { OpCode.RelativeBase, new RelativeBaseOperation() }
            };
        }

        private OpCode GetOpCode(long opCodeExtended)
        {
            return (OpCode)GetDigit(opCodeExtended, 1);
        }

        private List<ParameterMode> GetParameterModes(long opCodeExtended)
        {
            ParameterMode firstParamMode = (ParameterMode)GetDigit(opCodeExtended, 3);
            ParameterMode secondParamMode = (ParameterMode)GetDigit(opCodeExtended, 4);
            ParameterMode thirdParamMode = (ParameterMode)GetDigit(opCodeExtended, 5);

            return new List<ParameterMode>() { firstParamMode, secondParamMode, thirdParamMode };
        }

        private static long GetDigit(long value, int rightToLeftTimes)
        {
            int count = 0;
            long toReturn = value;
            while (count < rightToLeftTimes)
            {
                toReturn = value % 10;
                value /= 10;
                count++;
            }
            return toReturn;
        }
    }

    public enum OpCode
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        RelativeBase = 9,
        Halt = 99
    }

    public enum ParameterMode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2
    }

    public enum Halt
    {
        Terminated = 0,
        NeedInput = 1
    }
}
