using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day9
    {
        public Day9()
        {
            var input = InputHelper.ReadAllLines(9, 2021).Select(i => i.Select(c => int.Parse(c.ToString())).ToArray())
                .ToArray();
            // var input = "2199943210\n3987894921\n9856789892\n8767896789\n9899965678".Split("\n")
            //     .Select(i => i.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

            var seeds = new Queue<Point>();

            int sum = 0;
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    int top = GetVal(x, y + 1);
                    int bottom = GetVal(x, y - 1);
                    int right = GetVal(x + 1, y);
                    int left = GetVal(x - 1, y);
                    int curr = GetVal(x, y);

                    if (top > curr && bottom > curr && right > curr && left > curr)
                    {
                        sum += curr + 1;

                        seeds.Enqueue(new Point(x, y));
                    }
                }
            }
            Console.WriteLine(sum);

            var basins = new List<HashSet<Point>>();

            Size[] neighbours = {
                new(0, 1),
                new(0, -1),
                new(1, 0),
                new(-1, 0),
            };

            while (seeds.Count > 0)
            {
                Queue<Point> toVisit = new Queue<Point>();
                Point seed = seeds.Dequeue();
                
                if(basins.Any(b => b.Contains(seed))) continue;

                HashSet<Point> visited = new HashSet<Point>();
                toVisit.Enqueue(seed);

                while (toVisit.Count > 0)
                {
                    Point p = toVisit.Dequeue();
                    foreach (Size offset in neighbours)
                    {
                        Point newNeighbour = p + offset;
                        
                        if(visited.Contains(newNeighbour)) continue;
                        if (GetVal(newNeighbour.X, newNeighbour.Y) >= 9) continue;
                        
                        visited.Add(newNeighbour);
                        toVisit.Enqueue(newNeighbour);
                    }
                }
                
                basins.Add(visited);
            }

            Console.WriteLine(basins.Select(o => o.Count).OrderByDescending().Take(3).Aggregate((a, b) => a * b));
            
            int GetVal(int x, int y)
            {
                if (x < 0 || y < 0 || y >= input.Length)
                {
                    return int.MaxValue;
                }

                return x >= input[y].Length ? int.MaxValue : input[y][x];
            }
        }
    }
}