using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace AdventOfCode
{
    public static class InputHelper
    {
        private static readonly string InputPath = Environment.GetEnvironmentVariable("INPUT_PATH") ??
                                                   Path.Join(
                                                       Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                                       "AOCInputs");

        private static WebClient _webClient;

        static InputHelper()
        {
            _webClient = new();
            _webClient.Headers.Add("user-agent", "https://github.com/suppergerrie2/AdventOfCode");
            _webClient.Headers.Add("cookie", "session=" + Environment.GetEnvironmentVariable("AOC_SESSION"));

            _webClient.DownloadProgressChanged += (sender, args) =>
            {
                Console.WriteLine($"Downloading  {args.ProgressPercentage}%");
            };
            _webClient.DownloadFileCompleted += (sender, args) =>
            {
                if (args.Error != null)
                {
                    Console.Error.WriteLine("Error downloading input file: " + args.Error.Message);
                    return;
                }

                Console.WriteLine($"Download finished!");
            };
        }

        public static T[] ReadArray<T>(string s, Func<string, T> parser, string separator = " ")
        {
            return s.Split(separator).Select(parser).ToArray();
        }

        public static int[] ReadIntArray(string s, string separator = " ")
        {
            return ReadArray(s, int.Parse, separator);
        }

        public static int[,] Read2DIntArray(int day, int year)
        {
            string[] lines = ReadAllLines(day, year).ToArray();

            int[,] arr = new int[lines.Length, lines[0].Length];
            
            for (var i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    arr[i, j] = int.Parse(lines[i][j].ToString());
                }
            }

            return arr;
        }

        private static string GetPath(int day, int year)
        {
            string path = Path.Join(InputPath, year.ToString(), $"Day{day}.txt");

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                string inputSource = $"https://adventofcode.com/{year}/day/{day}/input";
                Console.WriteLine($"Downloading input file from '{inputSource}'...");
                _webClient.DownloadFile(inputSource, path);
            }

            return path;
        }

        public static FileStream LoadFile(int day, int year)
        {
            return File.OpenRead(GetPath(day, year));
        }

        public static IEnumerable<string> ReadAllLines(int day, int year)
        {
            return File.ReadAllLines(GetPath(day, year));
        }

        public static string ReadAllText(int day, int year)
        {
            return File.ReadAllText(GetPath(day, year));
        }
    }
}