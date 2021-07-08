using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TR20.DotNet
{
    public class TR20Engine
    {
        private string executableDirectory = "";
        public TR20Engine()
        {
            CheckForExecutable();
        }

        public TR20Engine(string executableDirectory)
        {
            this.executableDirectory = executableDirectory;
            CheckForExecutable();
        }

        private void CheckForExecutable()
        {
            // Look for executable file
            if (!File.Exists(Path.Combine(executableDirectory, "tr20.exe")))
            {
                throw new Exception("TR20 executable not found");
            }
        }

        public TR20Output Run(TR20Input input)
        {
            // Get paths
            var pathExe = Path.Combine(executableDirectory, "tr20.exe");
            var pathInp = Path.Combine(executableDirectory, "tr20.inp");
            var pathOut = Path.Combine(executableDirectory, "tr20.out");
            var pathErr = Path.Combine(executableDirectory, "tr20.err");
            var pathDbg = Path.Combine(executableDirectory, "tr20.dbg");

            // Clear old files
            File.Delete(pathInp);
            File.Delete(pathOut);
            File.Delete(pathErr);
            File.Delete(pathDbg);

            // Write input to file
            input.WriteToInputFile(pathInp);

            // Run TR20
            var startInfo = new ProcessStartInfo(pathExe);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = true;
            var process = Process.Start(startInfo);
            process.WaitForExit();

            // Read error file
            var errContents = File.ReadAllText(pathErr).Trim();

            if (errContents != null && errContents.Length > 0)
            {
                throw new Exception(errContents);
            }

            // Read output file
            var output = TR20Output.FromFile(pathOut);

            return output;
        }

    }
}
