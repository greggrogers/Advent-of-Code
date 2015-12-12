using System;
using System.Diagnostics;

namespace Day7
{
    public class Program
    {
        private const string InputFileName = "Day7-InputData.txt";

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Business.Helper.Utils.GetInputFilePath(), InputFileName);


            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }
    }
}
