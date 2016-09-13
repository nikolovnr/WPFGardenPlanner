using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFGardenPlanner
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Title = "Login window";
        }

        private void buttonLogIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
            //TODO: Change the address of the URL in XAML
        }

        private void textBoxEMail_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxEMail.Text))
            {
                textBoxEMail.Visibility = System.Windows.Visibility.Collapsed;
                waterMarkedEMail.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void waterMarkedEMail_GotFocus(object sender, RoutedEventArgs e)
        {
            waterMarkedEMail.Visibility = System.Windows.Visibility.Collapsed;
            textBoxEMail.Visibility = System.Windows.Visibility.Visible;
            textBoxEMail.Focus();
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void waterMarkedPassword_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    } 
}
