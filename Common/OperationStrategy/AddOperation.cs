using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public class AddOperation : OperationStrategy
    {
        public override void Execute(ref long[] input, ref long instructionPointer, List<ParameterMode> paramModes, ref long relativeBase)
        {
            long firstParam = input[instructionPointer + 1];
            long secondParam = input[instructionPointer + 2];
            long outputPosition = input[instructionPointer + 3];

            long firstValue = GetParamValue(firstParam, paramModes[0], ref input, ref relativeBase);
            long secondValue = GetParamValue(secondParam, paramModes[1], ref input, ref relativeBase);

            long address = GetAddress(outputPosition, paramModes[2], ref input, ref relativeBase);
            input[address] = firstValue + secondValue;

            instructionPointer += 4;
        }
    }
}
