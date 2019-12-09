using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public class JumpIfTrueOperation : OperationStrategy
    {
        public override void Execute(ref long[] input, ref long instructionPointer, List<ParameterMode> paramModes, ref long relativeBase)
        {
            long firstParam = input[instructionPointer + 1];
            long secondParam = input[instructionPointer + 2];

            long firstValue = GetParamValue(firstParam, paramModes[0], ref input, ref relativeBase);
            long secondValue = GetParamValue(secondParam, paramModes[1], ref input, ref relativeBase);

            if (firstValue != 0)
            {
                instructionPointer = secondValue;
            }
            else
            {
                instructionPointer += 3;
            }
        }
    }
}
