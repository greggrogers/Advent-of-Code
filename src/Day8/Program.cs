using Business.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Day8
{
    public class Program
    {
        private const string InputFileName = "Day8-InputData.txt";

        private static long TotalCharsCodeLengthP1 { get; set; }
        private static long TotalCharsMemoryLengthP1 { get; set; }
        private static long TotalCharsCodeLengthP2 { get; set; }
        private static long TotalCharsMemoryLengthP2 { get; set; }

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Business.Helper.Utils.GetInputFilePath(), InputFileName);
            
            using (var sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    DetermineCharacterCount(line.Trim(), ProcessingLogic.PartOne);
                    DetermineCharacterCount(line.Trim(), ProcessingLogic.PartTwo);
                }
            }

            Console.WriteLine(string.Concat("Part 1 - The number of characters of code for string literals minus the number of characters in memory for the values of the strings: ", (TotalCharsCodeLengthP1 - TotalCharsMemoryLengthP1)));
            Console.WriteLine(string.Concat("Part 2 - The total number of characters to represent the newly encoded strings minus the number of characters of code in each original string literal: ", (TotalCharsMemoryLengthP2 - TotalCharsCodeLengthP2)));

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        private static void DetermineCharacterCount(string line, ProcessingLogic logic)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            var charArray = line.ToCharArray();

            switch (logic)
            {
                case ProcessingLogic.PartOne:
                    TotalCharsCodeLengthP1 += line.Length;

                    var charsInMemory = 0;

                    for (int i = 0; i < line.Length; i++)
                    {
                        if (charArray[i] == '"') continue;
                        if (charArray[i] == '\\')
                        {
                            if (charArray[i + 1] == '\\')
                            {
                                charsInMemory++;
                                i++;
                                continue;
                            }
                            if (charArray[i + 1] == '"')
                            {
                                charsInMemory++;
                                i++;
                                continue;
                            }
                            if (charArray[i + 1] == 'x')
                            {
                                charsInMemory++;
                                i = i + 3;
                            }
                        }
                        else
                        {
                            charsInMemory++;    
                        }
                    }

                    TotalCharsMemoryLengthP1 += charsInMemory;
                    break;

                case ProcessingLogic.PartTwo:
                    TotalCharsCodeLengthP2 += line.Length;

                    var sb = new StringBuilder();
                    sb.Append('"');

                    for (int i = 0; i < charArray.Length; i++)
                    {
                        if (charArray[i] == '"') sb.Append("\\\"");
                        else if (charArray[i] == '\\') sb.Append("\\\\");
                        else
                        {
                            sb.Append(charArray[i]);
                        }
                    }

                    sb.Append('"');

                    TotalCharsMemoryLengthP2 += sb.ToString().Length;
                    break;
            }
        }
    }
}
