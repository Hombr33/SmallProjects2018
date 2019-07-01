using System.Diagnostics;
using System.Windows;
using ColorWheel.Controls;
using ColorWheel.Core;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace PokerPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        private ColorPicker colorPicker;
        private ColorWheelControl colorWheel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel(this.uiColorpicker);
            viewModel.GenerateRectangles(this.uiMainGrid);
            viewModel.InitializeColors(this.uiColorsList);
            RGBColorWheel cw = new RGBColorWheel();
            this.DataContext = viewModel;
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            ColorPicker cp = (ColorPicker)sender;
            if (colorPicker == null)
                colorPicker = cp;
            if (cp.SelectedColor != null)
                viewModel.SelectedColor = (Color)(cp.SelectedColor);
        }

        private void uiMainStackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (colorPicker != null)
            //    if (colorPicker.IsOpen)
            //        colorPicker.IsOpen = false;
            //Mouse.OverrideCursor = Cursors.Hand;
        }

        private void ColorPicker_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            //ColorPicker cp = (ColorPicker)sender;
            //if (!cp.IsOpen)
            //    cp.IsOpen = true;
        }

        private void ColorPicker_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            //ColorPicker cp = (ColorPicker)sender;
            //if (cp.IsOpen)
            //    cp.IsOpen = false;
        }

        private void Erase_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectedColor = Colors.White;
            Mouse.OverrideCursor = Cursors.Cross;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ResetAll();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void uiMainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (viewModel.SelectedColor != Colors.White)
                Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddMainColors(this.uiColorsList);
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Ellipse item = (Ellipse)this.uiColorsList.SelectedItem;
            if (item == null)
                return;
            viewModel.SaveColor(item);
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Ellipse item = (Ellipse)this.uiColorsList.SelectedItem;
            if (item == null)
                return;
            viewModel.ClearColor(item);
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
