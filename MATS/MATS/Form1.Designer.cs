using System;

namespace MATS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.verifyta_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.InputQueryTxtBox = new System.Windows.Forms.TextBox();
            this.numericUpDownSimRuns = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.selectModelButton = new System.Windows.Forms.Button();
            this.labelSelectedModel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.StepSizeSelector = new System.Windows.Forms.NumericUpDown();
            this.simTimeNumeric = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.select_MutantsButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.periodLengthNumeric = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.outputQueryTxtBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.periodCountNumeric = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownDelta = new System.Windows.Forms.NumericUpDown();
            this.labelSelectedMutants = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSimRuns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StepSizeSelector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simTimeNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodLengthNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodCountNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelta)).BeginInit();
            this.SuspendLayout();
            // 
            // verifyta_button
            // 
            this.verifyta_button.Location = new System.Drawing.Point(177, 200);
            this.verifyta_button.Name = "verifyta_button";
            this.verifyta_button.Size = new System.Drawing.Size(130, 50);
            this.verifyta_button.TabIndex = 1;
            this.verifyta_button.Text = "Run Verifyta";
            this.verifyta_button.UseVisualStyleBackColor = true;
            this.verifyta_button.Click += new System.EventHandler(this.runVerifyta_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Simulation Runs:";
            // 
            // InputQueryTxtBox
            // 
            this.InputQueryTxtBox.Location = new System.Drawing.Point(15, 131);
            this.InputQueryTxtBox.Multiline = true;
            this.InputQueryTxtBox.Name = "InputQueryTxtBox";
            this.InputQueryTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InputQueryTxtBox.Size = new System.Drawing.Size(207, 63);
            this.InputQueryTxtBox.TabIndex = 3;
            this.InputQueryTxtBox.Text = "pBrakePedalLDM_ElSignalIn\r\npLDM_Sensor_FL_TicksIn\r\npLDM_Sensor_FR_TicksIn\r\npLDM_S" +
    "ensor_RL_TicksIn\r\npLDM_Sensor_RR_TicksIn";
            // 
            // numericUpDownSimRuns
            // 
            this.numericUpDownSimRuns.Location = new System.Drawing.Point(110, 66);
            this.numericUpDownSimRuns.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSimRuns.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownSimRuns.Name = "numericUpDownSimRuns";
            this.numericUpDownSimRuns.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownSimRuns.TabIndex = 5;
            this.numericUpDownSimRuns.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Input Query Parameters:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Reference Model:";
            // 
            // selectModelButton
            // 
            this.selectModelButton.Location = new System.Drawing.Point(110, 8);
            this.selectModelButton.Name = "selectModelButton";
            this.selectModelButton.Size = new System.Drawing.Size(75, 23);
            this.selectModelButton.TabIndex = 8;
            this.selectModelButton.Text = "Select";
            this.selectModelButton.UseVisualStyleBackColor = true;
            this.selectModelButton.Click += new System.EventHandler(this.selectModelButton_Click);
            // 
            // labelSelectedModel
            // 
            this.labelSelectedModel.AutoSize = true;
            this.labelSelectedModel.Location = new System.Drawing.Point(191, 13);
            this.labelSelectedModel.Name = "labelSelectedModel";
            this.labelSelectedModel.Size = new System.Drawing.Size(76, 13);
            this.labelSelectedModel.TabIndex = 9;
            this.labelSelectedModel.Text = "None selected";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(311, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Sample Size:";
            // 
            // StepSizeSelector
            // 
            this.StepSizeSelector.DecimalPlaces = 3;
            this.StepSizeSelector.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.StepSizeSelector.Location = new System.Drawing.Point(387, 66);
            this.StepSizeSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.StepSizeSelector.Name = "StepSizeSelector";
            this.StepSizeSelector.Size = new System.Drawing.Size(66, 20);
            this.StepSizeSelector.TabIndex = 12;
            this.StepSizeSelector.Value = new decimal(new int[] {
            500,
            0,
            0,
            262144});
            // 
            // simTimeNumeric
            // 
            this.simTimeNumeric.Enabled = false;
            this.simTimeNumeric.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.simTimeNumeric.Location = new System.Drawing.Point(110, 92);
            this.simTimeNumeric.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.simTimeNumeric.Name = "simTimeNumeric";
            this.simTimeNumeric.ReadOnly = true;
            this.simTimeNumeric.Size = new System.Drawing.Size(48, 20);
            this.simTimeNumeric.TabIndex = 13;
            this.simTimeNumeric.ThousandsSeparator = true;
            this.simTimeNumeric.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Simulation Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Mutated Models:";
            // 
            // select_MutantsButton
            // 
            this.select_MutantsButton.Location = new System.Drawing.Point(110, 37);
            this.select_MutantsButton.Name = "select_MutantsButton";
            this.select_MutantsButton.Size = new System.Drawing.Size(75, 23);
            this.select_MutantsButton.TabIndex = 16;
            this.select_MutantsButton.Text = "Select";
            this.select_MutantsButton.UseVisualStyleBackColor = true;
            this.select_MutantsButton.Click += new System.EventHandler(this.select_MutantsButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(164, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Period Length:";
            // 
            // periodLengthNumeric
            // 
            this.periodLengthNumeric.Location = new System.Drawing.Point(246, 92);
            this.periodLengthNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.periodLengthNumeric.Name = "periodLengthNumeric";
            this.periodLengthNumeric.Size = new System.Drawing.Size(59, 20);
            this.periodLengthNumeric.TabIndex = 18;
            this.periodLengthNumeric.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.periodLengthNumeric.ValueChanged += new System.EventHandler(this.Period_Changed);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(236, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Output Query Parameter (one):";
            // 
            // outputQueryTxtBox
            // 
            this.outputQueryTxtBox.Location = new System.Drawing.Point(239, 131);
            this.outputQueryTxtBox.Name = "outputQueryTxtBox";
            this.outputQueryTxtBox.Size = new System.Drawing.Size(214, 20);
            this.outputQueryTxtBox.TabIndex = 20;
            this.outputQueryTxtBox.Text = "energy";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(164, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Period Count:";
            // 
            // periodCountNumeric
            // 
            this.periodCountNumeric.Location = new System.Drawing.Point(246, 66);
            this.periodCountNumeric.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.periodCountNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.periodCountNumeric.Name = "periodCountNumeric";
            this.periodCountNumeric.Size = new System.Drawing.Size(59, 20);
            this.periodCountNumeric.TabIndex = 24;
            this.periodCountNumeric.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.periodCountNumeric.ValueChanged += new System.EventHandler(this.Period_Changed);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(311, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Output Delta:";
            // 
            // numericUpDownDelta
            // 
            this.numericUpDownDelta.DecimalPlaces = 1;
            this.numericUpDownDelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDelta.Location = new System.Drawing.Point(387, 92);
            this.numericUpDownDelta.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownDelta.Name = "numericUpDownDelta";
            this.numericUpDownDelta.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownDelta.TabIndex = 26;
            this.numericUpDownDelta.Value = new decimal(new int[] {
            35,
            0,
            0,
            65536});
            // 
            // labelSelectedMutants
            // 
            this.labelSelectedMutants.AutoSize = true;
            this.labelSelectedMutants.Location = new System.Drawing.Point(191, 42);
            this.labelSelectedMutants.Name = "labelSelectedMutants";
            this.labelSelectedMutants.Size = new System.Drawing.Size(78, 13);
            this.labelSelectedMutants.TabIndex = 27;
            this.labelSelectedMutants.Text = "None Selected";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 261);
            this.Controls.Add(this.labelSelectedMutants);
            this.Controls.Add(this.numericUpDownDelta);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.periodCountNumeric);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.outputQueryTxtBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.periodLengthNumeric);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.select_MutantsButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.simTimeNumeric);
            this.Controls.Add(this.StepSizeSelector);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelSelectedModel);
            this.Controls.Add(this.selectModelButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownSimRuns);
            this.Controls.Add(this.InputQueryTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.verifyta_button);
            this.Name = "Form1";
            this.Text = "MATS";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSimRuns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StepSizeSelector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simTimeNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodLengthNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodCountNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button verifyta_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InputQueryTxtBox;
        private System.Windows.Forms.NumericUpDown numericUpDownSimRuns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selectModelButton;
        private System.Windows.Forms.Label labelSelectedModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown StepSizeSelector;
        private System.Windows.Forms.NumericUpDown simTimeNumeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button select_MutantsButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown periodLengthNumeric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox outputQueryTxtBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown periodCountNumeric;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownDelta;
        private System.Windows.Forms.Label labelSelectedMutants;
    }
}

