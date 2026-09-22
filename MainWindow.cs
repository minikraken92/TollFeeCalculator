namespace TollFeeCalculator
{
    public partial class MainWindow : Form
    {
        private readonly List<DateTime> enteredTimes = new List<DateTime>();
        private readonly TollFeeReportBuilder reportBuilder = new TollFeeReportBuilder(new TollCalculator());

        public MainWindow()
        {
            InitializeComponent();

            vehicleComboBox.Items.AddRange(new object[]
            {
                "Car",
                "Motorbike",
                "Tractor",
                "Emergency",
                "Diplomat",
                "Foreign",
                "Military"
            });
            vehicleComboBox.SelectedIndex = 0;
            timeInputPicker.Value = DateTime.Now;
        }

        private void AddTimeButton_Click(object sender, EventArgs e)
        {
            DateTime enteredTime = timeInputPicker.Value;
            enteredTimes.Add(enteredTime);
            timesListBox.Items.Add(enteredTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void DeleteTimeButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = timesListBox.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }

            enteredTimes.RemoveAt(selectedIndex);
            timesListBox.Items.RemoveAt(selectedIndex);
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            dailyFeesListBox.Items.Clear();
            monthlySummaryListBox.Items.Clear();

            if (enteredTimes.Count == 0)
            {
                dailyFeesListBox.Items.Add("Add at least one date/time");
                return;
            }

            string vehicleType = vehicleComboBox.SelectedItem?.ToString() ?? string.Empty;
            Vehicle vehicle = CreateVehicle(vehicleType);

            IReadOnlyList<DailyTollFee> dailyFees = reportBuilder.GetDailyFees(vehicle, enteredTimes);
            foreach (DailyTollFee dailyFee in dailyFees)
            {
                dailyFeesListBox.Items.Add($"{dailyFee.Date:yyyy-MM-dd}: {dailyFee.Fee} kr");
            }

            IReadOnlyList<MonthlyTollFeeSummary> monthlySummaries = reportBuilder.GetMonthlySummaries(dailyFees);
            foreach (MonthlyTollFeeSummary monthlySummary in monthlySummaries)
            {
                DateTime monthStart = new DateTime(monthlySummary.Year, monthlySummary.Month, 1);
                monthlySummaryListBox.Items.Add($"{monthStart:yyyy-MM}: {monthlySummary.TotalFee} kr");
            }
        }

        private static Vehicle CreateVehicle(string vehicleType)
        {
            return vehicleType switch
            {
                "Car" => new Car(),
                "Motorbike" => new Motorbike(),
                "Tractor" => new Tractor(),
                "Emergency" => new Emergency(),
                "Diplomat" => new Diplomat(),
                "Foreign" => new Foreign(),
                "Military" => new Military(),
                _ => throw new ArgumentOutOfRangeException(nameof(vehicleType))
            };
        }

        private void timeInputPicker_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
