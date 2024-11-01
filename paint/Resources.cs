using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace paint
{
    internal class Resources
    {
        private Rectangle currentRectangle;
        private Polygon currentTriangle;
        private Ellipse currentElipse;
        private Line currentLine;
        public string typeShape = "";
        public bool isDrawing = false;

        public Shape ShapeFactory(Brush selectedColor, double slectedThickness, DoubleCollection selectedPattern, Brush selectedBackground, Point startPoint)
        {
            switch (typeShape)
            {
                case "square":
                    return CreateRectangle(selectedColor, slectedThickness, selectedPattern, selectedBackground);
                case "circle":
                    return CreateEllipse(selectedColor, slectedThickness, selectedPattern, selectedBackground);
                case "triangle":
                    return CreatePolygon(selectedColor, slectedThickness, selectedPattern, selectedBackground, startPoint);
                case "line":
                    return CreateLine(selectedColor, slectedThickness, selectedPattern, selectedBackground, startPoint);
                default:
                    throw new ArgumentException("Tipo de forma no válido");
            }
        }

        public void ClearShapesData()
        {
            currentRectangle = null;
            currentElipse = null;
            currentTriangle = null;
            currentLine = null;
            isDrawing = true;
        }

        private Rectangle CreateRectangle(Brush selectedColor, double slectedThickness, DoubleCollection selectedPattern, Brush selectedBackground)
        {
            return new Rectangle
            {
                Stroke = selectedColor,
                StrokeThickness = slectedThickness,
                StrokeDashArray = selectedPattern,
                Fill = selectedBackground
            };
        }
        private Ellipse CreateEllipse(Brush selectedColor, double slectedThickness, DoubleCollection selectedPattern, Brush selectedBackground)
        {
            return new Ellipse
            {
                Stroke = selectedColor,
                StrokeThickness = slectedThickness,
                StrokeDashArray = selectedPattern,
                Fill = selectedBackground
            };
        }
        private Polygon CreatePolygon(Brush selectedColor, double slectedThickness, DoubleCollection selectedPattern, Brush selectedBackground, Point startPoint)
        {

            Polygon triangle = new Polygon
            {
                Stroke = selectedColor,
                StrokeThickness = slectedThickness,
                StrokeDashArray = selectedPattern,
                Fill = selectedBackground,
                Points = new PointCollection
                {
                    new Point(startPoint.X, startPoint.Y),
                    new Point(startPoint.X, startPoint.Y),
                    new Point(startPoint.X, startPoint.Y)
                }
            };
            return triangle;
        }
        private Line CreateLine(Brush selectedColor, double slectedThickness, DoubleCollection selectedPattern, Brush selectedBackground, Point startPoint)
        {
            return new Line
            {
                Stroke = selectedColor,
                StrokeThickness = slectedThickness,
                StrokeDashArray = selectedPattern,
                X1 = startPoint.X,
                Y1 = startPoint.Y,
                X2 = startPoint.X,
                Y2 = startPoint.Y
            };
        }
    }
}
