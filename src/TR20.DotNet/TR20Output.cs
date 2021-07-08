using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TR20.DotNet
{
    public class TR20Output
    {
        /// <summary>
        /// Start time in hours
        /// </summary>
        public double StartTime { get; set; } = -1;

        /// <summary>
        /// Time increment in hours
        /// </summary>
        public double TimeIncrement { get; set; }

        /// <summary>
        /// Flow in cfs
        /// </summary>
        public double[] Q { get; set; }


        public static TR20Output FromFile(string path)
        {
            var output = new TR20Output();

            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Equals("WinTR-20 Printed Page File      End of Input Data List       ")) break;
                }

                var flowrates = new List<double>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line.StartsWith("Area or"))
                    {
                        for (int i = 1; i <= 7; i++)
                            line = reader.ReadLine();

                        if (line.Equals(" ")) break;

                        var s = line.Substring(58, 6);
                        output.TimeIncrement = double.Parse(s);

                        line = reader.ReadLine();
                        line = reader.ReadLine();
                        line = reader.ReadLine();

                        while (line != "")
                        {
                            while (line.Contains("  ")) line = line.Trim().Replace("  ", " ");

                            var values = line.Split(' ');

                            if (output.StartTime == -1) output.StartTime = double.Parse(values[0]);

                            for (int i = 1; i < values.Count(); i++)
                            {
                                flowrates.Add(double.Parse(values[i]));
                            }

                            line = reader.ReadLine();

                            if (line.Equals(" "))
                            {
                                line = reader.ReadLine();

                                if (line.Equals(" ")) break;

                                for (int i = 1; i <= 9; i++)
                                    line = reader.ReadLine();
                            }

                        }

                        output.Q = flowrates.ToArray();
                        
                        break;
                    }
                }
            }

            return output;
        }
    }
}
