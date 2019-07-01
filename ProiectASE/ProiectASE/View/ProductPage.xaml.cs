using ProiectASE.Model;
using ProiectASE.ViewModel;
using System.Windows;

namespace ProiectASE.View
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Window
    {
        private Product p;
        MainService mainService;

        public ProductPage(Product p)
        {
            InitializeComponent();
            this.p = p;
            mainService = MainService.Instance;

            this.uiTBId.Text = p.Id.ToString();
            this.uiTBPret.Text = p.Price.ToString();
            this.uiTBName.Text = p.Name.ToString();
            this.uiTBPD.Text = p.ProductionDate.ToShortDateString();
            this.uiTBED.Text = p.ExpirationDate.ToShortDateString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainService.DeleteProduct(p);
            Message message = new Message("Produsul a fost sters");
            message.Show();
            this.Close();
        }
    }
}
