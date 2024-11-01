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
    public partial class MainWindow: Window
    {
        public Point startPoint;
        private Resources resources;
        private Shape shape;
        public Brush selectedColor = Brushes.Black;
        public Brush selectedBackground = Brushes.Transparent;
        public DoubleCollection selectedPattern = new DoubleCollection();
        public double slectedThickness = 2;
        private double rotation = 0;
        private double width, height;
        private bool isDragging = false;

        public MainWindow()
        {
            InitializeComponent();
            resources = new Resources();
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
            shape = null;
            resources.isDrawing = true;
            resources.typeShape = "square";
        }
        private void btn_circle_Click(object sender, RoutedEventArgs e)
        {
            shape = null;
            resources.isDrawing = true;
            resources.typeShape = "circle";
        }
        private void btn_triangle_Click(object sender, RoutedEventArgs e)
        {
            shape = null;
            resources.isDrawing = true;
            resources.typeShape = "triangle";
        }
        private void btn_line_Click(object sender, RoutedEventArgs e)
        {
            shape = null;
            resources.isDrawing = true;
            resources.typeShape = "line";
        }


        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (resources.isDrawing)
            {
                startPoint = e.GetPosition(canvas);
                shape = resources.ShapeFactory(selectedColor, slectedThickness, selectedPattern, selectedBackground, startPoint);
                canvas.Children.Add(shape);
                Canvas.SetLeft(shape, startPoint.X);
                Canvas.SetTop(shape, startPoint.Y);

                canvas.CaptureMouse();
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (resources.isDrawing && shape != null)
            {
                Point currentPosition = e.GetPosition(canvas);

                width = Math.Abs(currentPosition.X - startPoint.X);
                height = Math.Abs(currentPosition.Y - startPoint.Y);

                shape.Width = width;
                shape.Height = height;

                Canvas.SetLeft(shape, Math.Min(currentPosition.X, startPoint.X));
                Canvas.SetTop(shape, Math.Min(currentPosition.Y, startPoint.Y));

                
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (resources.isDrawing)
            {
                resources.isDrawing = false;
                rotation = 0;
                canvas.ReleaseMouseCapture();
            }
        }

        private void btn_pencil_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_clrBlack_Click(object sender, RoutedEventArgs e)
        {
            shape.Stroke = Brushes.Black;
        }

        private void btn_clrRed_Click(object sender, RoutedEventArgs e)
        {
            shape.Stroke = Brushes.Red;
        }

        private void btn_clrBlue_Click(object sender, RoutedEventArgs e)
        {
            shape.Stroke = Brushes.Blue;
        }

        private void btn_clrGreen_Click(object sender, RoutedEventArgs e)
        {
            shape.Stroke = Brushes.Green;
        }

        private void btn_dotLne_Click(object sender, RoutedEventArgs e)
        {
            shape.StrokeDashArray = new DoubleCollection { 1, 2 };
        }

        private void btn_lineStroke_Click(object sender, RoutedEventArgs e)
        {
            shape.StrokeDashArray = new DoubleCollection { 4, 2 };
        }

        private void btn_convStroke_Click(object sender, RoutedEventArgs e)
        {
            shape.StrokeDashArray = new DoubleCollection { 4, 2, 1, 2 };
        }

        private void btn_clean_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private void sld_stroke_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slectedThickness = e.NewValue;
            if (shape != null)
            {
                shape.StrokeThickness = slectedThickness;
            }
        }

        private void btn_clrBlackBackground_Click(object sender, RoutedEventArgs e)
        {
            shape.Fill = Brushes.Black;
        }

        private void btn_clrRedBackground_Click(object sender, RoutedEventArgs e)
        {
            shape.Fill = Brushes.Red;
        }

        private void btn_clrBlueBackground_Click(object sender, RoutedEventArgs e)
        {
            shape.Fill = Brushes.Blue;
        }

        private void btn_clrGreenBackground_Click(object sender, RoutedEventArgs e)
        {
            shape.Fill = Brushes.Green;
        }

        private void btn_translation_Click(object sender, RoutedEventArgs e)
        {
            shape.MouseLeftButtonDown += Shape_MouseLeftButtonDown;
            shape.MouseMove += Shape_MouseMove;
            shape.MouseLeftButtonUp += Shape_MouseLeftButtonUp;
        }

        private void Shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(canvas);  // Obtener posición inicial
            shape.CaptureMouse();  // Capturar el mouse
        }

        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(canvas);

                // Calcular el desplazamiento
                double offsetX = currentPoint.X - startPoint.X;
                double offsetY = currentPoint.Y - startPoint.Y;

                // Actualizar la posición del rectángulo en el Canvas
                double newLeft = Canvas.GetLeft(shape) + offsetX;
                double newTop = Canvas.GetTop(shape) + offsetY;

                Canvas.SetLeft(shape, newLeft);
                Canvas.SetTop(shape, newTop);

                startPoint = currentPoint;  // Actualizar la posición inicial
            }
        }

        // Evento al soltar el botón izquierdo del mouse
        private void Shape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            shape.ReleaseMouseCapture();  // Liberar el mouse
        }

        private void btn_scaled_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_rotation_Click(object sender, RoutedEventArgs e)
        {
            rotation += 10;
            RotateTransform rotateTransform = new RotateTransform
            {
                Angle = rotation,
                CenterX = width / 2,
                CenterY = height / 2
            };

            shape.RenderTransform = rotateTransform;
        }

        private void btn_skewed_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
