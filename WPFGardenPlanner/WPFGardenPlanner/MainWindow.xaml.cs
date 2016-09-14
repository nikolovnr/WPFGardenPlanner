using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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
using System.Xml;
using System.Xml.Linq;

namespace WPFGardenPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        bool isDrawingBeds = false;
        string SeedPlant;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        
        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            GardenSchema.Children.Add(new WPF.MDI.MdiChild() { Title = "Garden Shema", Background = System.Windows.Media.Brushes.Green, Content =  new Canvas() { Background = Brushes.Green } });
            
        }
        
        private void RibbonButton_Click_GardenBed(object sender, RoutedEventArgs e)
        {
            isDrawingBeds = true;
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button someButton = sender as Button;
            Image someImage = sender as Image;
            if (someButton != null)
            {
                if (isDrawingBeds)
                {
                    someButton.Background = Brushes.Brown;
                }
                else
                {
                    //var ss = someButton.Content;
                    Image imgControl = new Image();
                    //var bitmapImage = new BitmapImage(new Uri(@"C:\Users\Nikolay Nikolov\Documents\WPFGardenPlanner2\WPFGardenPlanner\WPFGardenPlanner\Pictures\Plants\Beet.png"));


                    //imgControl.Source = bitmapImage;


                    var bitmapImage = new BitmapImage(new Uri(SeedPlant));

                    imgControl.Source = bitmapImage;

                    someButton.Content = imgControl;

                   
                }
            } 

        }

        private void RibbonButton_Plants(object sender, RoutedEventArgs e)
        {
            Button someButton = sender as Button;
            if (someButton != null)
            {
                isDrawingBeds = false;
                SeedPlant = (string)someButton.Tag;
            }
        }

        private void SaveGardenAsImage(object sender, RoutedEventArgs e)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(grdGardenPlan);
            double dpi = 96d;


            RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);


            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(grdGardenPlan);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }

            rtb.Render(dv);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                pngEncoder.Save(ms);
                ms.Close();

                System.IO.File.WriteAllBytes("garden.png", ms.ToArray());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintGarden(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog dialog = new PrintDialog();

                if (dialog.ShowDialog() != true)
                    return;
                dialog.PrintVisual(grdGardenPlan, "IFMS Print Screen");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Screen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
