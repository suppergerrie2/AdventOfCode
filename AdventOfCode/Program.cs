using System;
using System.IO;
using System.Linq;
using AdventOfCode.Solutions.Year2019;

namespace AdventOfCode
{
    static class Program
    {
        static void Main(string[] args)
        {
            int year = DateTime.Now.Year;
            int[] daysToRun;

            // Default the day to the current day if there is an advent of code going on.
            // Else default to an empty array.
            if (DateTime.Now.Month == 12 && DateTime.Now.Day <= 25)
            {
                daysToRun = new[] { DateTime.Now.Day };
            }
            else
            {
                daysToRun = Array.Empty<int>();
            }
            
            foreach (string arg in args)
            {
                if (arg.Contains("--year="))
                {
                    year = int.Parse(arg.Split('=')[1]);
                } else if (arg.Contains("--days="))
                {
                    if (arg.Split("=")[1].Trim().Equals("*"))
                    {
                        daysToRun = Enumerable.Range(1, 25).ToArray();
                        continue;
                    }
                    
                    daysToRun = arg.Split('=')[1].Split(",").Select(s => s.Trim()).Select(int.Parse).Where(i => i is > 0 and < 26).ToArray();
                } else if (arg.Contains("--day="))
                {
                    daysToRun = new[] { int.Parse(arg.Split("=")[1]) };
                }
            }
            
            if (daysToRun.Length == 0)
            {
                Console.WriteLine("No days to run");
                return;
            }
            
            Console.WriteLine("Running days: " + string.Join(", ", daysToRun));
            
            foreach (int day in daysToRun)
            {
                Console.WriteLine();
                Console.WriteLine("Day " + day);
                Type type = Type.GetType($"AdventOfCode.Solutions.Year{year}.Day{day}");

                if (type == null)
                {
                    Console.WriteLine($"[WARN] Year {year}, day " + day + " not implemented");
                    continue;
                }
                
                var dayInstance = Activator.CreateInstance(type);

                if (dayInstance is Day day2019)
                {
                    Console.WriteLine("Part 1: ");
                    day2019.Part1();

                    Console.WriteLine("Part 2: ");
                    day2019.Part2();
                }
            }
        }
    }
}