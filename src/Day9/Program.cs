using Business.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
        public IList<string> Destinations { get; set; }
        public int TotalDistance { get; set; }
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

            var filePath = string.Concat(Utils.GetInputFilePath(), InputFileName);

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
            Routes = Routes.OrderBy(r => r.TotalDistance).ToList();

            Console.WriteLine(string.Concat("Part 1 - What is the distance of the shortest route?: ", Routes[0].TotalDistance));

            Routes = Routes.OrderByDescending(r => r.TotalDistance).ToList();

            Console.WriteLine(string.Concat("Part 2 - What is the distance of the longest route?: ", Routes[0].TotalDistance));
            
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

            var permutationEngine = new Permutations<string>();
            foreach (var permutation in permutationEngine.GeneratePermutations(destinations))
            {
                var route = new Route();

                for (int i = 0; i < (permutation.Count - 1); i++)
                {
                    var from = permutation[i];
                    var to = permutation[i + 1];

                    var leg = Legs.FirstOrDefault(l => (l.From == from && l.To == to) || (l.From == to && l.To == from));
                    if (leg == null) continue;

                    route.TotalDistance += leg.Distance;
                }

                route.Destinations = permutation;
                routes.Add(route);
            }

            return routes;
        }
    }
}