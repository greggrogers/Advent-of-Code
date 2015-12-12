using System;
using System.Diagnostics;
using System.IO;

namespace Day1
{
    public enum Movement
    {
        Up,
        Down,
        Unknown
    }

    public class Program
    {
        private const string InputFileName = "Day1-InputData.txt";

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Business.Helper.Utils.GetInputFilePath(), InputFileName);
            short floorLevel = 0;
            short posTracker = 0;
            short posEnteredBasement = 0;

            using (var sr = new StreamReader(filePath))
            {
                do
                {
                    var movement = DetermineMovement((char) sr.Read());
                    if (movement == Movement.Unknown) continue;

                    posTracker++;
                    floorLevel = ChangeFloorLevel(movement, floorLevel);

                    if (floorLevel == -1 && posEnteredBasement == 0) posEnteredBasement = posTracker;

                } while (!sr.EndOfStream);
            }

            Console.WriteLine(string.Concat("Part 1 - Santa ended up on floor: ", floorLevel));
            Console.WriteLine(string.Concat("Part 2 - Santa entered the basement when at character position: ", posEnteredBasement));

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        private static short ChangeFloorLevel(Movement movement, short floorLevel)
        {
            switch (movement)
            {
                case Movement.Up:
                    return (short) (floorLevel + 1);
                case Movement.Down:
                    return (short)(floorLevel - 1);
                default:
                    return floorLevel;
            }
        }

        private static Movement DetermineMovement(char character)
        {
            switch (character)
            {
                case '(':
                    return Movement.Up;
                case ')':
                    return Movement.Down;
                default:
                    return Movement.Unknown;
            }
        }
    }
}
