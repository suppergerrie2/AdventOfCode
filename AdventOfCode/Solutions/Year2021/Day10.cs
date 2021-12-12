using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day10
    {
        public Day10()
        {
            var input = InputHelper.ReadAllLines(10, 2021).ToArray();
            // var input =
            //     "[({(<(())[]>[[{[]{<()<>>\n[(()[<>])]({[<{<<[]>>(\n{([(<{}[<>[]}>{[]{[(<()>\n(((({<>}<{<{<>}{[]{[]{}\n[[<[([]))<([[{}[[()]]]\n[{[{({}]{}}([{[{{{}}([]\n{<[[]]>}<{[{[{[]{()[[[]\n[<(<(<(<{}))><([]([]()\n<{([([[(<>()){}]>(<<{{\n<{([{{}}[<[[[<>{}]]]>[]]"
            //         .Split("\n");

            Dictionary<char, int> scores = new Dictionary<char, int>()
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137},
            };

            int score = 0;

            List<long> completeScores = new List<long>();

            foreach (string s in input)
            {
                char? c = FindFirstInvalidChar(s);

                if (c != null)
                {
                    score += scores[c.Value];
                }
                else
                {
                    completeScores.Add(CalculateAutoCompleteScore(s));
                }
            }
            
            Console.WriteLine(score);
            
            completeScores.Sort();
            
            Console.WriteLine(completeScores[completeScores.Count/2]);
        }

        HashSet<char> openingChars = new HashSet<char>() { '(', '{', '[', '<' };
        Dictionary<char, char> closeToOpen = new()
        {
            {')', '('},
            {']', '['},
            {'}', '{'},
            {'>', '<'},
        };      
        
        Dictionary<char, int> autoCompleteScores = new Dictionary<char, int>()
        {
            {'(', 1},
            {'[', 2},
            {'{', 3},
            {'<', 4},
        };

        char? FindFirstInvalidChar(string s)
        {
            Stack<char> opened = new Stack<char>();
            foreach (char c in s)
            {
                if (openingChars.Contains(c))
                {
                    opened.Push(c);
                }
                else
                {
                    char opening = closeToOpen[c];
                    char expected = opened.Pop();

                    if (opening != expected)
                    {
                        return c;
                    }
                }
            }

            return null;
        } 
        
        long CalculateAutoCompleteScore(string s)
        {
            Stack<char> opened = new Stack<char>();
            foreach (char c in s)
            {
                if (openingChars.Contains(c))
                {
                    opened.Push(c);
                }
                else
                {
                    char opening = closeToOpen[c];
                    char expected = opened.Pop();

                    if (opening != expected)
                    {
                        throw new Exception();
                    }
                }
            }

            long score = 0;

            while (opened.Count > 0)
            {
                char next = opened.Pop();

                score = score * 5 + autoCompleteScores[next];
            }

            return score;
        }
    }
}