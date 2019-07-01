using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace PokerPicker
{
    public class MainWindowViewModel
    {
        private string path = Environment.CurrentDirectory + "\\Labels.txt";
        public MainWindowViewModel(ColorPicker cp)
        {
            this.cp = cp;
            ReadFromFile(path);
        }

        private void ReadFromFile(string path)
        {
            string labelsText = File.ReadAllText(path)
                .Replace(" ", "").Replace("\n", "").Replace("\r", "");
            cards = labelsText.Split(',');
        }

        private ColorPicker cp;
        private List<Label> labels;
        private List<Color> savedColors;
        private string[] cards;

        private Color selectedColor;
        public Color SelectedColor {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                cp.SelectedColor = value;
            }
        }
        public void ChangeRectangleColor(Label r, Brush b)
        {
            r.Background = b;
        }

        public void GenerateRectangles(Grid grid)
        {
            labels = new List<Label>();
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Label lbl = new Label();

                    lbl.BorderBrush = new SolidColorBrush(Colors.Black);
                    lbl.BorderThickness = new Thickness(0.85);
                    lbl.Background = new SolidColorBrush(Colors.Transparent);
                    lbl.Content = cards[i + 13 * j];
                    lbl.FontSize = 15;
                    lbl.VerticalContentAlignment = VerticalAlignment.Center;
                    lbl.HorizontalContentAlignment = HorizontalAlignment.Center;

                    lbl.MouseDown += Rect_MouseDown;

                    Grid.SetRow(lbl, i);
                    Grid.SetColumn(lbl, j);

                    grid.Children.Add(lbl);

                    labels.Add(lbl);
                }
            }
        }

        public void ClearColor(Ellipse item)
        {
            item.Fill = null;
        }

        public void InitializeColors(ListBox colorList)
        {
            colorList.SelectionChanged += ColorList_SelectionChanged;
        }

        private void ColorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ellipse el = (Ellipse)e.AddedItems[0];
            if (el.Fill == null)
                return;
            Color currentColor = ((SolidColorBrush)el.Fill).Color;
            if (currentColor != Colors.White)
                SelectedColor = currentColor;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        public void AddMainColors(ListBox listBox)
        {
            if (savedColors == null)
                savedColors = new List<Color>();

            if (listBox.Items.Count == 10)
                return;

            Ellipse ellipse = new Ellipse();
            ellipse.Width = 25;
            ellipse.Height = 25;
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            ellipse.Margin = new Thickness(5, 5, 0, 5);

            listBox.Items.Add(ellipse);

        }


        private void Rect_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChangeRectangleColor((Label)sender, new SolidColorBrush(SelectedColor));
        }

        public void ResetAll()
        {
            foreach (Label r in labels)
                ChangeRectangleColor(r, new SolidColorBrush(Colors.White));
        }

        public void SaveColor(Ellipse item)
        {
            item.Fill = new SolidColorBrush(SelectedColor);
        }
    }
}
