using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace WpfPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random;
        private bool isDrawing = false;
        private Shape DrawingShape;
        private Point StartPoint;
        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
            Mouse.Capture(myCanvas);
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            AddRandomShapes();
        }

        private void AddRandomShapes()
        {
            for (int i= 0; i < 4; i++)
            {
                Shape shape;
                if (random.Next(2) == 0)
                    shape = new Rectangle();
                else
                    shape = new Ellipse();

                shape.Width = random.Next(50, 200);
                shape.Height = random.Next(50, 200);
                shape.Fill = new SolidColorBrush(RandomColor());

                shape.MouseEnter += Shape_MouseEnter;
                shape.MouseLeave += Shape_MouseLeave;
                shape.MouseRightButtonDown += Shape_MouseRightButtonDown;

                Canvas.SetLeft(shape, random.Next((int)myCanvas.ActualWidth - (int)shape.Width));
                Canvas.SetTop(shape, random.Next((int)myCanvas.ActualHeight - (int)shape.Height));

                myCanvas.Children.Add(shape);
            }
        }

        private Color RandomColor()
        {
            byte r = (byte)random.Next(256);
            byte g = (byte)random.Next(256);
            byte b = (byte)random.Next(256);

            return Color.FromRgb(r, g, b);
        }

        private void Shape_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Shape_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void Shape_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape selectedShape = sender as Shape;

            if (selectedShape != null)
            {
                if (selectedShape.Stroke == Brushes.Red)
                {
                    selectedShape.Stroke = Brushes.Transparent;
                    selectedShape.Effect = null;
                }
                else
                {
                    selectedShape.Stroke = Brushes.Red;
                    ApplyGlowEffect(selectedShape);
                    BringShapeToFront(selectedShape);
                }
            }
        }

        private void ApplyGlowEffect(Shape shape)
        {
            DropShadowEffect glowEffect = new DropShadowEffect();
            glowEffect.Color = Colors.White;
            glowEffect.BlurRadius = 50;
            glowEffect.ShadowDepth = 0;
            glowEffect.Direction = 270;
            glowEffect.Opacity = 1;

            shape.Effect = glowEffect;
        }

        private void BringShapeToFront(Shape shape)
        {
            int maxZIndex = int.MinValue;

            foreach (UIElement element in myCanvas.Children)
            {
                int zIndex = Canvas.GetZIndex(element);
                if (zIndex > maxZIndex)
                    maxZIndex = zIndex;
            }

            Canvas.SetZIndex(shape, maxZIndex + 1);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            List<Shape> shapesToRemove = new List<Shape>();
            
            foreach (UIElement element in myCanvas.Children)
            {
                if (element is Shape shape && shape.Stroke == Brushes.Red)
                {
                    shapesToRemove.Add(shape);
                }
            }
            foreach (Shape shape in  shapesToRemove)
            {
                myCanvas.Children.Remove(shape);
            }
        }

        private void RandomColors_Click(object sender, RoutedEventArgs e)
        {
            List<Shape> shapesToChangeColor = new List<Shape>();

            foreach (UIElement element in myCanvas.Children)
            {
                if (element is Shape shape && shape.Stroke == Brushes.Red)
                {
                    shapesToChangeColor.Add(shape);
                }
            }

            foreach(Shape shape in shapesToChangeColor)
            {
                shape.Fill = new SolidColorBrush(RandomColor());
            }
        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && DrawingShape != null)
            {
                Point currentPoint = e.GetPosition(myCanvas);
                double width = Math.Abs(currentPoint.X - StartPoint.X);
                double height = Math.Abs(currentPoint.Y - StartPoint.Y);

                if (DrawingShape is Rectangle rectangle)
                {
                    rectangle.Width = width;
                    rectangle.Height = height;
                }
                else if (DrawingShape is Ellipse ellipse)
                {
                    ellipse.Width = width;
                    ellipse.Height = height;
                }

                Canvas.SetLeft(DrawingShape, Math.Min(StartPoint.X, currentPoint.X));
                Canvas.SetTop(DrawingShape, Math.Min(StartPoint.Y, currentPoint.Y));
            }
        }

        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DrawingShape != null)
            {
                StartPoint = e.GetPosition(myCanvas);
                isDrawing = true;
                DrawingShape.Fill = new SolidColorBrush(RandomColor());
            }
        }

        private void myCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                Mouse.OverrideCursor = null;
                DrawingShape = null;
                Mouse.Capture(null);
            }
        }

        private void myCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isDrawing && DrawingShape != null)
            {
                Mouse.Capture(null);
            }
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            DrawingShape = new Rectangle();
            DrawingShape.MouseEnter += Shape_MouseEnter;
            DrawingShape.MouseLeave += Shape_MouseLeave;
            DrawingShape.MouseRightButtonDown += Shape_MouseRightButtonDown;
            Mouse.Capture(myCanvas);
            myCanvas.Children.Add(DrawingShape);
        }

        private void Ellipse_Click(object sender, RoutedEventArgs e)
        {
            DrawingShape = new Ellipse();
            DrawingShape.MouseEnter += Shape_MouseEnter;
            DrawingShape.MouseLeave += Shape_MouseLeave;
            DrawingShape.MouseRightButtonDown += Shape_MouseRightButtonDown;
            Mouse.Capture(myCanvas);
            myCanvas.Children.Add(DrawingShape);
        }

        private void myCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (DrawingShape != null)
            {
                Mouse.OverrideCursor = Cursors.Cross;
            }
        }
    }
}
