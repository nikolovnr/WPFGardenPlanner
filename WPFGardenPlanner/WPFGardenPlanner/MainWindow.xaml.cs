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
        /*
        private List<List<Point>> Polygons = new List<List<Point>>();
        private List<Point> NewPolygon = null;
        private Point NewPoint;
        */

        private Point startPoint;
        private Rectangle rectSelectArea;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void mnuExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //App.Current.Shutdown();
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            GardenSchema.Children.Add(new WPF.MDI.MdiChild() { Title = "Garden Shema", Background = System.Windows.Media.Brushes.Green, Content =  new Canvas() { Background = Brushes.Green } });
            
        }

        private void cnvGardenPlan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*
            // See if we are already drawing a polygon.
            if (NewPolygon != null)
            {
                // We are already drawing a polygon.
                // If it's the right mouse button, finish this polygon.
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    // Finish this polygon.
                    if (NewPolygon.Count > 2) Polygons.Add(NewPolygon);
                    NewPolygon = null;
                }
                else
                {
                    // Add a point to this polygon.
                    if (NewPolygon[NewPolygon.Count - 1] != e.GetPosition(cnvGardenPlan))
                    {
                        NewPolygon.Add(e.GetPosition(cnvGardenPlan));
                    }
                }
            }
            else
            {
                // Start a new polygon.
                NewPolygon = new List<Point>();
                NewPoint = e.GetPosition(cnvGardenPlan);
                NewPolygon.Add(e.GetPosition(cnvGardenPlan));
            }

            // Redraw.
            cnvGardenPlan.InvalidateVisual();
            */

            startPoint = e.GetPosition(cnvGardenPlan);

            // Remove the drawn rectanglke if any.
            // At a time only one rectangle should be there
            if (rectSelectArea != null)
                cnvGardenPlan.Children.Remove(rectSelectArea);

            // Initialize the rectangle.
            // Set border color and width
            rectSelectArea = new Rectangle
            {
                Stroke = Brushes.Brown,
                Fill = Brushes.Brown,
                StrokeThickness = 2
            };

            Canvas.SetLeft(rectSelectArea, startPoint.X);
            Canvas.SetTop(rectSelectArea, startPoint.X);
            cnvGardenPlan.Children.Add(rectSelectArea);

        }

        private void cnvGardenPlan_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            if (NewPolygon == null) return;
            NewPoint = e.GetPosition(cnvGardenPlan);
            cnvGardenPlan.InvalidateVisual();
            */

            if (e.LeftButton == MouseButtonState.Released || rectSelectArea == null)
                return;

            var pos = e.GetPosition(cnvGardenPlan);

            // Set the position of rectangle
            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            // Set the dimenssion of the rectangle
            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            rectSelectArea.Width = w;
            rectSelectArea.Height = h;

            Canvas.SetLeft(rectSelectArea, x);
            Canvas.SetTop(rectSelectArea, y);
        }

        private void cnvGardenPlan_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rectSelectArea = null;
        }

        private void RibbonButton_Click_1(object sender, RoutedEventArgs e)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(cnvGardenPlan);
            double dpi = 96d;


            RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);


            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(cnvGardenPlan);
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

        private void RibbonButton_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog dialog = new PrintDialog();

                if (dialog.ShowDialog() != true)
                    return;
                dialog.PrintVisual(cnvGardenPlan, "IFMS Print Screen");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Screen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
