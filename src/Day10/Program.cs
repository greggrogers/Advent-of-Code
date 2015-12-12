using System;
using System.Diagnostics;
using System.Text;

namespace Day10
{
    public class Program
    {
        public const string InputValue = "1321131112";

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var partOneSequence = string.Empty;
            var partTwoSequence = string.Empty;

            var sequenceString = InputValue;

            for (int i = 0; i < 50; i++)
            {
                sequenceString = GetNextSequence(sequenceString);
                if (i == 39) partOneSequence = sequenceString;
                if (i == 49) partTwoSequence = sequenceString;
            }

            Console.WriteLine(string.Concat("Part 1 - The length of the result: ", partOneSequence.Length));
            Console.WriteLine(string.Concat("Part 2 - The length of the result: ", partTwoSequence.Length));

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        private static string GetNextSequence(string currentSequence)
        {
            var charArray = currentSequence.ToCharArray();
            var newSequenceSb = new StringBuilder();

            for (int i = 0; i < charArray.Length; i++)
            {
                var numberOfDigits = 1;
                for (int c = i; c < charArray.Length; c++)
                {
                    if (c + 1 >= charArray.Length) break;
                    if (charArray[c] == charArray[c + 1]) numberOfDigits++;
                    else
                    {
                        i = c;
                        break;
                    }
                }
                newSequenceSb.Append(string.Concat(numberOfDigits, charArray[i]));
            }

            return newSequenceSb.ToString();
        }
    }
}
