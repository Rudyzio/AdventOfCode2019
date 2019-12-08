using System;
using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public interface IOperationStrategy
    {
        void Execute(int[] input, ref int instructionPointer, List<ParameterMode> paramModes);
    }

    public abstract class OperationStrategy : IOperationStrategy
    {
        public abstract void Execute(int[] input, ref int instructionPointer, List<ParameterMode> paramModes);

        protected int GetParamValue(int param, ParameterMode parameterMode, int[] input)
        {
            switch (parameterMode)
            {
                case ParameterMode.Immediate:
                    return param;
                case ParameterMode.Position:
                    return input[param];
                default:
                    throw new Exception("Parameter mode not valid");
            }
        }

    }
}
