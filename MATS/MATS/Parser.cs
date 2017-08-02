using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace MATS
{
    public static class Parser
    {

        //can add multithread
        public static TraceManager Parse(Stream stream, int runs)
        {
            TraceManager tm = new TraceManager();
            using (StreamReader reader = new StreamReader(stream))
            {
                //remove the first 3 lines from UPPAAL output
                string clense; //redundant
                for (int i = 0; i < 3; i++)
                {
                    clense = reader.ReadLine();
                }
                while (reader.EndOfStream != true)
                {
                    //extract parameter name
                    string parameter = reader.ReadLine();
                    parameter = parameter.Replace(":", string.Empty);
                    SimulationTrace st = new SimulationTrace(parameter);

                    for (int i = 0; i < runs; i++)
                    {
                        if (reader.EndOfStream == true)
                            break;

                        while (reader.Read() != '[') ;
                        int sim = reader.Read();
                        string temp = "";
                        while (reader.Read() != ':') ;
                        reader.Read();
                        //some alignment hack
                        temp = reader.ReadLine();

                        //add points into a simulationTrace
                        temp = TraceCleaner(temp);
                        st.AddTrace(String2PointF(temp));
                    }
                    //add simulationtrace to traceManager
                    tm.AddSimulation(st);
                }
            }
                return tm;
            }

        private static List<PointF> String2PointF(string s)
        {
            List<PointF> pF = new List<PointF>();

            string[] points = s.Split(' ');
            for (int i = 0; i < points.Length; i++)
            {
                string[] pointPair = points[i].Split(',');

                pF.Add(new PointF(float.Parse(pointPair[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(pointPair[1], CultureInfo.InvariantCulture.NumberFormat)));
            }
            return pF;
        }
        private static string TraceCleaner(string inputToClean)
        {

            inputToClean = inputToClean.Replace("\r\n", " ");
            inputToClean = inputToClean.Replace("(", string.Empty);
            inputToClean = inputToClean.Replace(")", string.Empty);

            return inputToClean;
        }
    }
}
