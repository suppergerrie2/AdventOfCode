using System;
using System.Collections.Generic;
using System.Drawing;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day11
    {
        public Day11()
        {
            var input = InputHelper.Read2DIntArray(11, 2021);
            // var input = InputHelper.Parse2DIntArray(
            //     "5483143223\n2745854711\n5264556173\n6141336146\n6357385478\n4167524645\n2176841721\n6882881134\n4846848554\n5283751526"
            //         .Split("\n")); 
            // var input = InputHelper.Parse2DIntArray(
            //     "12345\n19991\n19191\n19991\n11111"
            //         .Split("\n"));

            int totalFlashes = 0;

            for (int i = 0; i < 100; i++)
            {
                totalFlashes += DoStep(input);
            }
            Console.WriteLine(totalFlashes);

            int iteration = 100;
            bool simultaneousFlash = false;
            while (!simultaneousFlash)
            {
                int flashed = DoStep(input);
                iteration++;

                if (flashed == input.GetLength(0) * input.GetLength(1))
                {
                    simultaneousFlash = true;
                }
            }
            
            Console.WriteLine(iteration);
        }

        int DoStep(int[,] input)
        {
            int flashed = 0;
            Queue<Point> toFlash = new Queue<Point>();
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    input[i,j]++;

                    if (input[i, j] > 9)
                    {
                        toFlash.Enqueue(new Point(i, j));
                    }
                }
            }

            HashSet<Point> hasFlashed = new HashSet<Point>();

            while (toFlash.Count > 0)
            {
                var p = toFlash.Dequeue();

                if(hasFlashed.Contains(p)) continue;
                hasFlashed.Add(p);

                flashed++;
                input[p.X, p.Y] = 0;
                
                foreach (Size offset in DirectionHelper.DiagonalNeighbours)
                {
                    Point neighbour = p + offset;

                    var increased = Increase(neighbour.X, neighbour.Y, input);

                    if (increased != null && increased.Value > 9)
                    {
                        toFlash.Enqueue(neighbour);
                    }
                }
            }
            
            foreach (Point point in hasFlashed)
            {
                input[point.X, point.Y] = 0;
            }

            return flashed;
        }

        int? Increase(int i, int j, int[,] input)
        {
            if(i < 0 || j < 0 || i >= input.GetLength(0) || j >= input.GetLength(1)) return null;

            input[i, j]++;

            return input[i, j];
        }
    }
}