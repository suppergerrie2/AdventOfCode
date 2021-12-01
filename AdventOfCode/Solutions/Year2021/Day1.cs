using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day1
    {
        public Day1()
        {
            int[] input = InputHelper.ReadAllLines(1, 2021).Select(int.Parse).ToArray();
            
            int count = 0;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] > input[i - 1])
                {
                    count++;
                }
            }
            
            Console.WriteLine(count); 
            
            count = 0;
            for (int i = 1; i < input.Length-2; i++)
            {
                int sumA = input[i - 1] + input[i] + input[i + 1];
                int sumB = input[i] + input[i + 1] + input[i+2];

                if (sumB > sumA)
                {
                    count++;
                }
            }
            
            Console.WriteLine(count);
        }
    }
}