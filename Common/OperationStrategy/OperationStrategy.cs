using System;
using System.Collections.Generic;

namespace Common.OperationStrategy
{
    public interface IOperationStrategy
    {
        void Execute(ref long[] input, ref long instructionPointer, List<ParameterMode> paramModes, ref long relativeBase);
    }

    public abstract class OperationStrategy : IOperationStrategy
    {
        public abstract void Execute(ref long[] input, ref long instructionPointer, List<ParameterMode> paramModes, ref long relativeBase);

        protected long GetParamValue(long param, ParameterMode parameterMode, ref long[] input, ref long relativeBase)
        {
            switch (parameterMode)
            {
                case ParameterMode.Immediate:
                    return param;
                case ParameterMode.Position:
                    CheckIndexExistAndResizeIfNeeded(ref input, param);
                    return input[param];
                case ParameterMode.Relative:
                    CheckIndexExistAndResizeIfNeeded(ref input, relativeBase + param);
                    return input[relativeBase + param];
                default:
                    throw new Exception("Parameter mode not valid");
            }
        }

        protected long GetAddress(long param, ParameterMode parameterMode, ref long[] input, ref long relativeBase)
        {
            switch (parameterMode)
            {
                case ParameterMode.Immediate:
                    CheckIndexExistAndResizeIfNeeded(ref input, param);
                    return param;
                case ParameterMode.Position:
                    CheckIndexExistAndResizeIfNeeded(ref input, param);
                    return param;
                case ParameterMode.Relative:
                    CheckIndexExistAndResizeIfNeeded(ref input, relativeBase + param);
                    return relativeBase + param;
                default:
                    throw new Exception("Parameter mode not valid");
            }
        }

        public void CheckIndexExistAndResizeIfNeeded(ref long[] array, long index)
        {
            if (!IndexExists(array, index))
            {
                ResizeArray(ref array, index);
            }
        }

        protected bool IndexExists(long[] array, long index)
        {
            return index < array.Length;
        }

        protected void ResizeArray(ref long[] array, long size)
        {
            Array.Resize(ref array, (int)size + 1);
        }
    }
}
