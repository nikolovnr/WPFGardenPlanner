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
using System.Data.SqlClient;
using System.Data;

namespace WPFGardenPlanner
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Title = "Login window";
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
            //TODO: Change the address of the URL in XAML
        }

        private void textBoxEMail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEMail.Text))
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
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                passwordBox.Visibility = System.Windows.Visibility.Collapsed;
                waterMarkedPassword.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void waterMarkedPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            waterMarkedPassword.Visibility = System.Windows.Visibility.Collapsed;
            passwordBox.Visibility = System.Windows.Visibility.Visible;
            passwordBox.Focus();
        }

        private void buttonLogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = textBoxEMail.Text;
            string password = passwordBox.Password;
            SqlConnection conn = new SqlConnection("Data Source=desrosiers.database.windows.net;Initial Catalog=gardenplanner;Integrated Security=False;User ID=sqladmin;Password=16Avril1889;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE Email=@Email AND Password=@Password", conn))
            {
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Sorry! Please enter existing email/password.");
                }
                else
                {
                    conn.Close();
                    Window Main = new Window();
                    Main.Show();
                    this.Close();
                }
            }
        }
    }
}
