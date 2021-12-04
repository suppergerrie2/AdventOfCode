using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day4
    {
        public Day4()
        {
            var input = new Queue<string>(InputHelper.ReadAllLines(4, 2021));
            // var input = new Queue<string>(
            //     "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1\n\n22 13 17 11  0\n 8  2 23  4 24\n21  9 14 16  7\n 6 10  3 18  5\n 1 12 20 15 19\n\n 3 15  0  2 22\n 9 18 13 17  5\n19  8  7 25 23\n20 11 10 24  4\n14 21 16 12  6\n\n14 21 17 24  4\n10 16 15  9 19\n18  8 23 26 20\n22 11 13  6  5\n 2  0 12  3  7"
            //         .Split("\n"));

            var numbersToDraw = new Queue<int>(input.Dequeue().Split(",").Select(int.Parse));

            List<Board> boards = new List<Board>();
            while (input.Count > 0)
            {
                boards.Add(Board.ParseBoard(input));
            }

            while (numbersToDraw.Count > 0)
            {
                int drawn = numbersToDraw.Dequeue();

                foreach (Board board in boards)
                {
                    if (board.ApplyNumber(drawn))
                    {
                        int score = board.CalculateScore();
                        Console.WriteLine(score * drawn);
                        goto done;
                    }
                }
            }
            
            done:
            Part2();
        }

        void Part2()
        {
            var input = new Queue<string>(InputHelper.ReadAllLines(4, 2021));
            // var input = new Queue<string>(
            //     "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1\n\n22 13 17 11  0\n 8  2 23  4 24\n21  9 14 16  7\n 6 10  3 18  5\n 1 12 20 15 19\n\n 3 15  0  2 22\n 9 18 13 17  5\n19  8  7 25 23\n20 11 10 24  4\n14 21 16 12  6\n\n14 21 17 24  4\n10 16 15  9 19\n18  8 23 26 20\n22 11 13  6  5\n 2  0 12  3  7"
            //         .Split("\n"));

            var numbersToDraw = new Queue<int>(input.Dequeue().Split(",").Select(int.Parse));

            List<Board> boards = new List<Board>();
            while (input.Count > 0)
            {
                boards.Add(Board.ParseBoard(input));
            }
            
            while (numbersToDraw.Count > 0)
            {
                int drawn = numbersToDraw.Dequeue();

                for (int i = boards.Count-1; i >= 0; i--)
                {
                    Board board = boards[i];
                    if (board.ApplyNumber(drawn))
                    {
                        if (boards.Count == 1)
                        {
                            int score = board.CalculateScore();
                            Console.WriteLine(score * drawn);
                            goto done;
                        }
                        else
                        {
                            boards.RemoveAt(i);
                        }
                    }
                }
            }
            
            done:
            return;
        }

        class Board
        {
            private int[,] numbers;
            private bool[,] drawn;

            private Board(int[,] numbers)
            {
                this.numbers = numbers;
                this.drawn = new bool[5, 5];
            }

            public static Board ParseBoard(Queue<string> input)
            {
                input.Dequeue();

                int[,] numbers = new int[5, 5];
                for (int i = 0; i < 5; i++)
                {
                    string str = input.Dequeue();
                    var nums = str.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Select(int.Parse).ToArray();
                    for (var j = 0; j < nums.Length; j++)
                    {
                        numbers[i, j] = nums[j];
                    }
                }

                return new Board(numbers);
            }

            public void Log()
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Console.Write(numbers[i,j] + " ");
                    }
                    Console.WriteLine();
                }
            }

            public bool CheckWin(int i, int j)
            {
                bool won = true;
                for (int i2 = 0; i2 < 5; i2++)
                {
                    if (!drawn[i2, j])
                    {
                        won = false;
                        break;
                    }
                }

                if (won) return true;
                
                won = true;
                for (int j2 = 0; j2 < 5; j2++)
                {
                    if (!drawn[i, j2])
                    {
                        won = false;
                        break;
                    }
                }

                return won;
            }

            public bool ApplyNumber(int num)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (numbers[i, j] == num)
                        {
                            drawn[i, j] = true;

                            if (CheckWin(i, j))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            public int CalculateScore()
            {
                int sum = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (!drawn[i, j])
                        {
                            sum += numbers[i, j];
                        }
                    }
                }

                return sum;
            }
        }
    }
}