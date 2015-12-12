using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Day9
{
    public class JourneyLeg
    {
        public string From { get; set; }
        public string To { get; set; }
        public int Distance { get; set; }
    }

    public class Route
    {
        public ArrayList Destinations { get; set; }
        public int TotalDistance
        {
            get { return 0; }
        }
    }

    public class Program
    {
        private const string InputFileName = "Day9-InputData.txt";

        private static IList<JourneyLeg> Legs { get; set; } 
        private static IList<Route> Routes { get; set; }

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Business.Helper.Utils.GetInputFilePath(), InputFileName);

            Legs = new List<JourneyLeg>();

            using (var sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var leg = DetermineJourneyLeg(line);
                    if (leg != null) Legs.Add(leg);
                }
            }

            Routes = PermutateRouteDestinations(GetUniqueDestinations());

            //Console.WriteLine(string.Concat("Part 1 - The number of characters of code for string literals minus the number of characters in memory for the values of the strings: ", (TotalCharsMemoryLength - TotalCharsCodeLength)));

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        private static JourneyLeg DetermineJourneyLeg(string line)
        {
            var parameters = line.Split(new[] {"to", "="}, StringSplitOptions.None);

            if (parameters.Length != 3) return null;
            if (string.IsNullOrWhiteSpace(parameters[0])) return null;
            if (string.IsNullOrWhiteSpace(parameters[1])) return null;
            if (string.IsNullOrWhiteSpace(parameters[2])) return null;

            var leg = new JourneyLeg {From = parameters[0].Trim(), To = parameters[1].Trim()};

            int distance;
            if (!Int32.TryParse(parameters[2].Trim(), out distance)) return null;

            leg.Distance = distance;

            return leg;
        }

        private static IList<string> GetUniqueDestinations()
        {
            var uniqueDestinations = new List<string>();

            foreach (var journeyLeg in Legs)
            {
                if (!uniqueDestinations.Contains(journeyLeg.From)) uniqueDestinations.Add(journeyLeg.From);
                if (!uniqueDestinations.Contains(journeyLeg.To)) uniqueDestinations.Add(journeyLeg.To);
            }

            return uniqueDestinations;
        }

        private static IList<Route> PermutateRouteDestinations(IList<string> destinations)
        {
            var routes = new List<Route>();



            return routes;
        }
    }
}