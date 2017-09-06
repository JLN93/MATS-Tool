using System.Collections.Generic;
using System.Drawing;

namespace MATS
{
    public class TraceManager
    {
        private List<SimulationTrace> Simulations;

        /// <summary>
        /// Creates an empty TraceManager.
        /// </summary>
        public TraceManager()
        {
            Simulations = new List<SimulationTrace>();
        }
        /// <summary>
        /// Creates a TraceManager with the given trace.
        /// </summary>
        /// <param name="trace">The Trace to add.</param>
        /// <param name="name">Name of the parameter.</param>
        public TraceManager(string trace, string name)
        {
            Simulations = new List<SimulationTrace>();
            List<PointF> parsedTrace = new List<PointF>();
            Simulations.Add(new SimulationTrace(name, parsedTrace));
        }
        /// <summary>
        /// Adds a simulation trace the the TraceManager.
        /// </summary>
        /// <param name="st">The simulation trace to add.</param>
        public void AddSimulation(SimulationTrace st)
        {
            Simulations.Add(st);
        }
        /// <summary>
        /// Prints all traces in the trace manager.
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
        /// Prints all interpolated traces in the trace manager.
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
        /// Interpolates all traces in the tracemanager.
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
        /// Interpolates all traces in the tracemanager. Also adds an offset to get cleaner outputs
        /// </summary>
        /// <param name="stepSize"></param>
        /// <param name="simTime"></param>
        /// <param name="offset">stepSize/2 is recommended.</param>
        public void Interpolate(decimal stepSize, decimal simTime, decimal offset)
        {
            foreach (SimulationTrace item in Simulations)
            {
                item.interpolateTraces(stepSize, simTime, offset);
            }
        }
        /// <summary>
        /// Returns the trace by name.
        /// </summary>
        /// <param name="name">Name of the parameter to return.</param>
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
        /// Returns the trace by index.
        /// </summary>
        /// <param name="index">Index of the trace to return</param>
        /// <returns></returns>
        public SimulationTrace GetTrace(int index)
        {
            return Simulations[index];
        }
        /// <summary>
        /// Extracts the decimal input values from the trace.
        /// </summary>
        /// <param name="inputName">Name of the input parameter.</param>
        /// <param name="simNR">The simulation to retrive.</param>
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
        /// Extracts the integer input values from the trace.
        /// </summary>
        /// <param name="inputName">Name of the input parameter.</param>
        /// <param name="simNR">The simulation to retrive.</param>
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
