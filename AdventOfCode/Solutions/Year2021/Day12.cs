using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Solutions.Year2021
{
    public class Day12
    {
        class Path
        {
            private List<Cave> _caves = new List<Cave>();
            private bool multiVisitedSmall = false;

            public Path(Path path, Cave cave)
            {
                multiVisitedSmall = path.multiVisitedSmall;
                _caves.AddRange(path._caves);
                if (cave._small && cave._name != "start" && _caves.Contains(cave)) multiVisitedSmall = true;

                _caves.Add(cave);
            }

            public Path(Cave path)
            {
                _caves = new List<Cave> { path };
            }

            public bool CanVisited(Cave cave)
            {
                if (!cave._small) return true;
                if (cave._name == "start") return false;

                if (!_caves.Contains(cave)) return true;

                if (!multiVisitedSmall && _caves.Count(c => c == cave) <= 1)
                {
                    return true;
                }

                return false;
            }

            public bool Contains(Cave cave)
            {
                return _caves.Contains(cave);
            }
        }
        
        [DebuggerDisplay("{_name}")]
        class Cave
        {
            internal bool _small;
            internal string _name;

            internal List<Cave> ConnectedCaves = new List<Cave>();

            public Cave(string name)
            {
                _name = name;
                _small = char.IsLower(name[0]);
            }

            public List<Path> DFS(Path path, string end, Func<Path, Cave, bool> canVisited)
            {
                if (_name == end) return new List<Path> {path};

                path = new Path(path, this);
                var paths = new List<Path>();
                foreach (Cave connectedCave in ConnectedCaves)
                {
                    if (!canVisited(path, connectedCave)) continue;

                    paths.AddRange(connectedCave.DFS(path, end, canVisited));
                }

                return paths;
            }
        }
        
        public Day12()
        {
            var input = InputHelper.ReadAllLines(12, 2021);
            // var input = "start-A\nstart-b\nA-c\nA-b\nb-d\nA-end\nb-end".Split("\n");
            // var input = "dc-end\nHN-start\nstart-kj\ndc-start\ndc-HN\nLN-dc\nHN-end\nkj-sa\nkj-HN\nkj-dc".Split("\n");
            // var input = "fs-end\nhe-DX\nfs-he\nstart-DX\npj-DX\nend-zg\nzg-sl\nzg-pj\npj-he\nRW-he\nfs-DX\npj-RW\nzg-RW\nstart-pj\nhe-WI\nzg-he\npj-fs\nstart-RW".Split("\n");


            Dictionary<string, Cave> idToCave = new Dictionary<string, Cave>();
            
            foreach (string connection in input)
            {
                string[] parts = connection.Split("-");

                if (!idToCave.ContainsKey(parts[0]))
                {
                    idToCave.Add(parts[0], new Cave(parts[0]));
                }
                
                if (!idToCave.ContainsKey(parts[1]))
                {
                    idToCave.Add(parts[1], new Cave(parts[1]));
                }

                Cave a = idToCave[parts[0]];
                Cave b = idToCave[parts[1]];

                a.ConnectedCaves.Add(b);
                b.ConnectedCaves.Add(a);
            }

            var part1 = idToCave["start"].DFS(new Path(idToCave["start"]), "end", (p, c) => !c._small || !p.Contains(c));
            var part2 = idToCave["start"].DFS(new Path(idToCave["start"]), "end", (p, c) => p.CanVisited(c));
            
            Console.WriteLine(part1.Count);
            Console.WriteLine(part2.Count);
        }
    }
}