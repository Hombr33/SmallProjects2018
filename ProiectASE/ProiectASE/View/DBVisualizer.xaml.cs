using ProiectASE.Model;
using ProiectASE.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProiectASE.View
{
    /// <summary>
    /// Interaction logic for DBVisualizer.xaml
    /// </summary>
    public partial class DBVisualizer : Window
    {
        private MainService mainService;
        public DBVisualizer()
        {
            InitializeComponent();
            InitLists();
        }

        private void InitLists()
        {
            mainService = MainService.Instance;
            Client[] clients = mainService.QueryAllClients();
            Product[] products = mainService.QueryAllGeneralProducts();
            Transaction[] trx = mainService.QueryAllTx();

            Array.ForEach<Client>(clients, (c) => 
            {
                ListViewItem l = new ListViewItem();
                l.FontSize = 20;
                l.Margin = new Thickness(10, 5, 10, 5);
                l.Content = c.Id + ". " + c.Name;
                l.MouseDoubleClick += (sender, e) => ClientDoubleClick(sender, e, c);
                this.uiClientsList.Items.Add(l);
            });

            Array.ForEach<Product>(products, (p) =>
            {
                ListViewItem l = new ListViewItem();
                l.FontSize = 20;
                l.Margin = new Thickness(10, 5, 10, 5);
                l.Content = p.Id + ". " + p.Name;
                l.MouseDoubleClick += (sender, e) => ProductDoubleClick(sender, e, p);
                this.uiProductsList.Items.Add(l);
            });

            Array.ForEach<Transaction>(trx, (t) =>
            {
                ListViewItem l = new ListViewItem();
                l.FontSize = 20;
                l.Margin = new Thickness(10, 5, 10, 5);
                l.Content = t.Id + " -> " + t.Client.Name;
                l.MouseDoubleClick += (sender, e) => TxDoubleClick(sender, e, t);
                this.uiTxList.Items.Add(l);
            });
        }

        private void ClientDoubleClick(object sender, MouseButtonEventArgs e, Client c)
        {
            ClientPage cp = new ClientPage(c);
            cp.Show();
        }

        private void ProductDoubleClick(object sender, MouseButtonEventArgs e, Product p)
        {
            ProductPage pp = new ProductPage(p);
            pp.Show();
        }

        private void TxDoubleClick(object sender, MouseButtonEventArgs e, Transaction t)
        {
            TransactionPage tp = new TransactionPage(t);
            tp.Show();
        }
    }
}
