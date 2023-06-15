using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatApp.Utils;

namespace ChatApp.Pages.Auth
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            PageManager.UserLogin(Login.Text, Password.Password);
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            PageManager.CreateLayout(new RegisterPage());
        }
    }
}
