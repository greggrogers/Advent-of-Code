using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Day4
{
    public class Program
    {
        public const string InputValue = "iwrupvqb";

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var hasher = MD5.Create();

            long partOne = 0;
            long partTwo = 0;

            long i = 1;
            while (true)
            {
                var md5Hash = Business.Helper.Utils.Md5Hash(string.Concat(InputValue, i), hasher);
                if (md5Hash.Substring(0, 5) == "00000" && partOne == 0)
                {
                    partOne = i;
                }
                if (md5Hash.Substring(0, 6) == "000000" && partTwo == 0)
                {
                    partTwo = i;
                    break;
                }
                i++;
            }

            Console.WriteLine(string.Concat("Part 1 - Lowest number to produce md5 hash with 5 leading zeros: ", partOne));
            Console.WriteLine(string.Concat("Part 2 - Lowest number to produce md5 hash with 6 leading zeros: ", partTwo));

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }
    }
}
