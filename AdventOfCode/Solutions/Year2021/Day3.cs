using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day3
    {
        public Day3()
        {
            var inputLines = InputHelper.ReadAllLines(3, 2021).ToArray();
            // var inputLines = "00100\n11110\n10110\n10111\n10101\n01111\n00111\n11100\n10000\n11001\n00010\n01010"
            //     .Split("\n").ToArray();

            (int[] zeroes, int[] ones) = Count(inputLines.Select(i => Convert.ToInt32(i, 2)).ToList(), inputLines[0].Length);

            int gamma = 0;
            int epsilon = 0;
            for (int i = zeroes.Length - 1; i >= 0; i--)
            {
                if (ones[i] > zeroes[i])
                {
                    gamma |= 1;
                }
                else
                {
                    epsilon |= 1;
                }

                epsilon <<= 1;
                gamma <<= 1;
            }

            epsilon >>= 1;
            gamma >>= 1;

            Console.WriteLine(gamma * epsilon);
            int count = inputLines[0].Length;

            var oxygen = inputLines.Select(s => Convert.ToInt32(s, 2)).ToList();
            for (int i = count-1; i >= 0; i--)
            {
                (zeroes, ones) = Count(oxygen, count);
                if (oxygen.Count > 1)
                {
                    oxygen.RemoveAll(o =>
                    {
                        if (zeroes[i] > ones[i])
                        {
                            return (o & (1 << i)) > 0;
                        }
                        
                        return (o & (1 << i)) == 0;
                    });
                }
            }            
            
            var co2 = inputLines.Select(s => Convert.ToInt32(s, 2)).ToList();
            for (int i = count-1; i >= 0; i--)
            {
                (zeroes, ones) = Count(co2, count);
                if (co2.Count > 1)
                {
                    co2.RemoveAll(o =>
                    {
                        if (zeroes[i] <= ones[i])
                        {
                            return (o & (1 << i)) > 0;
                        }
                        
                        return (o & (1 << i)) == 0;
                    });
                }
            }
            
            Console.WriteLine(co2[0]*oxygen[0]);
        }
        
        (int[], int[]) Count(List<int> input, int size)
        {
            int[] zeroes = new int[size];
            int[] ones = new int[size];

            foreach (int i in input)
            {
                int val = i;
                for (int j = 0; j < zeroes.Length; j++)
                {
                    if ((val & 1) == 0)
                    {
                        zeroes[j]++;
                    }
                    else
                    {
                        ones[j]++;
                    }

                    val >>= 1;
                }
            }

            return (zeroes, ones);
        }
    }
}