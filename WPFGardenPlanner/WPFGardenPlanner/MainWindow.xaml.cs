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

namespace WPFGardenPlanner
{   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = "GardenPlanner";
        }

<<<<<<< HEAD
       
=======
        private void mnuExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
>>>>>>> 60f1680bacb742fdb56eff6384b04793cb4f5f36
    }
}
