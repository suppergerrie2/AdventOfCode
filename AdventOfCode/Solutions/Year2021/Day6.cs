using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day6
    {
        public Day6()
        {
            Part1();
            Part2();
        }

        void Part1()
        {
            var input = InputHelper.ReadAllText(6, 2021).Split(",").Select(int.Parse).ToList();
            // var input = "3,4,3,1,2".Split(",").Select(int.Parse).ToList();

            const int days = 80;
            for (int i = 0; i < days; i++)
            {
                for (var j = input.Count - 1; j >= 0; j--)
                {
                    if (input[j] == 0)
                    {
                        input[j] = 6;
                        input.Add(8);
                    }
                    else
                    {
                        input[j]--;
                    }
                }
            }

            Console.WriteLine(input.Count);
        }

        void Part2()
        {
            // var input = InputHelper.ReadAllText(6, 2021).Split(",").Select(int.Parse).ToList();
            var input = "3,4,3,1,2".Split(",").Select(int.Parse).ToList();

            var fishTimers = new Dictionary<int, long>();

            for (int i = 0; i < 9; i++)
            {
                fishTimers.Add(i, 0);
            }
            
            foreach (int i in input)
            {
                fishTimers[i]++;
            }

            const int days = 256;
            for (int i = 0; i < days; i++)
            {
                long spawning = fishTimers[0];

                fishTimers[0] = fishTimers[1];
                fishTimers[1] = fishTimers[2];
                fishTimers[2] = fishTimers[3];
                fishTimers[3] = fishTimers[4];
                fishTimers[4] = fishTimers[5];
                fishTimers[5] = fishTimers[6];
                fishTimers[6] = fishTimers[7] + spawning;
                fishTimers[7] = fishTimers[8];
                fishTimers[8] = spawning;
            }

            Console.WriteLine(fishTimers.Values.Sum());
        }
    }
}