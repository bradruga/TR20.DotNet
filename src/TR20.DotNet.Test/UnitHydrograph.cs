using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrainNet.Engine
{
    public class UnitHydrograph
    {
        public class Point
        {
            public double TimeRatio { get; set; }
            public double DischargeRatio { get; set; }

            public Point(double t, double q)
            {
                TimeRatio = t; DischargeRatio = q;
            }
        }




        public string Name { get; set; }


        public List<Point> DataPoints { get; set; }

        public List<double> TimeRatio { get; set; }

        public List<double> DischargeRatio { get; set; }

        public UnitHydrograph()
        {
            TimeRatio = new List<double>();
            DischargeRatio = new List<double>();
        }

        public UnitHydrograph(string name)
        {
            Name = name;
            TimeRatio = new List<double>();
            DischargeRatio = new List<double>();
        }

        public UnitHydrograph Scale(double Tp, double Qp)
        {
            var unitHydrograph = new UnitHydrograph();

            foreach (var d in TimeRatio)
            {
                unitHydrograph.TimeRatio.Add(d * Tp);
            }

            foreach (var d in DischargeRatio)
            {
                unitHydrograph.DischargeRatio.Add(d * Qp);
            }

            return unitHydrograph;
        }


        public static List<UnitHydrograph> LoadFromFile(string path)
        {
            var unitHydrographs = new List<UnitHydrograph>();

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line.Equals("[START]"))
                    {
                        unitHydrographs.Add(new UnitHydrograph(reader.ReadLine()));
                    }
                    else if (line.Equals("[END]"))
                    {

                    }
                    else if (line.Contains('\t'))
                    {
                        var values = line.Split('\t');

                        unitHydrographs.Last().TimeRatio.Add(double.Parse(values[0]));
                        unitHydrographs.Last().DischargeRatio.Add(double.Parse(values[1]));
                    }
                }
            }

            return unitHydrographs;
        }
    }
}
