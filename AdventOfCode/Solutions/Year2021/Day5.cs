using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day5
    {
        public Day5()
        {
            var input = InputHelper.ReadAllLines(5, 2021);
            // var input =
            //     "0,9 -> 5,9\n8,0 -> 0,8\n9,4 -> 3,4\n2,2 -> 2,1\n7,0 -> 7,4\n6,4 -> 2,0\n0,9 -> 2,9\n3,4 -> 1,4\n0,0 -> 8,8\n5,5 -> 8,2"
            //         .Split("\n");

            var lines = input.Select(s =>
            {
                string[] parts = s.Split(" -> ");

                return (ParsePoint(parts[0]), ParsePoint(parts[1]));
            }).ToList();

            var overlappingCount = new Dictionary<Point, int>();

            foreach ((Point start, Point end) in lines.Where(StraightLine))
            {
                for (int y = Math.Min(start.Y, end.Y); y <= Math.Max(start.Y, end.Y); y++)
                for (int x = Math.Min(start.X, end.X); x <= Math.Max(start.X, end.X); x++)
                {
                    Point p = new Point(x, y);
                    if (!overlappingCount.ContainsKey(p))
                    {
                        overlappingCount.Add(p, 0);
                    }

                    overlappingCount[p]++;
                }
            }

            var overlappingDiagonal = new Dictionary<Point, int>(overlappingCount);
            
            foreach ((Point start, Point end) in lines.Where(l => !StraightLine(l)))
            {
                bool up = start.Y > end.Y;
                bool left = start.X > end.X;
                
                for (int i = 0; i <= Math.Abs(start.X - end.X); i++)
                {
                    int x = start.X + (left ? -i : i);
                    int y = start.Y + (up ? -i : i);
                    Point p = new(x, y);
                    if (!overlappingDiagonal.ContainsKey(p))
                    {
                        overlappingDiagonal.Add(p, 0);
                    }

                    overlappingDiagonal[p]++;
                }
            }

            int count = overlappingCount.Count(keyValuePair => keyValuePair.Value >= 2);
            Console.WriteLine(count);
            
            count = overlappingDiagonal.Count(keyValuePair => keyValuePair.Value >= 2);
            Console.WriteLine(count);
        }

        bool StraightLine((Point start, Point end) line)
        {
            if (line.start.X == line.end.X) return true;

            return line.start.Y == line.end.Y;
        }

        Point ParsePoint(string s)
        {
            var parts = s.Split(",");

            return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}