using DrainNet.Engine;
using System;
using System.Linq;
using TR20.DotNet;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dists = RainfallDistribution.LoadDistributions("TR20/distributions.txt");
            var duhs = UnitHydrograph.LoadFromFile("TR20/duh.txt");

            var input = new TR20Input();
            input.Area = 0.0015625;
            input.CN = 80;
            input.TC = 15;
            input.RainfallDepth = 5.6;
            input.RainfallDistribution = dists.First(x => x.Name == "NOAA Type C").Percent.ToArray();
            input.RainfallDistributionIncrement = 0.1;
            input.DimensionlessUnitHydrograph = duhs.First(x => x.Name == "Delmarva").DischargeRatio.ToArray();

            var engine = new TR20Engine("TR20");
            var output = engine.Run(input);

        }
    }
}
