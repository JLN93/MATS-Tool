using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MATS
{
    public partial class ResultForm : Form
    {
        List<List<int>> resultTable;
        List<List<ExtractedInputs>> extractedInputs;
        List<Label> Result;
        List<CheckBox> CheckBoxes;
        Form1 parentForm;
        String parameters;
        ResultHandler rH;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rH"></param>
        /// <param name="parameters"></param>
        /// <param name="extractedInputs"></param>
        /// <param name="delta"></param>
        public ResultForm(ResultHandler rH, String parameters, decimal delta, List<List<ExtractedInputs>> extractedInputs)
        {
            InitializeComponent();
            this.parameters = parameters;
            this.rH = rH;
            parentForm = (Form1)Application.OpenForms[0];
            this.extractedInputs = extractedInputs;
            CheckBoxes = new List<CheckBox>();
            Result = new List<Label>();
            PopulateForm(extractedInputs.Count);
            deltaNumUpDown.Value = delta;
            // Initial comparison
            //this.resultTable = rH.compareMutants(parameters, deltaNumUpDown.Value);
            //ColorBestTestCases();
        }
        /// <summary>
        /// Old constructor.
        /// </summary>
        /// <param name="resultTable"></param>
        /// <param name="extractedInputs"></param>
        public ResultForm(List<List<int>> resultTable, List<List<ExtractedInputs>> extractedInputs)
        {
            InitializeComponent();
            parentForm = (Form1)Application.OpenForms[0];
            this.resultTable = resultTable;
            this.extractedInputs = extractedInputs;
            CheckBoxes = new List<CheckBox>();
            Result = new List<Label>();
            PopulateForm(extractedInputs.Count);
            ColorBestTestCases();
        }
        /// <summary>
        /// Colors the best test cases for full coverage.
        /// </summary>
        private void ColorBestTestCases()
        {
            foreach (CheckBox item in CheckBoxes)
            {
                item.BackColor = SystemColors.Control;
            }
            List<int> bestTest = ResultHandler.SelectBestTestCase(resultTable);
            Console.WriteLine("Choosen test cases: ");
            foreach (int item in bestTest)
            {
                Console.WriteLine((item + 1));
                CheckBoxes[item].BackColor = Color.LightGreen;
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(this.ClientSize.Width, this.ClientSize.Height - 41);
            button1.Location = new System.Drawing.Point(this.ClientSize.Width - 87, this.ClientSize.Height - 35);
            buttonExport.Location = new System.Drawing.Point(12, this.ClientSize.Height - 35);
        }
        /// <summary>
        /// Fills the GUI with the result table.
        /// </summary>
        private void PopulateForm(int mutants)
        {
            //Build table, Mutants as colums and testcases as rows
            StringBuilder table = new StringBuilder("".PadLeft(10));
            for (int i = 0; i < mutants; i++)
            {
                table.Append("M" + (i + 1) + "".PadLeft(3));
            }
            HeadLabel.Text = table.ToString();
            table.Clear();
            for (int i = 0; i < mutants; i++)
            {
                /*int padding = 9 - (((i + 1).ToString().Length) * 2);
                table.Append("T" + (i + 1) + "".PadRight(padding));
                for (int j = 0; j < resultTable[i].Count; j++)
                {
                    table.Append(resultTable[i][j].ToString() + "".PadLeft(6));
                }*/
                //Label label1 = new Label();
                ToolTip toolTip = new ToolTip();
                CheckBox checkBox = new CheckBox();
                toolTip.AutoPopDelay = 5000;
                toolTip.InitialDelay = 300;
                toolTip.ReshowDelay = 300;
                toolTip.ToolTipTitle = "T" + (i + 1);
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip.ShowAlways = true;
                //checkBox.Name = "checkBoxTC" + i;
                checkBox.AutoSize = true;
                checkBox.Location = new System.Drawing.Point(11, HeadLabel.Location.Y + (17 * (i + 1) + 4));
                checkBox.Name = "checkBoxTC" + i.ToString();
                checkBox.TabIndex = 3;
                //checkBox.Text = table.ToString();
                checkBox.UseVisualStyleBackColor = true;
                CheckBoxes.Add(checkBox);
                panel1.Controls.Add(checkBox);
                toolTip.SetToolTip(checkBox, PrintInputs(i));
                table.Clear();
            }
        }
        /// <summary>
        /// Updates the GUI with the mutation result
        /// </summary>
        private void UpdateForm()
        {
            StringBuilder row = new StringBuilder();
            for (int i = 0; i < resultTable.Count; i++)
            {
                row.Clear();
                int padding = 9 - (((i + 1).ToString().Length) * 2);
                row.Append("T" + (i + 1) + "".PadRight(padding));
                for (int j = 0; j < resultTable[i].Count; j++)
                {
                    row.Append(resultTable[i][j].ToString() + "".PadLeft(6));
                }
                string foo = "checkBoxTC" + i.ToString();
                Control test = this.Controls.Find(foo, true)[0];
                test.Text = row.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Label_Click(object sender, EventArgs e)
        {
            Label label1 = (Label)sender;
            int index = int.Parse(label1.Name);
            MessageBox.Show(PrintInputs(index));
        }
        /// <summary>
        /// Returs a string containing the input values from selected testcase.
        /// </summary>
        /// <param name="simulation">Test case to show.</param>
        /// <returns></returns>
        private string PrintInputs(int simulation)
        {
            List<ExtractedInputs> ei = extractedInputs[simulation];
            StringBuilder sb = new StringBuilder();
            foreach (ExtractedInputs item in ei)
            {
                sb.Append(item.name + ": ");
                foreach (decimal numb in item.values)
                {
                    sb.Append(numb + " ");
                }
                sb.AppendLine("");
            }
            return sb.ToString();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CheckBox item in CheckBoxes)
            {
                if (item.Checked)
                {
                    sb.AppendLine("T" + (int.Parse(item.Name) + 1) + ":");
                    sb.Append(PrintInputs(int.Parse(item.Name)));
                }
            }
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.InitialDirectory = Environment.CurrentDirectory;
            SFD.Filter = "Text File|*.txt|All files|*.*";
            SFD.FileName = "Test_Cases.txt";
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SFD.FileName, sb.ToString());
            }
        }

        private void DeltaChange(object sender, EventArgs e)
        {
            this.resultTable = rH.compareMutants(parameters, deltaNumUpDown.Value);
            UpdateForm();
            ColorBestTestCases();
        }
    }
}
