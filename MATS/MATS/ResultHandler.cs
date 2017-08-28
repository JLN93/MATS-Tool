using System;
using System.Collections.Generic;
using System.Drawing;

namespace MATS
{
   public class ResultHandler
    {
        TraceManager tmOriginal;
        TraceManager[][] tmMutantArray;
        decimal stepSize;
        decimal simTime;
        decimal deltaValue;
        int mutantCount;

        public ResultHandler(TraceManager tmOriginal, TraceManager[][] tmMutantArray, int mutantCount, decimal stepSize, decimal simTime, decimal deltaValue)
        {
            this.mutantCount = mutantCount;
            this.tmOriginal = tmOriginal;
            this.tmMutantArray = tmMutantArray;
            this.stepSize = stepSize;
            this.simTime = simTime;
            this.deltaValue = deltaValue;
        }

        public static List<int> SelectBestTestCase(List<List<int>> resultTable, int mutants)
        {
            List<int> testCases = new List<int>();
            testCases.Add(0);
            for (int i = 1; i < resultTable.Count; i++)
            {
                bool add = true;
                for (int j = 0; j < testCases.Count; j++)
                {
                    int result = CompareTestArray(resultTable[testCases[j]].ToArray(), resultTable[i].ToArray(), mutants);
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
        // 0 = equal, 1 = different, 2 = same but more
        private static int CompareTestArray(int[] first, int[] second, int size)
        {
            bool containsOther, different;
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
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<List<int>> compareMutants(string parameter)
        {
            //compare mutants
            List<List<int>> resultTable = new List<List<int>>();
            tmOriginal.Interpolate(stepSize, simTime);
            for (int i = 0; i < mutantCount; i++)
            {
                resultTable.Add(new List<int>());
                List<PointF> reference = tmOriginal.GetTrace(parameter).GetInterpolatedTrace(i);
                foreach (TraceManager items in tmMutantArray[i])
                {
                    items.Interpolate(stepSize, simTime);
                    List<PointF> mutant = items.GetTrace(parameter).GetInterpolatedTrace(0);
                    int result = CompareOutput(reference, mutant, (float)deltaValue);
                    if (result == -1)
                    {
                        Console.WriteLine("unequal length");
                    }
                    resultTable[i].Add(result);
                }
            }
            return resultTable;
        }
        private int CompareOutput(List<PointF> first, List<PointF> second, float delta)
        {
            if (first.Count != second.Count)
            {
                return -1;
            }
            int i = 0;
            while (i < first.Count)
            {
                float calc = Math.Abs(first[i].Y - second[i].Y);
                if (calc >= delta)
                {
                    return 1;
                }
                i++;
            }
            return 0;
        }

    }
}
