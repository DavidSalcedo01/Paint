using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace paint
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private string typeShape = "";
        private Point startPoint;
        private Rectangle currentRectangle;
        private Polygon currentTriangle;
        private Ellipse currentElipse;
        private Line currentLine;
        private Brush selectedColor = Brushes.Black;
        private DoubleCollection selectedPattern = new DoubleCollection();
        private double slectedThickness = 2; 

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btn_size_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btn_square_Click(object sender, RoutedEventArgs e)
        {
            isDrawing = true;
            typeShape = "square";
        }
        private void btn_circle_Click(object sender, RoutedEventArgs e)
        {
            isDrawing = true;
            typeShape = "circle";
        }
        private void btn_triangle_Click(object sender, RoutedEventArgs e)
        {
            isDrawing = true;
            typeShape = "triangle";
        }
        private void btn_line_Click(object sender, RoutedEventArgs e)
        {
            isDrawing = true;
            typeShape = "line";
        }


        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing)
            {
                startPoint = e.GetPosition(canvas);
                Debug.WriteLine($"posX: {startPoint.X}, posY: {startPoint.Y}");

                if (typeShape == "square")
                {
                    currentRectangle = new Rectangle
                    {
                        Stroke = selectedColor,
                        StrokeThickness = slectedThickness,
                        StrokeDashArray = selectedPattern,
                        Fill = Brushes.Transparent
                    };

                    canvas.Children.Add(currentRectangle);
                    Canvas.SetLeft(currentRectangle, startPoint.X);
                    Canvas.SetTop(currentRectangle, startPoint.Y);
                }
                if (typeShape == "circle")
                {
                    currentElipse = new Ellipse
                    {
                        Stroke = selectedColor,
                        StrokeThickness = slectedThickness,
                        StrokeDashArray = selectedPattern,
                        Fill = Brushes.Transparent
                    };

                    canvas.Children.Add(currentElipse);
                    Canvas.SetLeft(currentElipse, startPoint.X);
                    Canvas.SetTop(currentElipse, startPoint.Y);
                }
                if (typeShape == "triangle")
                {
                    currentTriangle = new Polygon
                    {
                        Stroke = selectedColor,
                        StrokeThickness = slectedThickness,
                        StrokeDashArray = selectedPattern,
                        Fill = Brushes.Transparent
                    };

                    PointCollection points = new PointCollection
                    {
                        new Point(startPoint.X, startPoint.Y),
                        new Point(startPoint.X, startPoint.Y),
                        new Point(startPoint.X, startPoint.Y)
                    };

                    currentTriangle.Points = points;
                    canvas.Children.Add(currentTriangle);
                }
                if (typeShape == "line")
                {
                    currentLine = new Line
                    {
                        Stroke = selectedColor,
                        StrokeThickness = slectedThickness,
                        StrokeDashArray = selectedPattern,
                        X1 = startPoint.X,
                        Y1 = startPoint.Y,
                        X2 = startPoint.X,
                        Y2 = startPoint.Y
                    };

                    canvas.Children.Add(currentLine);
                }

                canvas.CaptureMouse();
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && currentRectangle != null || isDrawing && currentElipse != null || isDrawing && currentTriangle != null || isDrawing && currentLine != null)
            {
                Point currentPosition = e.GetPosition(canvas);

                double width = Math.Abs(currentPosition.X - startPoint.X);
                double height = Math.Abs(currentPosition.Y - startPoint.Y);

                if (typeShape == "square")
                {
                    currentRectangle.Width = width;
                    currentRectangle.Height = height;

                    Canvas.SetLeft(currentRectangle, Math.Min(currentPosition.X, startPoint.X));
                    Canvas.SetTop(currentRectangle, Math.Min(currentPosition.Y, startPoint.Y));
                }
                else if (typeShape == "circle")
                {
                    currentElipse.Width = width;
                    currentElipse.Height = height;

                    Canvas.SetLeft(currentElipse, Math.Min(currentPosition.X, startPoint.X));
                    Canvas.SetTop(currentElipse, Math.Min(currentPosition.Y, startPoint.Y));
                }
                else if (typeShape == "triangle")
                {
                    PointCollection points = new PointCollection
                    {
                        new Point(width/2,  Math.Max(currentPosition.Y, startPoint.Y)),
                        new Point(Math.Max(currentPosition.X, startPoint.X), Math.Max(currentPosition.Y, startPoint.Y)),
                        new Point(Math.Min(currentPosition.X, startPoint.X), Math.Min(currentPosition.Y, startPoint.Y))
                    };

                    currentTriangle.Points = points;
                }
                else if (typeShape == "line")
                {
                    currentLine.X2 = currentPosition.X;
                    currentLine.Y2 = currentPosition.Y;

                }
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                if (typeShape == "square")
                {
                    currentRectangle = null;
                }
                else if (typeShape == "circle")
                {
                    currentElipse = null;
                }
                else if (typeShape == "triangle")
                {
                    currentTriangle = null;
                }
                else if (typeShape == "line")
                {
                    currentLine = null;
                }
                canvas.ReleaseMouseCapture();
            }
        }

        private void btn_pencil_Click(object sender, RoutedEventArgs e)
        {
            typeShape = "pencil";
            Debug.WriteLine("pencil");
        }

        private void btn_clrBlack_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = Brushes.Black;
        }

        private void btn_clrRed_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = Brushes.Red;
        }

        private void btn_clrBlue_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = Brushes.Blue;
        }

        private void btn_clrGreen_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = Brushes.Green;
        }

        private void btn_dotLne_Click(object sender, RoutedEventArgs e)
        {
            selectedPattern = new DoubleCollection { 1, 2 };
        }

        private void btn_lineStroke_Click(object sender, RoutedEventArgs e)
        {
            selectedPattern = new DoubleCollection { 4, 2 };
        }

        private void btn_convStroke_Click(object sender, RoutedEventArgs e)
        {
            selectedPattern = new DoubleCollection { 4, 2, 1, 2 };
        }

        private void btn_clean_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private void sld_stroke_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slectedThickness = e.NewValue;
        }
    }
}
