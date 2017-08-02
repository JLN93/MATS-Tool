using System.Collections.Generic;
using System.Drawing;

namespace MATS
{
    public class TraceManager
    {
        private List<SimulationTrace> Simulations;

        public TraceManager()
        {
            Simulations = new List<SimulationTrace>();
        }

        public TraceManager(string trace, string name)
        {
            /*
            parse trace
            */
            Simulations = new List<SimulationTrace>();
            List<PointF> parsedTrace = new List<PointF>();
            Simulations.Add(new SimulationTrace(name, parsedTrace));
        }
        public void AddSimulation(SimulationTrace st)
        {
            Simulations.Add(st);
        }

        public string PrintSimulations()
        {
            string output = "";
            foreach (SimulationTrace item in Simulations)
            {
                output += item.PrintSimulation() + "\r\n";
            }
            return output;
        }
        public string PrintInterpolatedSimulations()
        {
            string output = "";
            foreach (SimulationTrace item in Simulations)
            {
                output += item.PrintInterpolatedSimulation() + "\r\n\r\n";
            }
            return output;
        }
        public string PrintInterpolatedSimulationsCSV()
        {
            string output = "";
            foreach (SimulationTrace item in Simulations)
            {
                output += item.PrintInterpolatedSimulation() + "\r\n\r\n";
            }
            return output;
        }
        public void Interpolate(decimal stepSize, decimal simTime)
        {
            foreach (SimulationTrace item in Simulations)
            {
                item.interpolateTraces(stepSize, simTime);
            }
        }
        public void Interpolate(decimal stepSize, decimal simTime, decimal offset)
        {
            foreach (SimulationTrace item in Simulations)
            {
                item.interpolateTraces(stepSize, simTime, offset);
            }
        }
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
        public SimulationTrace GetTrace(int index)
        {
            return Simulations[index];
        }
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
