using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MATS
{
    public class SimulationTrace
    {
        private int simulations;
        private List<List<PointF>> traces;
        private List<List<PointF>> interpolatedTraces;

        public string valueName { get; private set; }

        public SimulationTrace(string paramName)
        {
            traces = new List<List<PointF>>();
            valueName = paramName;
        }
        public SimulationTrace(string paramName, List<PointF> trace)
        {
            traces = new List<List<PointF>>();
            valueName = paramName;
            traces.Add(trace);
        }
        public List<PointF> GetTrace(int simulationRun)
        {
            return traces[simulationRun];
        }
        // 0-based index
        public List<PointF> GetInterpolatedTrace(int simulationRun)
        {
            if (interpolatedTraces != null)
                return interpolatedTraces[simulationRun];
            else
            return null;
            
        }
        // 0-based index
        public int GetTraceLength(int index)
        {
            return ((getSimCount()-1) < index) ? -1 : traces[index].Count();
        }
        public int GetInterpolatedTraceLength(int index)
        {
            return ((getInterpolatedSimCount() - 1) < index) ? -1 : interpolatedTraces[index].Count();
        }
        public void AddTrace(List<PointF> newtrace)
        {
            traces.Add(newtrace);
            simulations++;
        }
        public int getSimCount()
        {
            return traces.Count();
        }
        public int getInterpolatedSimCount()
        {
            return traces.Count();
        }
        public string PrintSimulation(int i)
        {
            string output = valueName + "\r\nSimulation " + (i + 1) + ":\r\n";
            foreach (PointF item in traces[i])
            {
                output += "(" + item.X + "," + item.Y + ") ";
            }
            return output;
        }

        public string PrintSimulation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(valueName + ": ");

            for (int i = 0; i < getSimCount(); i++)
            {
                sb.Append("\r\nSimulation " + (i + 1) + ":\r\n");
                foreach (PointF item in traces[i])
                {
                    sb.Append("(" + item.X + "," + item.Y + ") ");
                }
            }
            return sb.ToString();
        }

        public string PrintInterpolatedSimulation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(valueName + ": ");

            for (int i = 0; i < getInterpolatedSimCount(); i++)
            {
                sb.Append("\r\nSimulation " + (i + 1) + ":\r\n");
                foreach (PointF item in interpolatedTraces[i])
                {
                    sb.Append("(" + item.X + "," + item.Y + ") ");
                }
            }
            return sb.ToString();
        }
        public string OLDPrintInterpolatedSimulation()
        {
            string output = valueName + ": ";

            for (int i = 0; i < getInterpolatedSimCount(); i++)
            {
                output += "\r\nSimulation " + (i + 1) + ":\r\n";
                foreach (PointF item in interpolatedTraces[i])
                {
                    output += "(" + item.X + "," + item.Y + ") ";
                }
            }
            return output;
        }

        //interpolate all simulations
        public void interpolateTraces(decimal stepSize, decimal simTime)
        {
            interpolatedTraces = new List<List<PointF>>();
            //loop through "simulations"
            for (int i = 0; i < traces.Count; i++)
            {
                interpolatedTraces.Add(new List<PointF>());
                //List<PointF> temp = new List<PointF>();
                decimal stepCount = 0;
                //loop through points and get 2 points
                for (int j = 1; j < traces[i].Count; j++)
                {
                    PointF fst = traces[i][j - 1];
                    PointF snd = traces[i][j];
                    //loop until stepcounter is bigger than the end point 
                    while (stepCount < (decimal)snd.X && stepCount <= simTime)
                    {
                        interpolatedTraces[i].Add(vectorInterpolation(fst, snd, stepCount));
                        stepCount += stepSize;
                    }
                }
                
            }
        }
        public void interpolateTraces(decimal stepSize, decimal simTime, decimal offset)
        {
            interpolatedTraces = new List<List<PointF>>();
            //loop through "simulations"
            for (int i = 0; i < traces.Count; i++)
            {
                interpolatedTraces.Add(new List<PointF>());
                //List<PointF> temp = new List<PointF>();
                decimal stepCount = offset;
                //loop through points and get 2 points
                for (int j = 1; j < traces[i].Count; j++)
                {
                    PointF fst = traces[i][j - 1];
                    PointF snd = traces[i][j];
                    //loop until stepcounter is bigger than the end point 
                    while (stepCount < (decimal)snd.X && stepCount <= simTime)
                    {
                        interpolatedTraces[i].Add(vectorInterpolation(fst, snd, stepCount));
                        stepCount += stepSize;
                    }
                }

            }
        }

        private PointF vectorInterpolation(PointF startPoint, PointF endPoint, decimal pointSize)
        {
            //get the deltas of x and y
            decimal diff_X = (decimal)(endPoint.X - startPoint.X);
            decimal diff_Y = (decimal)(endPoint.Y - startPoint.Y);
            //divide by zero check
            if (diff_X == 0)
            {
                MessageBox.Show("X: " + pointSize + " Y: " + endPoint.Y);
                return new PointF((float)pointSize, startPoint.Y);
            }
            //calculate the gradient
            decimal gradient = (diff_Y) / (diff_X);
            //applies the interpolation
            decimal y = ((gradient * pointSize - gradient * (decimal)startPoint.X) + (decimal)startPoint.Y);
           // MessageBox.Show("X: " + pointSize + " Y: " + y);
            return new PointF((float)pointSize, (float)y);
        }

    }
}
