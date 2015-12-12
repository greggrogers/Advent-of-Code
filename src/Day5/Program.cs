using System;
using System.Diagnostics;
using System.IO;
using Business.Enums;

namespace Day5
{
    public class Program
    {
        private const string InputFileName = "Day5-InputData.txt";
        private static readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };
        private static readonly string[] RestrictedStrings = {"ab","cd","pq","xy"};

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Business.Helper.Utils.GetInputFilePath(), InputFileName);
            var niceWordsP1 = 0;
            var niceWordsP2 = 0;

            using (var sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (IsNiceWord(line, ProcessingLogic.PartOne)) niceWordsP1++;
                    if (IsNiceWord(line, ProcessingLogic.PartTwo)) niceWordsP2++;
                }
            }

            Console.WriteLine("Part 1 - The number of nice words was: {0}", niceWordsP1);
            Console.WriteLine("Part 2 - The number of nice words was: {0}", niceWordsP2);

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        private static bool IsNiceWord(string text, ProcessingLogic logic)
        {
            var textChars = text.ToCharArray();

            switch (logic)
            {
                case ProcessingLogic.PartOne:
                    //Must contain atleast 3 vowels
                    var numberOfVowels = 0;
                    for (int i = 0; i < textChars.Length; i++)
                    {
                        if (IsVowel(textChars[i])) numberOfVowels++;
                    }

                    if (numberOfVowels < 3) return false;

                    //Atleast one letter appearing twice in a row
                    var lettersAppearingTwice = 0;
                    for (int i = 0; i < textChars.Length; i++)
                    {
                        var substring = new string(new[] { textChars[i], textChars[i] });
                        if (text.Contains(substring)) lettersAppearingTwice++;
                    }

                    if (lettersAppearingTwice < 1) return false;

                    //Does not contain any of the restricted strings
                    for (int i = 0; i < RestrictedStrings.Length; i++)
                    {
                        if (text.Contains(RestrictedStrings[i])) return false;
                    }

                    //It's a nice word!
                    return true;

                case ProcessingLogic.PartTwo:
                    //Contains pair of any two letters that appears atleat twice
                    var numberOfPairs = 0;
                    for (int i = 0; i < textChars.Length; i++)
                    {
                        if ((i + 2) > textChars.Length) continue;
                        var substring = text.Substring(i, 2);

                        if (text.Split(new []{substring},StringSplitOptions.None).Length >= 3) numberOfPairs++;
                    }

                    if (numberOfPairs < 1) return false;

                    //Contains at least one letter which repeats with exactly one letter between them
                    var matchingLetters = 0;
                    for (int i = 0; i < textChars.Length; i++)
                    {
                        if ((i + 3) > textChars.Length) continue;
                        var charArray = text.Substring(i, 3).ToCharArray();

                        if (charArray[0] == charArray[2]) matchingLetters++;
                    }

                    if (matchingLetters < 1) return false;

                    //It's a nice word!
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsVowel(char value)
        {
            for (int i = 0; i < Vowels.Length; i++)
            {
                var vowel = Vowels[i];
                if (vowel == value) return true;
            }
            return false;
        }
    }
}
