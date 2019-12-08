using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_7_Solver
{
    public static class Day7Solver
    {
        public static int Part1Solution(int[] input)
        {
            IntCodeProgram[] amplifiers = new IntCodeProgram[5];
            int highestScore = Normal(input, amplifiers);
            return highestScore;
        }

        public static int Part2Solution(int[] input)
        {
            IntCodeProgram[] amplifiers = new IntCodeProgram[5];
            int highestScore = Feedback(input, amplifiers);
            return highestScore;
        }

        private static int Normal(int[] input, IntCodeProgram[] amplifiers)
        {
            var allPhaseSettingSequences = GetPermutations(Enumerable.Range(0, 5).ToList());
            int highestScore = 0;

            foreach (var phaseSettingSequence in allPhaseSettingSequences)
            {
                NormalSet(input, amplifiers, phaseSettingSequence);
                highestScore = Math.Max(amplifiers.Last().Output.Last(), highestScore);
            }
            return highestScore;
        }

        private static int Feedback(int[] input, IntCodeProgram[] amplifiers)
        {
            var feedbackPhaseSettingSequences = GetPermutations(Enumerable.Range(5, 5).ToList());
            int highestScore = 0;

            foreach (var feedbackPhaseSettingSequence in feedbackPhaseSettingSequences)
            {
                int feedbackValue = FeedbackSet(input, amplifiers, feedbackPhaseSettingSequence);
                highestScore = Math.Max(feedbackValue, highestScore);
            }
            return highestScore;
        }

        private static void NormalSet(int[] input, IntCodeProgram[] amplifiers, List<int> phaseSettingSequence)
        {
            Queue<int> nextInput = new Queue<int>();
            for (var i = 0; i < amplifiers.Length; i++)
            {
                Queue<int> inputQueue = new Queue<int>();
                if (i == 0)
                {
                    inputQueue.Enqueue(phaseSettingSequence[i]);
                    inputQueue.Enqueue(0);
                }
                else
                {
                    inputQueue.Enqueue(phaseSettingSequence[i]);
                    inputQueue.Enqueue(nextInput.Dequeue());
                }

                amplifiers[i] = new IntCodeProgram(input, inputQueue);
                amplifiers[i].Run();
                nextInput = amplifiers[i].Output;
            }
        }

        private static int FeedbackSet(int[] input, IntCodeProgram[] amplifiers, List<int> phaseSettingSequence)
        {
            for (int i = 0; i < amplifiers.Length; i++)
            {
                amplifiers[i] = new IntCodeProgram(input, phaseSettingSequence[i]);
            }
            amplifiers[0].Input.Enqueue(0);

            Queue<IntCodeProgram> amplifiersQueue = new Queue<IntCodeProgram>(amplifiers);
            Queue<int> nextInput = new Queue<int>();
            while (amplifiersQueue.Count > 0)
            {
                IntCodeProgram amp = amplifiersQueue.Dequeue();
                OutputToInput(nextInput, amp.Input);
                Halt haltType = amp.Run();
                OutputToInput(amp.Output, nextInput);

                if (haltType == Halt.NeedInput)
                    amplifiersQueue.Enqueue(amp);
            }

            return nextInput.Single();
        }

        private static void OutputToInput(Queue<int> output, Queue<int> input)
        {
            while (output.Count > 0)
            {
                input.Enqueue(output.Dequeue());
            }
        }

        private static List<List<int>> GetPermutations(List<int> values)
        {
            if (values.Count == 1)
            {
                return new List<List<int>>() { values };
            }
            return values.SelectMany(x => GetPermutations(values.Where(y => !y.Equals(x)).ToList()), (x, p) => p.Prepend(x).ToList()).ToList();
        }
    }
}
