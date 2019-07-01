using System;
using System.Windows.Forms;

namespace ProductActivationAction
{
    public partial class frmLicenseInfo : Form
    {
        public frmLicenseInfo()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.TopMost = true;
            this.productKey_box.PasswordChar = '*';
        }

        private void NextClick(object sender, EventArgs e)
        {
            if (this.productKey_box.Text == "qazwsxedc123")
            {
                this.DialogResult = DialogResult.Yes;
            } else
                MessageBox.Show("Key is invalid! Please provide the real key");
        }

    }
}
