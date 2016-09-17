using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GardenPlanner
{    
    public partial class SignIn : Window
    {
        public SignIn()
       //Database db = new Database();      
        {
            InitializeComponent();
            Title = "Sign up";
        }

        private void textBoxName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                textBoxName.Visibility = System.Windows.Visibility.Collapsed;
                watermarkName.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void watermarkName_GotFocus(object sender, RoutedEventArgs e)
        {
            watermarkName.Visibility = System.Windows.Visibility.Collapsed;
            textBoxName.Visibility = System.Windows.Visibility.Visible;
            textBoxName.Focus();
        }

        private void textBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEmail.Text))
            {
                textBoxEmail.Visibility = System.Windows.Visibility.Collapsed;
                watermarkEmail.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void watermarkEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            watermarkEmail.Visibility = System.Windows.Visibility.Collapsed;
            textBoxEmail.Visibility = System.Windows.Visibility.Visible;
            textBoxEmail.Focus();
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBox1.Password))
            {
                passwordBox1.Visibility = System.Windows.Visibility.Collapsed;
                watermarkPassword1.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void watermarkPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            watermarkPassword1.Visibility = System.Windows.Visibility.Collapsed;
            passwordBox1.Visibility = System.Windows.Visibility.Visible;
            passwordBox1.Focus();
        }

        private void passwordBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBox2.Password))
            {
                passwordBox2.Visibility = System.Windows.Visibility.Collapsed;
                watermarkPassword2.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void watermarkPassword2_GotFocus(object sender, RoutedEventArgs e)
        {
            watermarkPassword2.Visibility = System.Windows.Visibility.Collapsed;
            passwordBox2.Visibility = System.Windows.Visibility.Visible;
            passwordBox2.Focus();
        }

        private void Hyperlink_RequestNavigatePlantHardiness(object sender, RequestNavigateEventArgs e)
        {
            string country = ((ComboBoxItem)comboBoxGardenCountry.SelectedItem).Content.ToString();
            switch (country)
            {
                case "Canada":
                    Hyperlink hyperlinkCanada = new Hyperlink()
                    {
                        NavigateUri = new Uri("http://www.veseys.com/ca/en/learn/reference/hardinesszones")

                    };
                    Process.Start(new ProcessStartInfo(hyperlinkCanada.NavigateUri.AbsoluteUri));
                    break;
                case "United States":
                    Hyperlink hyperlinkUSA = new Hyperlink()
                    {
                        NavigateUri = new Uri("http://garden.org/nga/zipzone/")
                    };
                    Process.Start(new ProcessStartInfo(hyperlinkUSA.NavigateUri.AbsoluteUri));
                    break;
                default:
                    MessageBox.Show("In order to help you with this, you need to select your country.");
                    break;
            }
            e.Handled = true;
        }

        private void Hyperlink_RequestNavigateFrostDates(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void buttonSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text.Length == 0)
            {
                MessageBox.Show("Enter your first and last name.");
                textBoxName.Focus();
            }

            if (textBoxEmail.Text.Length == 0)
            {
                MessageBox.Show("Enter an email.");
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Enter a valid email.");
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string name = textBoxName.Text;
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                if (passwordBox1.Password.Length == 0)
                {
                    MessageBox.Show("Enter a password.");
                    passwordBox1.Focus();
                }
                else if (passwordBox2.Password.Length == 0)
                {
                    MessageBox.Show("Retype your password to confirm it.");
                    passwordBox2.Focus();
                }
                else if (passwordBox1.Password != passwordBox2.Password)
                {
                    MessageBox.Show("Both passwords must be the same.");
                    passwordBox2.Focus();
                }
                else
                {
                    int plantHardinessZone = (int)comboBoxPlantHardinessZone.SelectedItem;
                    DateTime lastSpringFrostDate = datepickerLastSpringFrostDate.SelectedDate.Value.Date;
                    DateTime firstFallFrostDate = datepickerFirstFallFrostDate.SelectedDate.Value.Date;
                    TimeSpan difference = firstFallFrostDate.Subtract(lastSpringFrostDate);
                    int frostFreeGrowingSeason = (int)difference.TotalDays;

                    //AddCustomer();                    
                    MessageBox.Show("You have Registered successfully.");
                    //Window Main = new Window();
                    //MainWindow.Show();
                    this.Close();

                }
            }
        }
    }
}