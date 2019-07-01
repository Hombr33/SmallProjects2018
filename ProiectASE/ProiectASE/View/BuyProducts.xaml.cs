using ProiectASE.Model;
using ProiectASE.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProiectASE.View
{
    /// <summary>
    /// Interaction logic for BuyProducts.xaml
    /// </summary>
    public partial class BuyProducts : Window
    {
        private MainService mainService;
        private Client[] clients;
        private Product[] products;

        public BuyProducts()
        {
            InitializeComponent();
            InitLists();
        }

        private void InitLists()
        {
            mainService = MainService.Instance;
            clients = mainService.QueryAllClients();
            products = mainService.QueryAllGeneralProducts();

            Array.ForEach<Client>(clients, (c) =>
            {
                ListViewItem l = new ListViewItem();
                l.FontSize = 20;
                l.Margin = new Thickness(10, 5, 10, 5);
                l.Content = c.Id + ". " + c.Name;
                this.uiClientsList.Items.Add(l);
            });

            Array.ForEach<Product>(products, (p) =>
            {
                ListViewItem l = new ListViewItem();
                l.FontSize = 20;
                l.Margin = new Thickness(10, 5, 10, 5);
                l.Content = p.Id + ". " + p.Name;
                this.uiProductsList.Items.Add(l);
            });
        }

        private void uiProductsList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void uiProductsList_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem = listView.SelectedItem as ListViewItem;
                Product p = products[listView.SelectedIndex];

                DataObject dragData = new DataObject("Product", p);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        private void uiClientsList_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("Product") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void uiClientsList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Product"))
            {
                Product product = e.Data.GetData("Product") as Product;
                Client client = clients[uiClientsList.SelectedIndex];
                if (client.Budget >= product.Price)
                {
                    client = mainService.UpdateClient(client, product);
                    int txId = mainService.GetCount("Trx") + 1;
                    Transaction tx = new Transaction(txId, client);
                    mainService.RegisterTransaction(tx);

                    Message message = new Message("Produs adaugat");
                    message.Show();
                } else
                {
                    Message message = new Message("Fonduri insuficiente");
                    message.Show();
                }
            }
        }
    }
}
