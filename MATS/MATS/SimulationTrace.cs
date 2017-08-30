using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MATS
{
    public class SimulationTrace
    {
        /// <summary>
        /// Used to store the original trace. First list represents the simulations and second list is the pointlist of the tracepoints.
        /// </summary>
        private List<List<PointF>> traces;
        /// <summary>
        /// Used to store the interpolated trace. First list represents the simulations and second list is the pointlist of the tracepoints.
        /// </summary>
        private List<List<PointF>> interpolatedTraces;
        /// <summary>
        /// Returns or sets the parameter name.
        /// </summary>
        public string valueName { get; private set; }

        /// <summary>
        /// Creates an empty trace with the name of paramName.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        public SimulationTrace(string paramName)
        {
            traces = new List<List<PointF>>();
            valueName = paramName;
        }
        /// <summary>
        /// Creates a filled trace.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        /// <param name="trace">The Simulation trace.</param>
        public SimulationTrace(string paramName, List<PointF> trace)
        {
            traces = new List<List<PointF>>();
            valueName = paramName;
            traces.Add(trace);
        }
        /// <summary>
        /// Returns a trace.
        /// </summary>
        /// <param name="simulationRun">The simulation to return, 0 indexed.</param>
        /// <returns></returns>
        public List<PointF> GetTrace(int simulationRun)
        {
            return traces[simulationRun];
        }

        /// <summary>
        /// Returns an interpolated trace.
        /// </summary>
        /// <param name="simulationRun">The simulation to return, 0 indexed.</param>
        /// <returns></returns>
        public List<PointF> GetInterpolatedTrace(int simulationRun)
        {
            if (interpolatedTraces != null)
                return interpolatedTraces[simulationRun];
            else
                return null;
        }

        /// <summary>
        /// Returns the number of points in the trace. 
        /// </summary>
        /// <param name="index">0-based index</param>
        /// <returns></returns>
        public int GetTraceLength(int index)
        {
            return ((getSimCount() - 1) < index) ? -1 : traces[index].Count();
        }
        /// <summary>
        /// Returns the number of points in the interpolated trace. 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetInterpolatedTraceLength(int index)
        {
            return ((getInterpolatedSimCount() - 1) < index) ? -1 : interpolatedTraces[index].Count();
        }
        /// <summary>
        /// Adds a new simulation trace to the list.
        /// </summary>
        /// <param name="newtrace">The trace to add.</param>
        public void AddTrace(List<PointF> newtrace)
        {
            traces.Add(newtrace);
        }
        /// <summary>
        /// Returns the number of simulations in the trace list.
        /// </summary>
        /// <returns></returns>
        public int getSimCount()
        {
            return traces.Count();
        }
        /// <summary>
        /// Returns the number of interpolated simulations in the trace list.
        /// </summary>
        /// <returns></returns>
        public int getInterpolatedSimCount()
        {
            return traces.Count();
        }
        /// <summary>
        /// Prints a simulation.
        /// </summary>
        /// <param name="i"></param>
        /// <returns>Formated output.</returns>
        public string PrintSimulation(int i)
        {
            string output = valueName + "\r\nSimulation " + (i + 1) + ":\r\n";
            foreach (PointF item in traces[i])
            {
                output += "(" + item.X + "," + item.Y + ") ";
            }
            return output;
        }
        /// <summary>
        /// Prints all simuations.
        /// </summary>
        /// <returns></returns>
        public string PrintSimulation()
        {
            StringBuilder sb = new StringBuilder();
            // Starts string with "'Parameter name':"
            sb.Append(valueName + ": ");

            for (int i = 0; i < getSimCount(); i++)
            {
                // Adds the header for the simulation
                sb.Append("\r\nSimulation " + (i + 1) + ":\r\n");
                foreach (PointF item in traces[i])
                {
                    // Adds each point in simulation
                    sb.Append("(" + item.X + "," + item.Y + ") ");
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Prints all interpolated simulation.
        /// </summary>
        /// <returns></returns>
        public string PrintInterpolatedSimulation()
        {
            StringBuilder sb = new StringBuilder();
            // Starts string with "'Parameter name':"
            sb.Append(valueName + ": ");

            for (int i = 0; i < getInterpolatedSimCount(); i++)
            {
                // Adds the header for the simulation
                sb.Append("\r\nSimulation " + (i + 1) + ":\r\n");
                foreach (PointF item in interpolatedTraces[i])
                {
                    // Adds each point in simulation
                    sb.Append("(" + item.X + "," + item.Y + ") ");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Interpolate all simulations and stores them in "interpolatedTraces". Offset can be used to get clean values.
        /// </summary>
        /// <param name="stepSize">Size of each step in time (s).</param>
        /// <param name="simTime">Total time of the simulation (s).</param>
        /// <param name="offset">Recommended value is stepSize/2, set to 0 for no effect.</param>
        public void interpolateTraces(decimal stepSize, decimal simTime, decimal offset)
        {
            interpolatedTraces = new List<List<PointF>>();
            // Loop through "simulations"
            for (int i = 0; i < traces.Count; i++)
            {
                interpolatedTraces.Add(new List<PointF>());
                decimal stepCount = offset;
                // Loop through points and get 2 points
                for (int j = 1; j < traces[i].Count; j++)
                {
                    PointF fst = traces[i][j - 1];
                    PointF snd = traces[i][j];
                    // Loop until stepcounter is bigger than the end point 
                    while (stepCount < (decimal)snd.X && stepCount <= simTime)
                    {
                        interpolatedTraces[i].Add(vectorInterpolation(fst, snd, stepCount));
                        stepCount += stepSize;
                    }
                }
            }
        }
        /// <summary>
        /// Linear interpolate the values between 2 points.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="pointSize">The interval between each point.</param>
        /// <returns></returns>
        private PointF vectorInterpolation(PointF startPoint, PointF endPoint, decimal pointSize)
        {
            // Get the deltas of x and y
            decimal diff_X = (decimal)(endPoint.X - startPoint.X);
            decimal diff_Y = (decimal)(endPoint.Y - startPoint.Y);
            // Divide by zero check
            if (diff_X == 0)
            {
                System.Console.WriteLine("X: " + pointSize + " Y: " + endPoint.Y);
                return new PointF((float)pointSize, startPoint.Y);
            }
            // calculate the gradient
            decimal gradient = (diff_Y) / (diff_X);
            // Applies the interpolation
            decimal y = ((gradient * pointSize - gradient * (decimal)startPoint.X) + (decimal)startPoint.Y);

            return new PointF((float)pointSize, (float)y);
        }
    }
}
