using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MATS
{
    /// <summary>
    /// Handles the connection to UPPAALS verifyta.
    /// </summary>
    public static class UPPAALSMCHandler
    {
        /// <summary>
        /// Builds a Verifyta query with the provided details.
        /// </summary>
        /// <param name="simRuns"></param>
        /// <param name="simTime"></param>
        /// <param name="inputs"></param>
        /// <param name="output"></param>
        /// <returns>The query string.</returns>
        public static string BuildQuery(int simRuns, decimal simTime, string[] inputs, string output)
        {
            string query;
            string processedInput = string.Empty;
            if (inputs != null)
            {
                StringBuilder sb = new StringBuilder(inputs[0]);
                for (int i = 1; i < inputs.GetLength(0); i++)
                {
                    sb.Append(", " + inputs[i]);
                }
                processedInput = sb.ToString();
            }

            if (processedInput.Equals(string.Empty) || output.Equals(string.Empty))
                query = "simulate " + simRuns + " [<=" + simTime + "] {" + processedInput + output + "}";
            else
                query = "simulate " + simRuns + " [<=" + simTime + "] {" + processedInput + ", " + output + "}";
            //Console.WriteLine(query);
            //File.WriteAllText(@"./tempquery.q", query);
            return query;
        }
        /// <summary>
        /// Simulates the provided model in UPPAAL and returns the result.
        /// </summary>
        /// <param name="filePath">Filepath to the model.</param>
        /// <param name="simruns">How many simulationruns to be parsed</param>
        /// <returns>A TraceManager containing the result from the simulations</returns>
        public static TraceManager RunUPPAAL(string filePath, int simruns) // Would be good to remove simruns.
        {
            TraceManager tm;
            string outputS;
            string outputE;

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @".\bin-win32\verifyta.exe",
                    Arguments = "-q -s \"" + filePath + "\" ./tempquery.q",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            outputS = proc.StandardOutput.ReadToEnd();
            outputE = proc.StandardError.ReadToEnd();
            proc.Dispose();

            if (!outputE.Equals(string.Empty))
            {
                throw new ArgumentException(outputE);
            }
            //debugging
            byte[] output = new UTF8Encoding(true).GetBytes(outputS);

            //Parse the UPPAAL output and put it into a Tracemanager
            //Parser parser = new Parser();
            tm = Parser.Parse(new MemoryStream(output), simruns);
            return tm;
        }
    }
}
