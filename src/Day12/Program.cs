using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Business.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Day12
{
    public class Program
    {
        private const string InputFileName = "Day12-InputData.txt";

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Utils.GetInputFilePath(), InputFileName);

            using (var sr = new StreamReader(filePath))
            {
                var jsonSerialised = sr.ReadToEnd();
                //dynamic data = JsonConvert.DeserializeObject(jsonSerialised);

                var data = JObject.Parse(jsonSerialised);

                var tokens = data.SelectTokens("$..*");
                var values = tokens.Select(x => ((JValue) x).Value);

                var numbers = FindAllNumbers(data);
            }

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }

        private static List<int> FindAllNumbers(dynamic data)
        {
            return new List<int>();
        }
    }
}
