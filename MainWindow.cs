namespace TollFeeCalculator
{
    public partial class MainWindow : Form
    {
        private readonly List<DateTime> enteredTimes = new List<DateTime>();
        private readonly TollCalculator tollCalculator = new TollCalculator();

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
            timesListBox.Items.Add(enteredTime.ToString("HH:mm:ss"));
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (enteredTimes.Count == 0)
            {
                resultTextBox.Text = "Add at least one time";
                return;
            }

            string vehicleType = vehicleComboBox.SelectedItem?.ToString() ?? string.Empty;
            Vehicle vehicle = CreateVehicle(vehicleType);
            int fee = tollCalculator.GetTollFee(vehicle, enteredTimes.ToArray());

            resultTextBox.Text = $"{fee} kr";
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
