using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace MATS
{
    public struct ExtractedInputs
    {
        public string name;
        public decimal[] values;
        public ExtractedInputs(string name, decimal[] values)
        {
            this.name = name;
            this.values = values;
        }
    }
    public partial class Form1 : Form
    {
        public int simruns;
        private int simTime;
        private int tickSize;
        private decimal deltaValue;
        private decimal stepSize;
        private string[] inputs;
        private string outputQuery;
        private string modelFilePath;
        private string[] mutantsFilePath;

        public Form1()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            if (!File.Exists(@".\bin-win32\verifyta.exe"))
            {
                MessageBox.Show(
                    "Verifyta.exe could not be found! Please verify that it's located inside \"./bin-win32/\".\n" +
                    "UPPAAL SMC can be downloaded at http://www.uppaal.org/.",
                    "Verifyta.exe is missing!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                Environment.Exit(2);
            }
        }

        private void Period_Changed(object sender, EventArgs e)
        {
            simTimeNumeric.Value = periodCountNumeric.Value * periodLengthNumeric.Value + (periodLengthNumeric.Value - 1);
        }
        private void setInternalVar()
        {
            //get number of simulations to run
            simruns = (int)numericUpDownSimRuns.Value;
            simTime = (int)simTimeNumeric.Value;
            tickSize = (int)periodLengthNumeric.Value;
            stepSize = StepSizeSelector.Value;
            deltaValue = numericUpDownDelta.Value;
            inputs = InputQueryTxtBox.Lines;
            outputQuery = outputQueryTxtBox.Text;
        }

        private void runVerifyta_Click(object sender, EventArgs e)
        {
            var totalWatch = System.Diagnostics.Stopwatch.StartNew();
            if (string.IsNullOrEmpty(modelFilePath) || mutantsFilePath == null)
            {
                MessageBox.Show("Please select a model first.");
                return;
            }
            string tmpVerifytaName = verifyta_button.Text;
            verifyta_button.Text = "Please Wait";
            this.Refresh();
            //TraceManager object to hold each models trace
            TraceManager tmOriginal;
            //simulations->models
            List<List<TraceManager>> tmMutants = new List<List<TraceManager>>();
            TraceManager[][] tmMutantArray;

            setInternalVar();

            //Container for UPPAAL output
            string uppaalOutput = string.Empty;

            //builds query based on the inputs on the form
            UPPAALSMCHandler.BuildQuery(simruns, simTime, inputs, outputQuery);
            verifyta_button.Text = "Simulating Model";
            this.Refresh();

            //Runs UPPAAL Program, needs to be extended with model to simulate and error handling
            try
            {
                tmOriginal = UPPAALSMCHandler.RunUPPAAL(modelFilePath, simruns);
            }
            catch(ArgumentException outputE)
            {
                MessageBox.Show(outputE.Message);
                return;
            }

            //interpolate to get input values
            tmOriginal.Interpolate(tickSize, simTime, tickSize / 2);

            Mutator m = new Mutator();
            m.inputBuilder(inputs, tmOriginal, simruns);
            List<List<string>> mutatedFiles;

            //generate mutants and organize them in a 2d list with simulation->mutant
            try
            {
                mutatedFiles = m.MutateFiles(mutantsFilePath, simruns);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            UPPAALSMCHandler.BuildQuery(1, simTime, null, outputQuery);

            tmMutantArray = new TraceManager[mutatedFiles.Count][];
            verifyta_button.Text = "Simulating Mutants";
            this.Refresh();
            try
            {
                Parallel.For(0, mutatedFiles.Count, i =>
                {
                    tmMutantArray[i] = new TraceManager[mutatedFiles[i].Count];

                    for (int j = 0; j < mutatedFiles[i].Count; j++)
                    {
                        tmMutantArray[i][j] = UPPAALSMCHandler.RunUPPAAL(mutatedFiles[i][j], simruns);

                    }
                }
                );
            }
            catch (ArgumentException outputE)
            {
                MessageBox.Show(outputE.Message);
                return;
            }

            //compare mutants
            List<List<int>> resultTable = new List<List<int>>();
            tmOriginal.Interpolate(stepSize, simTime);
            for (int i = 0; i < mutatedFiles.Count; i++)
            {
                resultTable.Add(new List<int>());
                List<PointF> reference = tmOriginal.GetTrace(outputQuery).GetInterpolatedTrace(i);
                foreach (TraceManager items in tmMutantArray[i])
                {
                    items.Interpolate(stepSize, simTime);
                    List<PointF> mutant = items.GetTrace(0).GetInterpolatedTrace(0);
                    int result = CompareOutput(reference, mutant, (float)deltaValue);
                    if (result == -1)
                    {
                        Console.WriteLine("unequal length");
                    }
                    resultTable[i].Add(result);
                }
            }
            ResultForm RF = new ResultForm(resultTable, m.GetExtractedInputs());
            totalWatch.Stop();
            var TotalelapsedS = totalWatch.ElapsedMilliseconds / 1000;
            MessageBox.Show("Total Execution Time: " + TotalelapsedS.ToString() + "s");
            RF.ShowDialog();
            verifyta_button.Text = tmpVerifytaName;
        }

        public int CompareOutput(List<PointF> first, List<PointF> second, float delta)
        {
            if (first.Count != second.Count)
            {
                return -1;
            }
            int i = 0;
            while (i < first.Count)
            {
                float calc = Math.Abs(first[i].Y - second[i].Y);
                if (calc >= delta)
                {
                    return 1;
                }
                i++;
            }
            return 0;
        }

        private void selectModelButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectModelDialog = new OpenFileDialog();
            selectModelDialog.InitialDirectory = Environment.CurrentDirectory;
            selectModelDialog.RestoreDirectory = true;
            selectModelDialog.Filter = "UPPAAL Model|*.xml|All files|*.*";

            if (selectModelDialog.ShowDialog() == DialogResult.OK)
            {
                modelFilePath = selectModelDialog.FileName;
                int cutIndex = modelFilePath.LastIndexOf('\\') + 1;
                labelSelectedModel.Text = modelFilePath.Substring(cutIndex, modelFilePath.Length - cutIndex);
            }

        }

        private void select_MutantsButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectModelDialog = new OpenFileDialog();
            selectModelDialog.InitialDirectory = Environment.CurrentDirectory;
            selectModelDialog.RestoreDirectory = true;
            selectModelDialog.Filter = "UPPAAL Models|*.xml|All files|*.*";
            selectModelDialog.Multiselect = true;

            if (selectModelDialog.ShowDialog() == DialogResult.OK)
            {
                mutantsFilePath = selectModelDialog.FileNames;
                labelSelectedMutants.Text = mutantsFilePath.Length + " Mutants Selected";
            }
        }

    }
}
