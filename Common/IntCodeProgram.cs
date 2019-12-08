using Common.OperationStrategy;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class IntCodeProgram
    {
        public Queue<int> Input { get; }
        public Queue<int> Output { get; }
        private int instructionPointer;
        private readonly int[] _initialMemory;
        private readonly int[] _memory;

        public IntCodeProgram(int[] memory)
        {
            _initialMemory = memory.ToArray();
            _memory = _initialMemory.ToArray();
            Input = new Queue<int>();
            Output = new Queue<int>();
            instructionPointer = 0;
        }

        public IntCodeProgram(int[] memory, int inputValue)
        {
            _initialMemory = memory.ToArray();
            _memory = _initialMemory.ToArray();
            Input = new Queue<int>();
            Output = new Queue<int>();
            instructionPointer = 0;

            Input.Enqueue(inputValue);
        }

        public IntCodeProgram(int[] memory, Queue<int> input)
        {
            _initialMemory = memory.ToArray();
            _memory = _initialMemory.ToArray();
            Input = input;
            Output = new Queue<int>();
            instructionPointer = 0;
        }

        public int GetFirstPosition()
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

                operations[singleOpCode].Execute(_memory, ref instructionPointer, GetParameterModes(opCode));
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
                { OpCode.Equals, new EqualsOperation() }
            };
        }

        private OpCode GetOpCode(int opCodeExtended)
        {
            return (OpCode)GetDigit(opCodeExtended, 1);
        }

        private List<ParameterMode> GetParameterModes(int opCodeExtended)
        {
            ParameterMode firstParamMode = (ParameterMode)GetDigit(opCodeExtended, 3);
            ParameterMode secondParamMode = (ParameterMode)GetDigit(opCodeExtended, 4);
            ParameterMode thirdParamMode = (ParameterMode)GetDigit(opCodeExtended, 5);

            return new List<ParameterMode>() { firstParamMode, secondParamMode, thirdParamMode };
        }

        private static int GetDigit(int value, int rightToLeftTimes)
        {
            int count = 0;
            int toReturn = value;
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
        Halt = 99
    }

    public enum ParameterMode
    {
        Position = 0,
        Immediate = 1
    }

    public enum Halt
    {
        Terminated = 0,
        NeedInput = 1
    }
}
