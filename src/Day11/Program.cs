using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day11
{
    public class Program
    {
        private const string InputValue = "vzbxkghb";
        private static readonly char[] RestrictedChars = { 'i', 'o', 'l' };

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var newPassword = InputValue;

            while (true)
            {
                newPassword = GenerateNextPassword(newPassword);
                if (ValidPassword(newPassword)) break;
            }

            Console.WriteLine("Part 1 - His next password will be: {0}", newPassword);

            while (true)
            {
                newPassword = GenerateNextPassword(newPassword);
                if (ValidPassword(newPassword)) break;
            }

            Console.WriteLine("Part 2 - His next password will be: {0}", newPassword);

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        public static string GenerateNextPassword(string password)
        {
            var passwordChars = password.ToCharArray();

            for (int i = passwordChars.Length - 1; i > -1; i--)
            {
                var current = passwordChars[i];
                if (current == 'z')
                {
                    passwordChars[i] = 'a';
                }
                else
                {
                    passwordChars[i]++;
                    break;
                }
            }

            return new string(passwordChars);
        }

        public static bool ValidPassword(string password)
        {
            var textChars = password.ToCharArray();

            //One set of three letters straight
            var straightThreeLetters = false;
            for (int i = 0; i < (textChars.Length - 2); i++)
            {
                var threeChars = new string(new[] { textChars[i], textChars[i+1], textChars[i+2] });

                var charOne = textChars[i];
                var charTwo = ((charOne) == 'z') ? ' ' : (char)(charOne + 1);
                var charThree = ((charTwo) == 'z') ? ' ' : (char)(charTwo + 1);

                var comparison = new string(new []{charOne, charTwo, charThree});

                if (string.Equals(threeChars, comparison))
                {
                    straightThreeLetters = true;
                    break;
                }
            }

            if (!straightThreeLetters) return false;

            //No restricted characters
            for (int i = 0; i < textChars.Length; i++)
            {
                for (int j = 0; j < RestrictedChars.Length; j++)
                {
                    if (textChars[i] == RestrictedChars[j]) return false;
                }
            }

            //Atleast two unique pairs of letters
            var matchingPairs = new HashSet<string>();
            for (int i = 0; i < (textChars.Length - 1); i++)
            {
                var twoChars = new string(new[] {textChars[i], textChars[i + 1]});
                var comparison = new string(new[] {textChars[i], textChars[i]});
                if (string.Equals(twoChars, comparison))
                {
                    matchingPairs.Add(twoChars);
                }
            }

            if (matchingPairs.Count < 2) return false;

            return true;
        }
    }
}
