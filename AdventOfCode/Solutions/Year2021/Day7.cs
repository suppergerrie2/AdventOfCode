using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day7
    {
        public Day7()
        {
            var input = InputHelper.ReadAllText(7, 2021).Split(",").Select(int.Parse).ToArray();
            // var input = "16,1,2,0,4,2,7,1,2,14".Split(",").Select(int.Parse).ToArray();

            int min = input.Min();
            int max = input.Max();

            int minCost = int.MaxValue;
            int minH = min;
            for (int h = min; h <= max; h++)
            {
                int cost = 0;
                foreach (int i in input)
                {
                    cost += Math.Abs(h - i);
                }

                if (cost < minCost)
                {
                    minCost = cost;
                    minH = h;
                }
            }

            Console.WriteLine(minH);
            Console.WriteLine(minCost);
            
            minCost = int.MaxValue;
            minH = min;
            for (int h = min; h <= max; h++)
            {
                int cost = 0;
                foreach (int i in input)
                {
                    cost += CalcFuel(Math.Abs(h - i));
                }

                if (cost < minCost)
                {
                    minCost = cost;
                    minH = h;
                }
            }


            Console.WriteLine(minH);
            Console.WriteLine(minCost);
        }

        int CalcFuel(int movement)
        {
            return (movement * (movement + 1)) / 2;
        }
    }
}