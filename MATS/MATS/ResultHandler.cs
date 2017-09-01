using System;
using System.Collections.Generic;
using System.Drawing;

namespace MATS
{
    /// <summary>
    /// Extracts and processes the mutation analysis results.
    /// </summary>
    public class ResultHandler
    {
        TraceManager tmOriginal;
        TraceManager[][] tmMutantArray;
        decimal stepSize;
        decimal simTime;
        decimal deltaValue;
        int testCaseCount;
        /// <summary>
        /// Creates a result handler.
        /// </summary>
        /// <param name="tmOriginal"></param>
        /// <param name="tmMutantArray"></param>
        /// <param name="mutantCount"></param>
        /// <param name="stepSize"></param>
        /// <param name="simTime"></param>
        /// <param name="deltaValue"></param>
        public ResultHandler(TraceManager tmOriginal, TraceManager[][] tmMutantArray, int testCaseCount, decimal stepSize, decimal simTime, decimal deltaValue)
        {
            this.testCaseCount = testCaseCount;
            this.tmOriginal = tmOriginal;
            this.tmMutantArray = tmMutantArray;
            this.stepSize = stepSize;
            this.simTime = simTime;
            this.deltaValue = deltaValue;
        }
        /// <summary>
        /// Selects the test cases which yield closest to full coverage.
        /// </summary>
        /// <param name="resultTable"></param>
        /// <returns></returns>
        public static List<int> SelectBestTestCase(List<List<int>> resultTable)
        {
            List<int> testCases = new List<int>();
            testCases.Add(0);
            for (int i = 1; i < resultTable.Count; i++)
            {
                bool add = true;
                for (int j = 0; j < testCases.Count; j++)
                {
                    int result = CompareTestArray(resultTable[testCases[j]].ToArray(), resultTable[i].ToArray());
                    if (result == 2)
                    {
                        testCases[j] = i;
                        add = false;
                    }
                    else if (result == 0)
                    {
                        add = false;
                    }
                }
                if (add)
                    testCases.Add(i);
            }
            // Debugging
            foreach (int item in testCases)
            {
                Console.WriteLine(item);
            }

            for (int j = 0; j < testCases.Count; j++)
            {
                bool same = false;
                for (int k = j + 1; k < testCases.Count; k++)
                {
                    if (testCases[j] == testCases[k])
                    {
                        same = true;
                        break;
                    }
                }
                if (same)
                {
                    testCases.Remove(testCases[j]);
                    j--;
                }
            }
            return testCases;
        }

        /// <summary>
        /// Compares two equal sized test case results.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>0 = equal, 1 = different, 2 = same but more.</returns>
        private static int CompareTestArray(int[] first, int[] second)
        {
            bool containsOther, different;
            int size = first.Length;
            containsOther = different = false;
            for (int i = 0; i < size; i++)
            {
                if (first[i] != second[i])
                {
                    if (first[i] == 0)
                        containsOther = true;
                    else
                        different = true;
                }
            }
            if (containsOther && !different)
                return 2;
            else if (containsOther && different)
                return 1;
            else
                return 0;
        }
        /// <summary>
        /// Builds a result table where 1 represents detected mutant and 0 undetected
        /// </summary>
        /// <param name="parameter">The parameter to compare.</param>
        /// <returns></returns>
        public List<List<int>> compareMutants(string parameter)
        {
            List<List<int>> resultTable = new List<List<int>>();
            // Sets the trace to even steps with the mutants.
            tmOriginal.Interpolate(stepSize, simTime);
            // Loop through every test case.
            for (int i = 0; i < testCaseCount; i++)
            {
                resultTable.Add(new List<int>());
                // Gets the reference trace from testcase[i].
                List<PointF> reference = tmOriginal.GetTrace(parameter).GetInterpolatedTrace(i);
                // Loops through all mutants.
                foreach (TraceManager items in tmMutantArray[i])
                {
                    // Interpolates to same step size as reference.
                    items.Interpolate(stepSize, simTime);
                    // Gets the trace.
                    List<PointF> mutant = items.GetTrace(parameter).GetInterpolatedTrace(0);
                    // Compares the reference to the mutant to see if they differ more than delta value.
                    int result = CompareOutput(reference, mutant, (float)deltaValue);
                    if (result == -1)
                    {
                        Console.WriteLine("unequal length");
                    }
                    // Adds the result.
                    resultTable[i].Add(result);
                }
            }
            return resultTable;
        }
        /// <summary>
        /// Compares two point lists to see if they differ more than the provided delta.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        private int CompareOutput(List<PointF> first, List<PointF> second, float delta)
        {
            if (first.Count != second.Count)
                return -1;            
            int i = 0;
            while (i < first.Count)
            {
                float calc = Math.Abs(first[i].Y - second[i].Y);
                if (calc >= delta)
                    return 1;                
                i++;
            }
            return 0;
        }
    }
}
