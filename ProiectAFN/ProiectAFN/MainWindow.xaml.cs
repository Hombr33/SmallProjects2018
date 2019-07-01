using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ProiectAFN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Measurements measurements;
        public MainWindow()
        {
            InitializeComponent();
            measurements = new Measurements();
            this.uiBombType.SelectedIndex = 0;
            this.uiHeight.SelectedIndex = 0;
        }

        private void uiCompute_Click(object sender, RoutedEventArgs e)
        {
            string bombType = ((Label)this.uiBombType.SelectedItem).Content.ToString();
            double distance = Convert.ToDouble(this.uiDistance.Text);
            double height = Convert.ToDouble(((Label)this.uiHeight.SelectedItem).Tag.ToString());
            double temperature = Convert.ToDouble(this.uiTemp.Text);
            double pressure = Convert.ToDouble(this.uiPressure.Text);
            double windSpeed = Convert.ToDouble(this.uiWindSpeed.Text);

            double distanceCorrectionByHeight = Math.Truncate(100000 * measurements.DistanceCorrectionByHeight(bombType, distance, height)) / 100000;
            distance -= distanceCorrectionByHeight; 

            double angle = Math.Truncate(100000 * measurements.InterpolateAngleFromDistance(bombType, distance)) / 100000;
            double tempAngleCor = Math.Truncate(100000 * measurements.AngleCorrectionByTemp(temperature)) / 100000;
            double pressureAngleCor = Math.Truncate(100000 * measurements.AngleCorrectionByPressure(pressure)) / 100000;
            double windAngleCor = Math.Truncate(100000 * measurements.AngleCorrectionByWind(windSpeed)) / 100000;

            angle += tempAngleCor + pressureAngleCor + windAngleCor;
            double time = Math.Truncate(100000 * measurements.InterpolateTimeFromDistance(bombType, distance)) / 100000;

            this.uiAngle.Text = angle.ToString();
            this.uiTime.Text = time.ToString();
        }

        private void uiBombType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string bombType = ((Label)this.uiBombType.SelectedItem).Content.ToString();
            if (bombType.Contains("1200"))
            {
                this.uiHeight.Items.Clear();
                this.uiHeight.Items.Add(_CreateHeightLabel(3, "3"));
                this.uiHeight.Items.Add(_CreateHeightLabel(6, "6"));
                this.uiHeight.Items.Add(_CreateHeightLabel(9, "9"));
                this.uiHeight.SelectedIndex = 0;
            }
            else if (bombType.Contains("2500"))
            {
                this.uiHeight.Items.Clear();
                this.uiHeight.Items.Add(_CreateHeightLabel(3, "3 - 7"));
                this.uiHeight.Items.Add(_CreateHeightLabel(8, "8 - 11"));
                this.uiHeight.SelectedIndex = 0;
            } else
            {
                throw new BombNotFoundException();
            }
        }

        private Label _CreateHeightLabel(int height, string content)
        {
            Label l = new Label();
            l.Tag = height.ToString();
            l.Content = content;
            return l;
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void uiDistance_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void uiDistance_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
