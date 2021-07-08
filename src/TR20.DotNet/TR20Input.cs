using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TR20.DotNet
{
    public class TR20Input
    {
        /// <summary>
        /// Area in square miles
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Curve number
        /// </summary>
        public double CN { get; set; }

        /// <summary>
        /// Time of concentration in minutes
        /// </summary>
        public double TC { get; set; }

        /// <summary>
        /// Rainfall depth in inches
        /// </summary>
        public double RainfallDepth { get; set; }

        public string RainfallDistributionName { get; set; } = "DIST";
        public double[] RainfallDistribution { get; set; }
        public double RainfallDistributionIncrement { get; set; }


        public double[] DimensionlessUnitHydrograph { get; set; }

        public double MinimumHydrographValue { get; set; } = 0.0001;
        public double MinimumHydrographDisplayFlow { get; set; } = 0.001;
        public int HydrographPrintPrecision { get; set; } = 3;

        public string WriteToInputString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("WinTR-20: Version 1.10                  0         0         " + PadDoubleR(MinimumHydrographValue, 6));
            sb.AppendLine("[Project title]");
            sb.AppendLine("[Project subtitle]");
            sb.AppendLine("");
            sb.AppendLine("SUB-AREA:");
            sb.AppendLine("          A         Outlet              " + Area.ToString("0.00000") + "   " + CN.ToString("0") + ".       " + TC.ToString("0.00"));
            sb.AppendLine("");
            sb.AppendLine("STREAM REACH:");
            sb.AppendLine("");
            sb.AppendLine("STORM ANALYSIS:");
            sb.AppendLine("          Default                          " + RainfallDepth.ToString("0.00") + "      " + PadStringR(RainfallDistributionName, 10) + "2");
            sb.AppendLine("");
            sb.AppendLine("STRUCTURE RATING:");
            sb.AppendLine("");
            sb.AppendLine("RAINFALL DISTRIBUTION:");
            sb.AppendLine("          " + PadStringR(RainfallDistributionName, 10) + "          " + RainfallDistributionIncrement.ToString("0.00000") + "   ");
            WriteArray(sb, RainfallDistribution);
            sb.AppendLine("\r\n");
            sb.AppendLine("DIMENSIONLESS UNIT HYDROGRAPH:");
            WriteArray2(sb, DimensionlessUnitHydrograph);
            sb.AppendLine("\r\n");
            sb.AppendLine("GLOBAL OUTPUT:");
            sb.AppendLine("          " + PadIntR(HydrographPrintPrecision, 10) + PadDoubleR(MinimumHydrographDisplayFlow, 10) + "          YYYYN     YYYYNN");
            sb.AppendLine("");

            return sb.ToString();
        }

        private string PadIntR(int i, int count)
        {
            var s = i.ToString();
            return PadStringR(s, count);
        }

        private string PadDoubleR(double d, int count)
        {
            var s = d.ToString("0.##########");
            return PadStringR(s, count);
        }


        private string PadStringR(string s, int count)
        {
            return s + new string(' ', count - s.Length);
        }

        public void WriteToInputFile(string path)
        {
            using (var writer = new StreamWriter(path))
            {
                writer.Write(WriteToInputString());
            }
        }

        private void WriteArray(StringBuilder sb, double[] array)
        {
            int row = 1;
            int col = 1;

            foreach (double value in array)
            {
                if (col == 1 || col == 6)
                {
                    if (row > 1) sb.Append("          \r\n");
                    sb.Append("                    " + value.ToString("0.00000") + "   ");
                    col = 1;
                    row++;
                }
                else
                {
                    sb.Append(value.ToString("0.00000") + "   ");
                }
                col++;
            }
        }

        private void WriteArray2(StringBuilder sb, double[] array)
        {
            int row = 1;
            int col = 1;

            foreach (double value in array)
            {
                if (col == 1 || col == 6)
                {
                    if (row > 1) sb.Append("\r\n");
                    sb.Append("                    " + value.ToString("0.00000"));
                    col = 1;
                    row++;
                }
                else
                {
                    sb.Append("   " + value.ToString("0.00000"));
                }
                col++;
            }
        }
    }
}
