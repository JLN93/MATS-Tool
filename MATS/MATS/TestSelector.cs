using System;
using System.Collections.Generic;

namespace MATS
{
   public static class TestSelector
    {
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
    }
}
