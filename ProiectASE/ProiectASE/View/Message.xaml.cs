using System;
using System.Windows;

namespace ProiectASE.View
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        private string msg = String.Empty;
        public Message(string msg)
        {
            this.msg = msg;
            InitializeComponent();
            this.uiMsgTB.Text = msg;
        }
    }
}
