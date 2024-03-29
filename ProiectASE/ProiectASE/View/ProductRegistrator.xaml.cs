﻿using ProiectASE.Model;
using ProiectASE.ViewModel;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProiectASE.View
{
    /// <summary>
    /// Interaction logic for ProductRegistrator.xaml
    /// </summary>
    public partial class ProductRegistrator : Window
    {
        private MainService mainService;

        public ProductRegistrator()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainService = MainService.Instance;
            string productName = this.uiNameTB.Text;
            float productPrice = Convert.ToSingle(this.uiPriceTB.Text);
            DateTime productionDate = this.uiPDTB.DisplayDate;
            DateTime expirationDate = this.uiEDTB.DisplayDate;
            int productId = mainService.GetCount("GeneralProduct") + 1;
            Product product = new Product(productId, productName, productPrice, productionDate, expirationDate);
            mainService.RegisterProduct(product);
            this.Close();
        }

        private void uiBudgetTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsControl(GetCharFromKey(e.Key)) && !char.IsDigit(GetCharFromKey(e.Key)) && (GetCharFromKey(e.Key) != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((GetCharFromKey(e.Key) == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }
        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);
        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);
        [DllImport("user32.dll")]
        public static extern int ToUnicode(
          uint wVirtKey,
          uint wScanCode,
          byte[] lpKeyState,
          [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)]
            StringBuilder pwszBuff,
          int cchBuff,
          uint wFlags);

        private char GetCharFromKey(Key key)
        {
            char ch = ' ';

            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            byte[] keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            uint scanCode = MapVirtualKey((uint)virtualKey, MapType.MAPVK_VK_TO_VSC);
            StringBuilder stringBuilder = new StringBuilder(2);

            int result = ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
            switch (result)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
                default:
                    {
                        ch = stringBuilder[0];
                        break;
                    }
            }
            return ch;
        }
    }
}
