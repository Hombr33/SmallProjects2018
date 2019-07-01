using ProiectASE.View;
using System.Windows;

namespace ProiectASE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientRegistrator clientRegistrator = new ClientRegistrator();
            clientRegistrator.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProductRegistrator productRegistrator = new ProductRegistrator();
            productRegistrator.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BuyProducts buyProducts = new BuyProducts();
            buyProducts.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DBVisualizer dBVisualizer = new DBVisualizer();
            dBVisualizer.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
    }
}
