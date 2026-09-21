namespace TollFeeCalculator
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        private Label vehicleLabel;
        private ComboBox vehicleComboBox;
        private Label timeLabel;
        private DateTimePicker timeInputPicker;
        private Button addTimeButton;
        private Label enteredTimesLabel;
        private ListBox timesListBox;
        private Button calculateButton;
        private Label resultLabel;
        private TextBox resultTextBox;

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            vehicleLabel = new Label();
            vehicleComboBox = new ComboBox();
            timeLabel = new Label();
            timeInputPicker = new DateTimePicker();
            addTimeButton = new Button();
            enteredTimesLabel = new Label();
            timesListBox = new ListBox();
            calculateButton = new Button();
            resultLabel = new Label();
            resultTextBox = new TextBox();
            SuspendLayout();
            // 
            // vehicleLabel
            // 
            vehicleLabel.AutoSize = true;
            vehicleLabel.Location = new Point(12, 15);
            vehicleLabel.Name = "vehicleLabel";
            vehicleLabel.Size = new Size(47, 15);
            vehicleLabel.TabIndex = 0;
            vehicleLabel.Text = "Vehicle:";
            // 
            // vehicleComboBox
            // 
            vehicleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            vehicleComboBox.Location = new Point(110, 12);
            vehicleComboBox.Name = "vehicleComboBox";
            vehicleComboBox.Size = new Size(160, 23);
            vehicleComboBox.TabIndex = 1;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(12, 51);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(73, 15);
            timeLabel.TabIndex = 2;
            timeLabel.Text = "Time of day:";
            // 
            // timeInputPicker
            // 
            timeInputPicker.CustomFormat = "HH:mm:ss";
            timeInputPicker.Format = DateTimePickerFormat.Custom;
            timeInputPicker.Location = new Point(110, 45);
            timeInputPicker.Name = "timeInputPicker";
            timeInputPicker.ShowUpDown = true;
            timeInputPicker.Size = new Size(160, 23);
            timeInputPicker.TabIndex = 3;
            timeInputPicker.ValueChanged += timeInputPicker_ValueChanged;
            // 
            // addTimeButton
            // 
            addTimeButton.Location = new Point(280, 44);
            addTimeButton.Name = "addTimeButton";
            addTimeButton.Size = new Size(90, 25);
            addTimeButton.TabIndex = 4;
            addTimeButton.Text = "Add";
            addTimeButton.Click += AddTimeButton_Click;
            // 
            // enteredTimesLabel
            // 
            enteredTimesLabel.AutoSize = true;
            enteredTimesLabel.Location = new Point(12, 82);
            enteredTimesLabel.Name = "enteredTimesLabel";
            enteredTimesLabel.Size = new Size(82, 15);
            enteredTimesLabel.TabIndex = 5;
            enteredTimesLabel.Text = "Entered times:";
            // 
            // timesListBox
            // 
            timesListBox.Location = new Point(12, 105);
            timesListBox.Name = "timesListBox";
            timesListBox.Size = new Size(358, 109);
            timesListBox.TabIndex = 6;
            // 
            // calculateButton
            // 
            calculateButton.Location = new Point(12, 238);
            calculateButton.Name = "calculateButton";
            calculateButton.Size = new Size(130, 30);
            calculateButton.TabIndex = 7;
            calculateButton.Text = "Calculate toll fee";
            calculateButton.Click += CalculateButton_Click;
            // 
            // resultLabel
            // 
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(155, 246);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new Size(42, 15);
            resultLabel.TabIndex = 8;
            resultLabel.Text = "Result:";
            // 
            // resultTextBox
            // 
            resultTextBox.Location = new Point(210, 242);
            resultTextBox.Name = "resultTextBox";
            resultTextBox.ReadOnly = true;
            resultTextBox.Size = new Size(160, 23);
            resultTextBox.TabIndex = 9;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 320);
            Controls.Add(vehicleLabel);
            Controls.Add(vehicleComboBox);
            Controls.Add(timeLabel);
            Controls.Add(timeInputPicker);
            Controls.Add(addTimeButton);
            Controls.Add(enteredTimesLabel);
            Controls.Add(timesListBox);
            Controls.Add(calculateButton);
            Controls.Add(resultLabel);
            Controls.Add(resultTextBox);
            Name = "MainWindow";
            Text = "Toll Fee Calculator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
