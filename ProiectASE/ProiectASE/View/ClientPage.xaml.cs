using ProiectASE.Model;
using ProiectASE.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProiectASE.View
{
    /// <summary>
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Window
    {
        private MainService mainService;
        private Client c;

        public ClientPage(Client c)
        {
            InitializeComponent();
            this.c = c;
            mainService = MainService.Instance;
            this.uiTBId.Text = c.Id.ToString();
            this.uiTBBudget.Text = c.Budget.ToString();
            this.uiTBName.Text = c.Name.ToString();
            List<Product> clientProducts = mainService.FindClientProducts(c);

            foreach (Product p in clientProducts)
            {
                c.Products.Add(p);
                ListViewItem l = new ListViewItem();
                l.Content = p.Name;
                this.uiLBProducts.Items.Add(l);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainService.DeleteClient(c);
            Message message = new Message("Clientul a fost sters");
            message.Show();
            this.Close();
        }
    }
}
