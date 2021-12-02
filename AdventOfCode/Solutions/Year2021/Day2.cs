using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day2
    {
        public Day2()
        {
            string[] input = InputHelper.ReadAllLines(2, 2021).ToArray();
            // string[] input = "forward 5\ndown 5\nforward 8\nup 3\ndown 8\nforward 2".Split("\n");
            
            int horizontalPosition = 0;
            int depth = 0;
            
            foreach (string line in input)
            {
                string[] parts = line.Split(" ");

                if (parts[0].Trim().Equals("forward"))
                {
                    horizontalPosition += int.Parse(parts[1]);
                } else if (parts[0].Trim().Equals("down"))
                {
                    depth += int.Parse(parts[1]);
                } else if (parts[0].Trim().Equals("up"))
                {
                    depth -= int.Parse(parts[1]);
                }
            }
            
            Console.WriteLine(horizontalPosition * depth);
            
            horizontalPosition = 0;
            depth = 0;
            int aim = 0;
            
            foreach (string line in input)
            {
                string[] parts = line.Split(" ");

                if (parts[0].Trim().Equals("forward"))
                {
                    horizontalPosition += int.Parse(parts[1]);
                    depth += aim * int.Parse(parts[1]);
                } else if (parts[0].Trim().Equals("down"))
                {
                    aim += int.Parse(parts[1]);
                } else if (parts[0].Trim().Equals("up"))
                {
                    aim -= int.Parse(parts[1]);
                }
            }
            
            Console.WriteLine(horizontalPosition * depth);
        }
    }
}