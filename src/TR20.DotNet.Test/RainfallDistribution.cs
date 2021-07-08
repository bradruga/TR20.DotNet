using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrainNet.Engine
{
    public class RainfallDistribution
    {
        public string Name { get; set; }

        public List<double> Time { get; set; }

        public List<double> Percent { get; set; }

        public RainfallDistribution(string name)
        {
            Name = name;

            Time = new List<double>();
            Percent = new List<double>();
        }

        public static List<RainfallDistribution> LoadDistributions(string path)
        {
            var rainfallDists = new List<RainfallDistribution>();

            using (StreamReader reader = new StreamReader(path))
            {
                var firstLine = true;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (!line.StartsWith("#"))
                    {

                        var values = line.Split('\t');

                        if (firstLine)
                        {


                            for (int i = 1; i < values.Count(); i++)
                            {
                                rainfallDists.Add(new RainfallDistribution(values[i]));
                            }

                            firstLine = false;
                        }
                        else
                        {
                            for (int i = 1; i < values.Count(); i++)
                            {
                                rainfallDists[i - 1].Time.Add(double.Parse(values[0]));
                                rainfallDists[i - 1].Percent.Add(double.Parse(values[i]));
                            }
                        }
                    }
                }
            }

            return rainfallDists;
        }
    }
}
