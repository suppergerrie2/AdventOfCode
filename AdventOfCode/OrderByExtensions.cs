using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class OrderByExtensions
    {
        public static IOrderedEnumerable<int> OrderByDescending(this IEnumerable<int> source) =>
            source.OrderByDescending(i => i);
        
        public static IOrderedEnumerable<int> OrderBy(this IEnumerable<int> source) =>
            source.OrderBy(i => i);
    }
}