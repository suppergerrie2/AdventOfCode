using System;

namespace AdventOfCode
{
    public class Utils
    {
        private static void Print(int[,] input)
        {
            for (var i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    Console.Write(input[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}