using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Day3
{
    public enum Movement
    {
        Up,
        Down,
        Left,
        Right,
        Unknown
    }

    public class Program
    {
        private const string InputFileName = "Day3-InputData.txt";

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Business.Helper.Utils.GetInputFilePath(), InputFileName);

            var positionsP1 = new Dictionary<Point, int>();
            var positionsP2 = new Dictionary<Point, int>();

            var totalSantaHousesVisitedP1 = 1;

            var totalSantaHousesVisitedP2 = 1;
            var totalRobotHousesVisitedP2 = 0;

            using (var sr = new StreamReader(filePath))
            {
                var lastSantaPositionP1 = new Point(0,0);
                var lastSantaPositionP2 = new Point(0,0);
                var lastRobotPositionP2 = new Point(0,0);

                positionsP1.Add(lastSantaPositionP1, 0);
                positionsP2.Add(lastSantaPositionP2, 0);

                var charPos = 0;
                do
                {
                    var movement = DetermineMovement((char)sr.Read());
                    charPos++;
                    if (movement == Movement.Unknown) continue;

                    //Part one
                    lastSantaPositionP1 = ChangePosition(movement, lastSantaPositionP1);

                    if (!positionsP1.ContainsKey(lastSantaPositionP1))
                    {
                        totalSantaHousesVisitedP1++;
                        positionsP1.Add(lastSantaPositionP1, 0);
                    }

                    //Part Two
                    if (charPos % 2 == 0)
                    {
                        //Robot
                        lastRobotPositionP2 = ChangePosition(movement, lastRobotPositionP2);

                        if (!positionsP2.ContainsKey(lastRobotPositionP2))
                        {
                            totalRobotHousesVisitedP2++;
                            positionsP2.Add(lastRobotPositionP2, 0);
                        }
                    }
                    else
                    {
                        //Santa
                        lastSantaPositionP2 = ChangePosition(movement, lastSantaPositionP2);

                        if (!positionsP2.ContainsKey(lastSantaPositionP2))
                        {
                            totalSantaHousesVisitedP2++;
                            positionsP2.Add(lastSantaPositionP2, 0);
                        }
                    }
                } while (!sr.EndOfStream);
            }

            Console.WriteLine(string.Concat("Part 1 - Houses visited with at least one present: ", totalSantaHousesVisitedP1));
            Console.WriteLine(string.Concat("Part 2 - Houses visited with at least one present: ", totalSantaHousesVisitedP2 + totalRobotHousesVisitedP2));

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        private static Point ChangePosition(Movement movement, Point lastPoint)
        {
            switch (movement)
            {
                case Movement.Up:
                    return new Point(lastPoint.X, lastPoint.Y + 1);
                case Movement.Down:
                    return new Point(lastPoint.X, lastPoint.Y - 1);
                case Movement.Left:
                    return new Point(lastPoint.X - 1, lastPoint.Y);
                case Movement.Right:
                    return new Point(lastPoint.X + 1, lastPoint.Y);
                default:
                    return new Point(0,0);
            }
        }

        private static Movement DetermineMovement(char character)
        {
            switch (character)
            {
                case '^':
                    return Movement.Up;
                case 'v':
                    return Movement.Down;
                case '<':
                    return Movement.Left;
                case '>':
                    return Movement.Right;
                default:
                    return Movement.Unknown;
            }
        }
    }
}
