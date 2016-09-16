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
    public partial class MainWindow : Window
    {
        Database db = new Database();

        Plant[,] seededPlants = new Plant[32,20];
        PlantsLookup pLookup = new PlantsLookup();

        bool isDrawingBeds = false;

        string BedColor;
        string SeedPlant;
        string Companions;

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

        private void RibbonButton_Companions(object sender, RoutedEventArgs e)
        {
            Button someButton = sender as Button;
            if (someButton != null)
            {
                Companions = (string)someButton.Tag;

                switch (Companions)
                {
                    case "Asparagus":
                        MessageBox.Show("Basil, Nasturtium, Parsley and Tomato.", "Asparagus companion plants");
                        break;
                    case "Bean Bush":
                        MessageBox.Show("Beet, Cabbage, Carrot, Cauliflower, Celeriac, Celery, Chard, Corn, Cucumber, Eggplant, Leek, Marigold, Parsnip, Pea, Potato, Radish, Rosemary, Strawberry and Sunflower.", "Bush Bean companion plants");
                        break;
                    case "Bean Pole":
                        MessageBox.Show("Carrot, Cauliflower, Chard, Corn, Cucumber, Eggplant, Marigold, Pea, Potato, Rosemary and Strawberry.", "Pole Bean companion plants");
                        break;
                    case "Beet":
                        MessageBox.Show("Bush Bean, Broccoli, Brussels Sprouts, Cabbage, Cauliflower, Corn, Cress, Horseradish, Kale, Kohlrabi, Leek, Lettuce, Lima Bean, Mizuna, Mustard, Onion, Pak Choi, Radish, Rutabaga, Radish and Turnip.", "Beet companion plants");
                        break;
                    case "Broccoli":
                        MessageBox.Show("Asparagus, Beet, Bush Bean, Carrot, Celery, Chard, Chive, Cucumber, Dill, Garlic, Kale, Leek, Lettuce, Mint, Nasturtium, Onion, Oregano, Potato, Rosemary, Sage, Scallion, Spinach and Tomato.", "Broccoli companion plants");
                        break;
                    case "Brussels Sprouts":
                        MessageBox.Show("Asparagus, Beet, Bush Bean, Carrot, Celery, Chive, Cucumber, Garlic, Leek, Lettuce, Nasturtium, Onion, Pea, Potato, Radish, Scallion, Spinach and Tomato.", "Brussels Sprout companion plants");
                        break;
                    case "Cabbage":
                        MessageBox.Show("Asparagus, Beet, Bush Bean, Carrot, Celery, Chive, Cucumber, Dill, Garlic, Kale, Leek, Lettuce, Mint, Nasturtium, Onion, Potato, Rosemary, Sage, Scallion, Spinach, Thyme and Tomato.", "Cabbage companion plants");
                        break;
                    case "Carrot":
                        MessageBox.Show("Bean, Brussels Sprouts, Cabbage, Chive, leaf Lettuce, Leek, Onion, Pea, Pepper, red Radish, Rosemary and Sage.", "Carrot companion plants");
                        break;
                    case "Cauliflower":
                        MessageBox.Show("Asparagus, Beet, Bush Bean, Carrot, Celery, Chive, Cucumber, Dill, Garlic, Kale, Leek, Lettuce, Mint, Nasturtium, Onion, Potato, Rosemary, Sage, Scallion, Spinach and Tomato.", "Cauliflower companion plants");
                        break;
                    case "Celery":
                        MessageBox.Show("Bush Bean, Broccoli, Brussels Sprouts, Cabbage, Cauliflower, Cress, Horseradish, Kale, Kohlrabi, Leek, Mizuna, Mustard, Pak Choi, Parsley, Pea, Radish, Rutabaga, Tomato and Turnip.", "Celery companion plants");
                        break;
                    case "Chives":
                        MessageBox.Show("Beet, Broccoli, Brussels Sprouts, Cabbage, Carrot, Cauliflower, Cress, Horseradish, Kale, Kohlrabi, Leek, early Lettuce, Mizuna, Mustard, Pak Choi, Parsnip, Pepper, Radish, Rutabaga, Spinach, Strawberry, Tomato and Turnip.", "Chive companion plants");
                        break;
                    case "Corn":
                        MessageBox.Show("Beet, Bush Bean, Cabbage, Cucumber, Melon, Morning Glory, Parsley, Pumpkin and Squash.", "Corn companion plants");
                        break;
                    case "Cucumber":
                        MessageBox.Show("Bush Bean, Broccoli, Brussels Sprouts, Cabbage, Cauliflower, Corn, Cress, Dill, Eggplant, Horseradish, Kale, Kohlrabi, Lettuce, Mizuna, Mustard, Nasturtium, Pak Choi, Pea, Radish, Rutabaga, Sunflower, Tomato and Turnip.", "Cucumber companion plants");
                        break;
                    case "Eggplant":
                        MessageBox.Show("Bush Bean, Pea, Pepper and Potato.", "Eggplant companion plants");
                        break;
                    case "Garlic":
                        MessageBox.Show("Beet, Broccoli, Brussels Sprouts, Cabbage, Carrot, Cauliflower, Cress, Horseradish, Kale, Kohlrabi, Leek, early Lettuce, Mizuna, Mustard, Pak Choi, Parsnip, Pepper, Radish, Rutabaga, Spinach, Strawberry, Tomato and Turnip.", "Garlic companion plants");
                        break;
                    case "Kale":
                        MessageBox.Show("Beet, Bush Bean, Cabbage, Celery, Cucumber, Lettuce, Nasturtium, Onion, Potato, Spinach and Tomato.", "Kale companion plants");
                        break;
                    case "Kohlrabi":
                        MessageBox.Show("Beet, Bush Bean, Celery, Cucumber, Lettuce, Nasturtium, Onion, Potato and Tomato.", "Kohlrabi companion plants");
                        break;
                    case "Leek":
                        MessageBox.Show("Beet, Bush Bean, Carrot, Celeriac, Celery, Onion, Parsley and Tomato.", "Leek companion plants");
                        break;
                    case "Lettuce":
                        MessageBox.Show("Everything, but especially Asparagus, Carrot, Chive, Garlic, Leek, Onion, Scallion and Radish.", "Lettuce companion plants");
                        break;
                    case "Lima Bean":
                        MessageBox.Show("Beet and Radish.", "Lima Bean companion plants");
                        break;
                    case "Melon":
                        MessageBox.Show("Corn.", "Melon companion plant");
                        break;
                    case "Onion":
                        MessageBox.Show("Beet, Broccoli, Brussels Sprouts, Cabbage, Carrot, Cauliflower, Cress, Horseradish, Kale, Kohlrabi, Leek, early Lettuce, Mizuna, Mustard, Pak Choi, Parsnip, Pepper, Radish, Rutabaga, Spinach, Strawberry, Tomato and Turnip.", "Onion companion plants");
                        break;
                    case "Parsley":
                        MessageBox.Show("Asparagus, Corn and Tomato.", "Parsley companion plants");
                        break;
                    case "Parsnip":
                        MessageBox.Show("Bush Bean, Garlic, Onion, Pea, Pepper, Potato and Radish.", "Parsnip companion plants");
                        break;
                    case "Peas":
                        MessageBox.Show("Bean, Carrot, Celery, Chicory, Corn, Cucumber, Eggplant, Parsley, Radish, Spinach, Strawberry, sweet Pepper and Turnip.", "Pea companion plants");
                        break;
                    case "Sweet Pepper":
                        MessageBox.Show("Carrot, Eggplant, Onion, Parsnip, Pea and Tomato.", "Pepper companion plants");
                        break;
                    case "Potato":
                        MessageBox.Show("Bush Bean, Broccoli, Brussels Sprouts, Cabbage, Cauliflower, Corn, Cress, Eggplant (as trap crop), Horseradish, Kale, Kohlrabi, Marigold, Mizuna, Mustard, Pak Choi, Parsnip, Pea, Radish, Rutabaga and Turnip.", "Potato companion plants");
                        break;
                    case "Pumpkin":
                        MessageBox.Show("Corn, Eggplant, Nasturtium and Radish.", "Pumpkin companion plants");
                        break;
                    case "Radish":
                        MessageBox.Show("Bean, Beet, Broccoli, Brussels Sprouts, Cabbage, Carrot, Cauliflower, Chervil, Corn, Cucumber, Cress, Horseradish, Kale, Kohlrabi, leaf Lettuce, Melon, Mizuna, Mustard, Nasturtium, Pak Choi, Parsnip, Pea, Pumpkin, Radish, Rutabaga, Spinach, Squash, Sweet Potato, Tomato, Turnip and Zucchini.", "Radish companion plants");
                        break;
                    case "Rutabaga":
                        MessageBox.Show("Asparagus, Chive, Garlic, Leek, Nasturtium, Onion, Scallion and Pea.", "Rutabaga companion plants");
                        break;
                    case "Scallion":
                        MessageBox.Show("Beet, Broccoli, Brussels Sprouts, Cabbage, Carrot, Cauliflower, Cress, Horseradish, Kale, Kohlrabi, Leek, early Lettuce, Mizuna, Mustard, Pak Choi, Parsnip, Pepper, Radish, Rutabaga, Spinach, Strawberry, Tomato and Turnip.", "Scallion companion plants");
                        break;
                    case "Spinach":
                        MessageBox.Show("Broccoli, Brussels Sprouts, Cabbage, Cauliflower, Celery, Cress, Horseradish, Kale, Kohlrabi, Legumes, Lettuce, Mizuna, Mustard, Onion, Pak Choi, Pea, Radish, Rutabaga, Strawberry and Turnip.", "Spinach companion plants");
                        break;
                    case "Squash":
                        MessageBox.Show("Celeriac, Celery, Corn, Dill, Melon, Nasturtium, Onion and Radish.", "Squash companion plants");
                        break;
                    case "Strawberry":
                        MessageBox.Show("Bean, Borage, Lettuce, Onion, Pea and Spinach.", "Strawberry companion plants");
                        break;
                    case "Tomato":
                        MessageBox.Show("Asparagus, Basil, Bee Balm, Bush Bean, Broccoli, Brussels Sprouts, Cabbage, Cauliflower, Celery, Chive, Cress, Cucumber, Garlic, Horseradish, Kale, Kohlrabi, head Lettuce, Marigold, Mint, Mizuna, Mustard, Nasturtium, Onion, Pak Choi, Parsley, Pepper, Radish, Rutabaga and Turnip.", "Tomato companion plants");
                        break;
                    case "Turnip":
                        MessageBox.Show("Asparagus, Chive, Garlic, Leek, Onion, Scallion and Pea.", "Turnip companion plants");
                        break;
                }
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
