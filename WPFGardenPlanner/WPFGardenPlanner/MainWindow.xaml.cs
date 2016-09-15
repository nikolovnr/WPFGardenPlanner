using GardenPlanner;
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
        Database db = new Database();

        Plant[,] seededPlants = new Plant[32,20];
        PlantsLookup pLookup = new PlantsLookup();

        bool isDrawingBeds = false;

        string BedColor;
        string SeedPlant;

        public MainWindow()
        {
            InitializeComponent();
            Title = "Garden Planner";
        }        
        
        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            //GardenSchema.Children.Add(new WPF.MDI.MdiChild() { Title = "Garden Shema", Background = System.Windows.Media.Brushes.Green, Content =  new Canvas() { Background = Brushes.Green } });

            for (int col = 0; col < 32; col++)
            {
                for (int row = 0; row < 20; row++)
                {
                    seededPlants[col, row] = null;
                    var btn = grdGardenPlan.Children.Cast<Button>().First(eg => Grid.GetRow(eg) == row && Grid.GetColumn(eg) == col);
                    btn.Content = null;
                }
            }
            
        }
        
        private void RibbonButton_GardenBed(object sender, RoutedEventArgs e)
        {
            isDrawingBeds = true;
            Button someButton = sender as Button;
            if (someButton != null)
            {
                isDrawingBeds = true;
                BedColor = (string)someButton.Tag;
            }
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button someButton = sender as Button;
            Image someImage = sender as Image;
            if (someButton != null)
            {               
                if (isDrawingBeds)
                {
                    switch(BedColor)
                    {
                        case "Cornsilk":
                            someButton.Background = Brushes.Cornsilk;
                            break;
                        case "Wheat":
                            someButton.Background = Brushes.Wheat;
                            break;
                        case "BurlyWood":
                            someButton.Background = Brushes.BurlyWood;
                            break;
                        case "Tan":
                            someButton.Background = Brushes.Tan;
                            break;
                        case "Goldenrod":
                            someButton.Background = Brushes.Goldenrod;
                            break;
                        case "DarkGoldenrod":
                            someButton.Background = Brushes.DarkGoldenrod;
                            break;
                        case "SaddleBrown":
                            someButton.Background = Brushes.SaddleBrown;
                            break;
                        case "Sienna":
                            someButton.Background = Brushes.Sienna;
                            break;
                        case "LimeGreen":
                            someButton.Background = Brushes.LimeGreen;
                            break;
                        case "Green":
                            someButton.Background = Brushes.Green;
                            break;
                        case "DarkGreen":
                            someButton.Background = Brushes.DarkGreen;
                            break;
                        case "Olive":
                            someButton.Background = Brushes.Olive;
                            break;
                        case "OliveDrab":
                            someButton.Background = Brushes.OliveDrab;
                            break;
                        case "DarkOliveGreen":
                            someButton.Background = Brushes.DarkOliveGreen;
                            break;
                        case "DarkSeaGreen":
                            someButton.Background = Brushes.DarkSeaGreen;
                            break;
                        default:
                            break;

                    }                   
                }
                else

                {
                    Image imgControl = new Image();
                    var bitmapImage = new BitmapImage(new Uri(pLookup.PictureSource));
                    imgControl.Source = bitmapImage;
                    someButton.Content = imgControl;

                    int col = Grid.GetColumn(someButton);
                    int row = Grid.GetRow(someButton);
                    
                    //TODO: Take the correct GardenID
                    Plant pp = new Plant() { GardenId = 4, PlantId = pLookup.PlantId, CoordinateX = col, CoordinateY = row};

                    seededPlants[col, row] = pp;
                }

            } 
        }

        private void RibbonButton_Plants(object sender, RoutedEventArgs e)
        {
            Button someButton = sender as Button;
            if (someButton != null)
            {
                isDrawingBeds = false;
                //SeedPlant = (string)someButton.Tag;

                int tag;
                bool res = int.TryParse((string)someButton.Tag, out tag);
                if (res == false)
                {
                    MessageBox.Show("Unable to fetch the plant from database", "Database error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                
                pLookup = db.GetPlantById(tag);
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

        private void RibbonButton_SaveGarden(object sender, RoutedEventArgs e)
        {
            for (int col = 0; col < 32; col++)
            {
                for(int row = 0; row < 20; row++)
                {
                    if (seededPlants[col, row] != null)
                    {
                        db.AddPlant(seededPlants[col, row]);
                    }
                }
            }
            MessageBox.Show("Garden is saved into database", "Save Garden", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RibbonButton_OpenGarden(object sender, RoutedEventArgs e)
        {
            for (int col = 0; col < 32; col++)
            {
                for (int row = 0; row < 20; row++)
                {
                    seededPlants[col, row] = null;
                }
            }
            List<Plant> plantsDB = new List<Plant>();
            
            //TODO: Take the correct GardenID
            plantsDB = db.GetAllPlants(4);
            foreach(Plant p in plantsDB)
            {
                Image imgControl = new Image();
                var bitmapImage = new BitmapImage(new Uri(p.PicSource));
                imgControl.Source = bitmapImage;
                var btn = grdGardenPlan.Children.Cast<Button>().First(eg => Grid.GetRow(eg) == p.CoordinateY && Grid.GetColumn(eg) == p.CoordinateX);
                btn.Content = imgControl;
                seededPlants[p.CoordinateX, p.CoordinateY] = p;
            }
        }
    }
}
