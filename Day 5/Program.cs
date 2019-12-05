using System;

namespace Day_5
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Part1Solution();
            Part2Solution();
        }

        private static void Part1Solution()
        {
            RunProgram(1);
        }

        private static void Part2Solution()
        {
            RunProgram(5);
        }

        private static void RunProgram(int inputId)
        {
            var memoryNumbersArray = ReadInput("../../../puzzle.input");

            int instructionPointer = 0;
            while (memoryNumbersArray[instructionPointer] != 99)
            {
                var opCodeExtended = memoryNumbersArray[instructionPointer];
                var opCode = GetOpCode(opCodeExtended);

                var firstInputPosition = memoryNumbersArray[instructionPointer + 1];
                var secondInputPosition = memoryNumbersArray[instructionPointer + 2];
                int outputPosition;

                var firstParamMode = GetFirstParameterMode(opCodeExtended);
                var secondParamMode = GetSecondParameterMode(opCodeExtended);
                int thirdParamMode;

                int valueToStore;
                int firstInput;
                int secondInput;
                switch (opCode)
                {
                    case 1:
                        outputPosition = memoryNumbersArray[instructionPointer + 3];
                        thirdParamMode = GetThirdParameterMode(opCodeExtended);
                        firstInput = firstParamMode == 0 ? memoryNumbersArray[firstInputPosition] : firstInputPosition;
                        secondInput = secondParamMode == 0 ? memoryNumbersArray[secondInputPosition] : secondInputPosition;
                        valueToStore = firstInput + secondInput;
                        if (thirdParamMode == 0)
                        {
                            memoryNumbersArray[outputPosition] = valueToStore;
                        }
                        else
                        {
                            memoryNumbersArray[instructionPointer + 3] = valueToStore;
                        }
                        instructionPointer += 4;
                        break;
                    case 2:
                        outputPosition = memoryNumbersArray[instructionPointer + 3];
                        thirdParamMode = GetThirdParameterMode(opCodeExtended);
                        firstInput = firstParamMode == 0 ? memoryNumbersArray[firstInputPosition] : firstInputPosition;
                        secondInput = secondParamMode == 0 ? memoryNumbersArray[secondInputPosition] : secondInputPosition;
                        valueToStore = firstInput * secondInput;
                        if (thirdParamMode == 0)
                        {
                            memoryNumbersArray[outputPosition] = valueToStore;
                        }
                        else
                        {
                            memoryNumbersArray[instructionPointer + 3] = valueToStore;
                        }
                        instructionPointer += 4;
                        break;
                    case 3:
                        if (firstParamMode == 0)
                        {
                            memoryNumbersArray[firstInputPosition] = inputId;
                        }
                        else
                        {
                            memoryNumbersArray[instructionPointer + 1] = inputId;
                        }
                        instructionPointer += 2;
                        break;
                    case 4:
                        int toOutput;
                        if (firstParamMode == 0)
                        {
                            toOutput = memoryNumbersArray[firstInputPosition];
                        }
                        else
                        {
                            toOutput = memoryNumbersArray[instructionPointer + 1];
                        }
                        Console.WriteLine(toOutput);
                        instructionPointer += 2;
                        break;
                    case 5:
                        firstInput = firstParamMode == 0 ? memoryNumbersArray[firstInputPosition] : firstInputPosition;
                        secondInput = secondParamMode == 0 ? memoryNumbersArray[secondInputPosition] : secondInputPosition;

                        if (firstInput != 0)
                        {
                            instructionPointer = secondInput;
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 6:
                        firstInput = firstParamMode == 0 ? memoryNumbersArray[firstInputPosition] : firstInputPosition;
                        secondInput = secondParamMode == 0 ? memoryNumbersArray[secondInputPosition] : secondInputPosition;

                        if (firstInput == 0)
                        {
                            instructionPointer = secondInput;
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 7:
                        outputPosition = memoryNumbersArray[instructionPointer + 3];
                        thirdParamMode = GetThirdParameterMode(opCodeExtended);
                        firstInput = firstParamMode == 0 ? memoryNumbersArray[firstInputPosition] : firstInputPosition;
                        secondInput = secondParamMode == 0 ? memoryNumbersArray[secondInputPosition] : secondInputPosition;

                        if (firstInput < secondInput)
                        {
                            valueToStore = 1;
                        }
                        else
                        {
                            valueToStore = 0;
                        }

                        if (thirdParamMode == 0)
                        {
                            memoryNumbersArray[outputPosition] = valueToStore;
                        }
                        else
                        {
                            memoryNumbersArray[instructionPointer + 3] = valueToStore;
                        }
                        instructionPointer += 4;
                        break;
                    case 8:
                        outputPosition = memoryNumbersArray[instructionPointer + 3];
                        thirdParamMode = GetThirdParameterMode(opCodeExtended);
                        firstInput = firstParamMode == 0 ? memoryNumbersArray[firstInputPosition] : firstInputPosition;
                        secondInput = secondParamMode == 0 ? memoryNumbersArray[secondInputPosition] : secondInputPosition;

                        if (firstInput == secondInput)
                        {
                            valueToStore = 1;
                        }
                        else
                        {
                            valueToStore = 0;
                        }

                        if (thirdParamMode == 0)
                        {
                            memoryNumbersArray[outputPosition] = valueToStore;
                        }
                        else
                        {
                            memoryNumbersArray[instructionPointer + 3] = valueToStore;
                        }
                        instructionPointer += 4;
                        break;
                    default:
                        throw new Exception("Not valid opCode when single digit opCode");
                }
            }
        }

        private static int[] ReadInput(string input)
        {
            string[] lines = System.IO.File.ReadAllLines(input);
            return Array.ConvertAll(lines[0].Split(","), s => int.Parse(s));
        }

        private static int GetOpCode(int value)
        {
            return GetDigit(value, 1);
        }

        private static int GetFirstParameterMode(int value)
        {
            return GetDigit(value, 3);
        }

        private static int GetSecondParameterMode(int value)
        {
            return GetDigit(value, 4);
        }

        private static int GetThirdParameterMode(int value)
        {
            return GetDigit(value, 5);
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
}
