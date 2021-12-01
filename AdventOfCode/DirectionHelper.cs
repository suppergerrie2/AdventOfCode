using System;
using System.Drawing;

namespace AdventOfCode
{
    public static class DirectionHelper
    {
        public static readonly Direction[] Directions = { Direction.Up, Direction.Right, Direction.Down, Direction.Left };

        public enum Direction { Up, Right, Down, Left }

        public static Direction Rotate90Degrees(this Direction dir, bool clockWise = true)
        {
            switch (dir)
            {
                default:
                case Direction.Up:
                    return clockWise ? Direction.Right : Direction.Left;
                case Direction.Right:
                    return clockWise ? Direction.Down : Direction.Up;
                case Direction.Left:
                    return clockWise ? Direction.Up : Direction.Down;
                case Direction.Down:
                    return clockWise ? Direction.Left : Direction.Right;
            }
        }

        public static void Offset(this ref Point p, Direction dir)
        {
            switch (dir)
            {
                default:
                case Direction.Up:
                    p.Y++;
                    break;
                case Direction.Right:
                    p.X++;
                    break;
                case Direction.Left:
                    p.X--;
                    break;
                case Direction.Down:
                    p.Y--;
                    break;
            }
        }

        public static Point OffsetImmutable(this Point p, Direction dir)
        {
            switch (dir)
            {
                default:
                case Direction.Up:
                    p.Y++;
                    break;
                case Direction.Right:
                    p.X++;
                    break;
                case Direction.Left:
                    p.X--;
                    break;
                case Direction.Down:
                    p.Y--;
                    break;
            }

            return p;
        }

        public static int GetIndex(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => 0,
                Direction.Down => 1,
                Direction.Left => 2,
                Direction.Right => 3,
                _ => throw new ArgumentException("Invalid direction.", nameof(direction))
            };
        }
    }
}
