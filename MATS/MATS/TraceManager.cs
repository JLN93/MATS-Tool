using System.Collections.Generic;
using System.Drawing;

namespace MATS
{
    public class TraceManager
    {
        private List<SimulationTrace> Simulations;

        /// <summary>
        /// 
        /// </summary>
        public TraceManager()
        {
            Simulations = new List<SimulationTrace>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trace"></param>
        /// <param name="name"></param>
        public TraceManager(string trace, string name)
        {
            /*
            parse trace
            */
            Simulations = new List<SimulationTrace>();
            List<PointF> parsedTrace = new List<PointF>();
            Simulations.Add(new SimulationTrace(name, parsedTrace));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        public void AddSimulation(SimulationTrace st)
        {
            Simulations.Add(st);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string PrintSimulations()
        {
            string output = "";
            foreach (SimulationTrace item in Simulations)
            {
                output += item.PrintSimulation() + "\r\n";
            }
            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string PrintInterpolatedSimulations()
        {
            string output = "";
            foreach (SimulationTrace item in Simulations)
            {
                output += item.PrintInterpolatedSimulation() + "\r\n\r\n";
            }
            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string PrintInterpolatedSimulationsCSV()
        {
            string output = "";
            foreach (SimulationTrace item in Simulations)
            {
                output += item.PrintInterpolatedSimulation() + "\r\n\r\n";
            }
            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepSize"></param>
        /// <param name="simTime"></param>
        public void Interpolate(decimal stepSize, decimal simTime)
        {
            foreach (SimulationTrace item in Simulations)
            {
                item.interpolateTraces(stepSize, simTime, 0);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepSize"></param>
        /// <param name="simTime"></param>
        /// <param name="offset"></param>
        public void Interpolate(decimal stepSize, decimal simTime, decimal offset)
        {
            foreach (SimulationTrace item in Simulations)
            {
                item.interpolateTraces(stepSize, simTime, offset);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SimulationTrace GetTrace(string name)
        {
            foreach (SimulationTrace item in Simulations)
            {
                if (item.valueName.Equals(name))
                {
                    return item;
                }
            }
            return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SimulationTrace GetTrace(int index)
        {
            return Simulations[index];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="simNR"></param>
        /// <returns></returns>
        public decimal[] ExtractInputs(string inputName, int simNR)
        {
            List<PointF> simRun;
            List<decimal> inputs = new List<decimal>();
            SimulationTrace trace = GetTrace(inputName);
            bool first = true;
            if (trace != null)
            {
                simRun = trace.GetInterpolatedTrace(simNR);

                foreach (PointF item in simRun)
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }
                    inputs.Add((decimal)item.Y);
                }
                return inputs.ToArray();
            }
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="simNR"></param>
        /// <returns></returns>
        public List<int> ExtractIntInputs(string inputName, int simNR)
        {
            List<PointF> simRun;
            List<int> inputs = new List<int>();
            SimulationTrace trace = GetTrace(inputName);
            if (trace != null)
            {
                simRun = trace.GetInterpolatedTrace(simNR);

                foreach (PointF item in simRun)
                {
                    inputs.Add((int)item.Y);
                }
                return inputs;
            }
            else
                return null;
        }
    }
}
