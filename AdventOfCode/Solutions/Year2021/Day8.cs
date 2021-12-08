using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day8
    {
        public Day8()
        {
            Part1();
            
            var input = InputHelper.ReadAllLines(8, 2021).ToArray();
            // var input =
            //     "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe\nedbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc\nfgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg\nfbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb\naecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea\nfgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb\ndbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe\nbdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef\negadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb\ngcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
            //         .Split("\n").ToArray();
            // var input = new string[] { "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf" };
            // var input = new string[] { "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb" };

            int total = 0;
            
            foreach (string s in input)
            {
                Dictionary<int, string> numberToString = new Dictionary<int, string>();

                string[] parts = s.Split("|").Select(s => s.Trim()).ToArray();

                string[] digits = parts[0].Split(" ");
                foreach (string digit in digits)
                {
                    switch (digit.Trim().Length)
                    {
                        case 2:
                            numberToString.Add(1, digit);
                            break;
                        case 3:
                            numberToString.Add(7, digit);
                            break;
                        case 4:
                            numberToString.Add(4, digit);
                            break;
                        case 7:
                            numberToString.Add(8, digit);
                            break;
                    }
                }

                var mappings = new Dictionary<char, char>();

                char aIs = numberToString[7].Except(numberToString[1]).First();
                mappings.Add(aIs, 'a');

                char gIs = digits.Select(s => s.Except(numberToString[4]).Where(c => c != aIs).ToList()).First(s => s.Count == 1)[0];
                mappings.Add(gIs, 'g');

                char eIs = digits.Select(s => s.Except(numberToString[4]).Where(c => c != aIs && c != gIs).ToList()).First(s => s.Count == 1)[0];
                mappings.Add(eIs, 'e');

                char dIs = digits.Where(s => s.Length == 5).Select(s => s.Except(numberToString[7]).Where(c => c != gIs && c != eIs).ToList()).First(s => s.Count == 1)[0];
                mappings.Add(dIs, 'd');

                char bIs = digits.Select(s => s.Except(numberToString[7]).Where(c => c != gIs && c != dIs && c != eIs).ToList()).First(s => s.Count == 1)[0];
                mappings.Add(bIs, 'b');

                char fIs = digits.Where(s => s.Length == 6).Select(s => s.Where(c => c != aIs && c != bIs && c != dIs && c != eIs && c != gIs).ToList()).First(s => s.Count == 1)[0];
                mappings.Add(fIs, 'f');

                var cs = numberToString[8]
                    .Where(c => c != aIs && c != bIs && c != dIs && c != eIs && c != fIs && c != gIs).ToList();
                Debug.Assert(cs.Count == 1);
                char cIs = cs.First();
                mappings.Add(cIs, 'c');

                string value = "";
                foreach (string digit in parts[1].Split(" "))
                {
                    string trimmed = digit.Trim();
                    string mapped = new string(trimmed.Select(c => mappings[c]).OrderBy(c => c).ToArray());

                    switch (mapped)
                    {
                        case "abcefg":
                            value += "0";
                            break;
                        case "cf":
                            value += "1";
                            break;
                        case "acdeg":
                            value += "2";
                            break;
                        case "acdfg":
                            value += "3";
                            break;
                        case "bcdf":
                            value += "4";
                            break;
                        case "abdfg":
                            value += "5";
                            break;
                        case "abdefg":
                            value += "6";
                            break;
                        case "acf":
                            value += "7";
                            break;
                        case "abcdefg":
                            value += "8";
                            break;
                        case "abcdfg":
                            value += "9";
                            break;
                    }
                }
                
                // Console.WriteLine(value);
                total += int.Parse(value);
            }
            
            Console.WriteLine(total);
        }

        void Part1()
        {
            var input = InputHelper.ReadAllLines(8, 2021).ToArray();
            // var input =
            //     "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe\nedbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc\nfgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg\nfbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb\naecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea\nfgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb\ndbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe\nbdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef\negadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb\ngcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
            //         .Split("\n").ToArray();

            int ones = 0, fours = 0, sevens = 0, eights = 0;
            
            foreach (string s in input)
            {
                string[] parts = s.Split("|").Select(s => s.Trim()).ToArray();

                foreach (string digit in parts[1].Split(" "))
                {
                    switch (digit.Trim().Length)
                    {
                        case 2:
                            ones++;
                            break;
                        case 3:
                            sevens++;
                            break;
                        case 4:
                            fours++;
                            break;
                        case 7:
                            eights++;
                            break;
                    }
                }
            }
            
            Console.WriteLine(ones + sevens + fours + eights);
        }
    }
}