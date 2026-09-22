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
        private Button deleteTimeButton;
        private Button calculateButton;
        private Label dailyFeesLabel;
        private ListBox dailyFeesListBox;
        private Label monthlySummaryLabel;
        private ListBox monthlySummaryListBox;

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
            deleteTimeButton = new Button();
            calculateButton = new Button();
            dailyFeesLabel = new Label();
            dailyFeesListBox = new ListBox();
            monthlySummaryLabel = new Label();
            monthlySummaryListBox = new ListBox();
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
            timeLabel.Text = "Date && time:";
            //
            // timeInputPicker
            //
            timeInputPicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            timeInputPicker.Format = DateTimePickerFormat.Custom;
            timeInputPicker.Location = new Point(110, 45);
            timeInputPicker.Name = "timeInputPicker";
            timeInputPicker.ShowUpDown = true;
            timeInputPicker.Size = new Size(220, 23);
            timeInputPicker.TabIndex = 3;
            timeInputPicker.ValueChanged += timeInputPicker_ValueChanged;
            //
            // addTimeButton
            //
            addTimeButton.Location = new Point(340, 44);
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
            timesListBox.Size = new Size(328, 109);
            timesListBox.TabIndex = 6;
            //
            // deleteTimeButton
            //
            deleteTimeButton.Location = new Point(350, 105);
            deleteTimeButton.Name = "deleteTimeButton";
            deleteTimeButton.Size = new Size(80, 25);
            deleteTimeButton.TabIndex = 12;
            deleteTimeButton.Text = "Remove";
            deleteTimeButton.Click += DeleteTimeButton_Click;
            //
            // calculateButton
            //
            calculateButton.Location = new Point(12, 228);
            calculateButton.Name = "calculateButton";
            calculateButton.Size = new Size(160, 30);
            calculateButton.TabIndex = 7;
            calculateButton.Text = "Calculate toll fees";
            calculateButton.Click += CalculateButton_Click;
            //
            // dailyFeesLabel
            //
            dailyFeesLabel.AutoSize = true;
            dailyFeesLabel.Location = new Point(12, 271);
            dailyFeesLabel.Name = "dailyFeesLabel";
            dailyFeesLabel.Size = new Size(66, 15);
            dailyFeesLabel.TabIndex = 8;
            dailyFeesLabel.Text = "Daily fees:";
            //
            // dailyFeesListBox
            //
            dailyFeesListBox.Location = new Point(12, 294);
            dailyFeesListBox.Name = "dailyFeesListBox";
            dailyFeesListBox.Size = new Size(418, 109);
            dailyFeesListBox.TabIndex = 9;
            //
            // monthlySummaryLabel
            //
            monthlySummaryLabel.AutoSize = true;
            monthlySummaryLabel.Location = new Point(12, 416);
            monthlySummaryLabel.Name = "monthlySummaryLabel";
            monthlySummaryLabel.Size = new Size(103, 15);
            monthlySummaryLabel.TabIndex = 10;
            monthlySummaryLabel.Text = "Monthly summary:";
            //
            // monthlySummaryListBox
            //
            monthlySummaryListBox.Location = new Point(12, 439);
            monthlySummaryListBox.Name = "monthlySummaryListBox";
            monthlySummaryListBox.Size = new Size(418, 95);
            monthlySummaryListBox.TabIndex = 11;
            //
            // MainWindow
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 570);
            Controls.Add(vehicleLabel);
            Controls.Add(vehicleComboBox);
            Controls.Add(timeLabel);
            Controls.Add(timeInputPicker);
            Controls.Add(addTimeButton);
            Controls.Add(enteredTimesLabel);
            Controls.Add(timesListBox);
            Controls.Add(deleteTimeButton);
            Controls.Add(calculateButton);
            Controls.Add(dailyFeesLabel);
            Controls.Add(dailyFeesListBox);
            Controls.Add(monthlySummaryLabel);
            Controls.Add(monthlySummaryListBox);
            Name = "MainWindow";
            Text = "Toll Fee Calculator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
