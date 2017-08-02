using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MATS
{
    //
    class Mutator
    {
        private class Mutants
        {
            public int model;
            public int simulation;
            private SimulationTrace mutant;
            public Mutants(int model, int simulation, SimulationTrace mutant)
            {
                this.model = model;
                this.simulation = simulation;
                this.mutant = mutant;
            }
        }

        //first list is for simulations second is for parameters
        private List<List<ExtractedInputs>> extractedInputs;

        public Mutator()
        {
        }
        public List<List<ExtractedInputs>> GetExtractedInputs()
        {
            return extractedInputs;
        }
        public List<List<string>> MutateFiles(string[] mutationModels, int simulations)
        {
            List<List<string>> fileList = new List<List<string>>();
            StringBuilder sB;
            for (int i = 0; i < simulations; i++)
            {
                fileList.Add(new List<string>());
                int num = 1;
                string tmp = createInputs(i);
                foreach (string model in mutationModels)
                {
                    string test;
                    test = File.ReadAllText(model);
                    if (!test.Contains("//MUTANAL INSERT HERE"))
                    {
                        throw new ApplicationException("Mutant not correct!");
                    }
                    sB = new StringBuilder(test);
                    sB.Replace("//MUTANAL INSERT HERE", tmp);
                    string fileName = ".\\Mutations\\BBWImplModelM" + num++ + "T" + (i + 1) + ".xml";
                    Directory.CreateDirectory(".\\Mutations");
                    File.WriteAllText(fileName, sB.ToString());
                    fileList[i].Add(fileName);
                }
            }
            return fileList;
        }
        public void inputBuilder(string[] inputs, TraceManager tm, int simulations)
        {
            extractedInputs = new List<List<ExtractedInputs>>();
            //Loop for extracting and storing each extracted input
            for (int i = 0; i < simulations; i++)
            {
                extractedInputs.Add(new List<ExtractedInputs>());
                for (int j = 0; j < inputs.Count(); j++)
                {
                    extractedInputs[i].Add(new ExtractedInputs(inputs[j], tm.ExtractInputs(inputs[j], i)));
                }
            }
        }
        public string printInputs(int simulations)
        {
            string exInputs = "";
            //loop for building the outputstring of extracted parameters
            for (int i = 0; i < simulations; i++)
            {
                exInputs += "Simulation " + (i + 1) + ":\r\n";
                foreach (ExtractedInputs item in extractedInputs[i])
                {
                    string values = "";
                    for (int j = 0; j < item.values.Count(); j++)
                    {
                        values += item.values[j] + " ";
                    }
                    exInputs += item.name + ": " + values + "\r\n";
                }
            }
            return exInputs;
        }
        private string createInputs(int sim)
        {
            string exInputs = "";
            //loop for building the outputstring of extracted parameters

            foreach (ExtractedInputs item in extractedInputs[sim])
            {
                string name = item.name;
                int tmp = name.LastIndexOf('_');
                name = name.Remove(tmp, name.Length - tmp).Insert(tmp, "_array");
                string values = item.values[0].ToString();
                for (int j = 1; j < item.values.Count(); j++)
                {
                    if ((item.values[j] % 1) != 0)
                    {
                        System.Windows.Forms.MessageBox.Show("ERROR INPUT NOT INTEGER");
                    }
                    values += ", " + item.values[j];
                }
                exInputs += "int " + name + " [" + item.values.Count() + "]={" + values + "};\r\n";
            }
            return exInputs;
        }
    }
}
