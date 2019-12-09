using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public class RelativeBaseOperation : OperationStrategy
    {
        public override void Execute(ref long[] input, ref long instructionPolonger, List<ParameterMode> paramModes, ref long relativeBase)
        {
            long param = input[instructionPolonger + 1];

            long value = GetParamValue(param, paramModes[0], ref input, ref relativeBase);

            relativeBase += value;

            instructionPolonger += 2;
        }
    }
}
