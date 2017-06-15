using System;
using System.Numerics;

namespace NT.ExamTasks.Election
{
    public class Program
    {
        static void Main()
        {
            int k = int.Parse(Console.ReadLine());
            int n = int.Parse(Console.ReadLine());
            var seatsPerN = new int[n];
            for (int i = 0; i < n; i++)
            {
                seatsPerN[i] = int.Parse(Console.ReadLine());
            }

            Array.Sort(seatsPerN);
            var count = GetCombinationCount(seatsPerN, k);

            Console.WriteLine(count);
        }

        private static BigInteger GetCombinationCount(int[] seatsPerN, int k)
        {
            var combinations = new BigInteger[(100 * 1000) + 1];
            var maxSum = 0;

            combinations[0] = 1;

            foreach (var seats in seatsPerN)
            {
                for (int i = maxSum; i >= 0; i--)
                {
                    if (combinations[i] > 0)
                    {
                        combinations[i + seats] += combinations[i];
                    }
                }

                maxSum += seats;
            }

            var countOfSolutions = new BigInteger(0);
            for (int f = k; f <= maxSum; f++)
            {
                countOfSolutions += combinations[f];
            }

            return countOfSolutions;
        }
    }
}
