using System;
using System.Diagnostics;

namespace Day11
{
    public class Program
    {
        public const string InputValue = "vzbxkghb";

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

            for (int i = 0; i < (textChars.Length - 2); i++)
            {
                var threeChars = new string(new[] { textChars[i], textChars[i+1], textChars[i+2] });

                var charOne = textChars[i];
                var two = ((textChars[i]) == 'z') ? 'a' : (char)(textChars[i] + 1);
                var three = ((textChars[i]) == 'y') ? 'a' : (char)(textChars[i] + 2);
            }

            return false;
        }
    }
}
