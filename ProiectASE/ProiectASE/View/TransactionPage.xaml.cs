using ProiectASE.Model;
using ProiectASE.ViewModel;
using System.Windows;

namespace ProiectASE.View
{
    /// <summary>
    /// Interaction logic for TransactionPage.xaml
    /// </summary>
    public partial class TransactionPage : Window
    {
        private Transaction t;
        private MainService mainService;
        public TransactionPage(Transaction t)
        {
            InitializeComponent();
            this.t = t;
            mainService = MainService.Instance;
        }
    }
}
