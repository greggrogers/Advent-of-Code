using Business.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day6
{
    public enum Switch
    {
        On,
        Off,
        Toggle,
        Unknown
    }

    public class Command
    {
        public Switch Type { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    public class Program
    {
        private const string InputFileName = "Day6-InputData.txt";

        private static IDictionary<Point, bool> GridP1 { get; set; }
        private static IDictionary<Point, int> GridP2 { get; set; }

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Business.Helper.Utils.GetInputFilePath(), InputFileName);

            GridP1 = new Dictionary<Point, bool>();
            GridP2 = new Dictionary<Point, int>();

            InitiateGridP1();
            InitiateGridP2();

            using (var sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var command = GetCommand(line);
                    if (command.Type == Switch.Unknown) continue;

                    ModifyLighting(command, ProcessingLogic.PartOne);
                    ModifyLighting(command, ProcessingLogic.PartTwo);
                }
            }

            Console.WriteLine(string.Concat("Part 1 - Number of lights lit: ", GridP1.Count(p => p.Value)));
            Console.WriteLine(string.Concat("Part 2 - Total brightness: ", GridP2.Sum(p => p.Value)));

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        private static void ModifyLighting(Command command, ProcessingLogic logic)
        {
            var bottomLeft = command.Start;
            var topRight = command.End;

            var numberOfRows = (topRight.Y - bottomLeft.Y) + 1;
            var numberOfColsPerRow = (topRight.X - bottomLeft.X) + 1;

            switch (logic)
            {
                case ProcessingLogic.PartOne:
                    if (!GridP1.ContainsKey(command.Start)) return;
                    if (!GridP1.ContainsKey(command.End)) return;

                    for (int i = 0; i < numberOfRows; i++)
                    {
                        for (int j = 0; j < numberOfColsPerRow; j++)
                        {
                            GridP1[new Point(bottomLeft.X + j, bottomLeft.Y + i)] = NewLightStatusP1(GridP1[new Point(bottomLeft.X + j, bottomLeft.Y + i)], command.Type);
                        }
                    }
                    break;

                case ProcessingLogic.PartTwo:
                    if (!GridP2.ContainsKey(command.Start)) return;
                    if (!GridP2.ContainsKey(command.End)) return;

                    for (int i = 0; i < numberOfRows; i++)
                    {
                        for (int j = 0; j < numberOfColsPerRow; j++)
                        {
                            GridP2[new Point(bottomLeft.X + j, bottomLeft.Y + i)] = NewLightStatusP2(GridP2[new Point(bottomLeft.X + j, bottomLeft.Y + i)], command.Type);
                        }
                    }
                    break;
            }
        }

        private static void InitiateGridP1()
        {
            GridP1.Add(new Point(0, 0), false);

            for (int i = 1; i < 1000; i++)
            {
                GridP1.Add(new Point(i, i), false);

                for (int x = 0; x < i; x++)
                {
                    GridP1.Add(new Point(i, x), false);
                }

                for (int y = 0; y < i; y++)
                {
                    GridP1.Add(new Point(y, i), false);
                }
            }
        }

        private static void InitiateGridP2()
        {
            GridP2.Add(new Point(0, 0), 0);

            for (int i = 1; i < 1000; i++)
            {
                GridP2.Add(new Point(i, i), 0);

                for (int x = 0; x < i; x++)
                {
                    GridP2.Add(new Point(i, x), 0);
                }

                for (int y = 0; y < i; y++)
                {
                    GridP2.Add(new Point(y, i), 0);
                }
            }
        }

        private static Command GetCommand(string text)
        {
            var command = new Command {Type = Switch.Unknown};
            var commandArray = text.Split(' ');

            string[] startPoints, endPoints;

            switch (commandArray[0])
            {
                case "toggle":
                    command.Type = Switch.Toggle;

                    startPoints = commandArray[1].Split(',');
                    command.Start = new Point(int.Parse(startPoints[0]), int.Parse(startPoints[1]));
                    endPoints = commandArray[3].Split(',');
                    command.End = new Point(int.Parse(endPoints[0]), int.Parse(endPoints[1]));
                    break;
                case "turn":
                    command.Type = (commandArray[1] == "on" ? Switch.On : Switch.Off);

                    startPoints = commandArray[2].Split(',');
                    command.Start = new Point(int.Parse(startPoints[0]), int.Parse(startPoints[1]));
                    endPoints = commandArray[4].Split(',');
                    command.End = new Point(int.Parse(endPoints[0]), int.Parse(endPoints[1]));
                    break;
            }

            return command;
        }

        private static bool NewLightStatusP1(bool current, Switch type)
        {
            switch (type)
            {
                case Switch.On:
                    return true;
                case Switch.Off:
                    return false;
                case Switch.Toggle:
                    return !current;
                default:
                    return current;
            }
        }

        private static int NewLightStatusP2(int current, Switch type)
        {
            switch (type)
            {
                case Switch.On:
                    return current + 1;
                case Switch.Off:
                    return (current == 0) ? 0 : current - 1;
                case Switch.Toggle:
                    return current + 2;
                default:
                    return current;
            }
        }
    }
}
