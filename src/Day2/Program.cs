using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day2
{
    public class Program
    {
        private const string InputFileName = "Day2-InputData.txt";

        public static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var filePath = string.Concat(Business.Helper.Utils.GetInputFilePath(), InputFileName);
            var totalSurfaceArea = 0;
            var totalRibbonLength = 0;

            using (var sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var values = line.Split('x');
                    short length = 0, width = 0, height = 0;

                    for (var i = 0; i < values.Length; i++)
                    {
                        short convertedValue;
                        if (short.TryParse(values[i], out convertedValue))
                        {
                            switch (i)
                            {
                                case 0:
                                    length = convertedValue;
                                    break;
                                case 1:
                                    width = convertedValue;
                                    break;
                                case 2:
                                    height = convertedValue;
                                    break;
                            }
                        }
                    }

                    var calcValues = new int[3];
                    calcValues[0] = (2*length*width);
                    calcValues[1] = (2*width*height);
                    calcValues[2] = (2*height*length);

                    var surfaceArea = calcValues[0] + calcValues[1] + calcValues[2];
                    var extraArea = calcValues.Min() / 2;

                    totalSurfaceArea = totalSurfaceArea + (surfaceArea + extraArea);

                    var ribbonForBow = (length*width*height);
                    var ribbonValues = new int[3];
                    ribbonValues[0] = (length);
                    ribbonValues[1] = (width);
                    ribbonValues[2] = (height);

                    Array.Sort(ribbonValues);
                    var ribbonForWrap = (ribbonValues[0] + ribbonValues[0] + ribbonValues[1] + ribbonValues[1]);
                    totalRibbonLength = totalRibbonLength + (ribbonForBow + ribbonForWrap);
                }
            }

            Console.WriteLine(string.Concat("Part 1 - Total square feet of wrapping paper needed: ", totalSurfaceArea));
            Console.WriteLine(string.Concat("Part 2 - Total ribbon length needed: ", totalRibbonLength));

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            Console.WriteLine("Overall run time was: " + elapsedTime);
        }
    }
}
