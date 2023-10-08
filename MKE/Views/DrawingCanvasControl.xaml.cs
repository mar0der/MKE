using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MKE
{
    public partial class DrawingCanvasControl : UserControl
    {

        public int GridSize { get; set; } = 50;  // Default value

        // Field to store whether snapping is enabled
        private bool _isSnappingEnabled;

        // Field to store the callback action when a point is picked
        private Action<Point?> _pointPickedCallback;

        private Rectangle _snappingIndicator;

        public DrawingCanvasControl()
        {
            InitializeComponent();
            DrawingCanvas.Loaded += OnControlLoaded;
        }

        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            DrawGridLines(GridSize);
            AddNode(200, 200);  // Example coordinates
        }

        public void DrawGridLines(int gridSize)
        {
            double width = DrawingCanvas.ActualWidth;
            double height = DrawingCanvas.ActualHeight;

            for (double i = 0; i <= width; i += gridSize)
            {
                Line verticalLine = new Line
                {
                    X1 = i,
                    Y1 = 0,
                    X2 = i,
                    Y2 = height,
                    Style = (Style)DrawingCanvas.Resources["GridLineStyle"]
                };
                DrawingCanvas.Children.Add(verticalLine);
            }

            for (double i = 0; i <= height; i += gridSize)
            {
                Line horizontalLine = new Line
                {
                    X1 = 0,
                    Y1 = i,
                    X2 = width,
                    Y2 = i,
                    Style = (Style)DrawingCanvas.Resources["GridLineStyle"]
                };
                DrawingCanvas.Children.Add(horizontalLine);
            }
        }

        private void AddNode(double x, double y)
        {
            Ellipse node = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(node, x - node.Width / 2);  // Center the node at the specified coordinates
            Canvas.SetTop(node, y - node.Height / 2);  // Center the node at the specified coordinates
            DrawingCanvas.Children.Add(node);
        }

        public void PickPoint(bool isSnappingEnabled, Action<Point?> pointPickedCallback)
        {
            _isSnappingEnabled = isSnappingEnabled;
            _pointPickedCallback = pointPickedCallback;

            // Change the cursor to a cross when over the canvas
            DrawingCanvas.Cursor = Cursors.Cross;

            // Attach event handlers
            DrawingCanvas.MouseLeftButtonDown += OnCanvasMouseLeftButtonDown;
            DrawingCanvas.PreviewKeyDown += OnCanvasPreviewKeyDown;
            if (isSnappingEnabled)
            {
                DrawingCanvas.MouseMove += OnCanvasMouseMove;
            }
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(DrawingCanvas);
            point = new Point(Math.Round(point.X / GridSize) * GridSize, Math.Round(point.Y / GridSize) * GridSize);
            ShowSnappingIndicator(point);
        }

        private void OnCanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(DrawingCanvas);

            if (_isSnappingEnabled)
            {
                // Snap the point to the nearest grid intersection
                point = new Point(Math.Round(point.X / 100) * 100, Math.Round(point.Y / 100) * 100);
            }

            // Detach event handlers
            DetachEventHandlers();

            // Invoke the callback with the picked point
            _pointPickedCallback?.Invoke(point);
            HideSnappingIndicator();
        }

        private void OnCanvasPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                // Detach event handlers
                DetachEventHandlers();

                // Invoke the callback with null to indicate cancellation
                _pointPickedCallback?.Invoke(null);
            }
        }

        private void DetachEventHandlers()
        {
            DrawingCanvas.MouseLeftButtonDown -= OnCanvasMouseLeftButtonDown;
            DrawingCanvas.PreviewKeyDown -= OnCanvasPreviewKeyDown;

            // Restore the cursor
            DrawingCanvas.Cursor = null;
            HideSnappingIndicator();
        }

        private void ShowSnappingIndicator(Point position)
        {
            if (_snappingIndicator == null)
            {
                _snappingIndicator = new Rectangle
                {
                    Width = 15,
                    Height = 15,
                    Stroke = Brushes.Yellow,
                    StrokeThickness = 1
                };
                DrawingCanvas.Children.Add(_snappingIndicator);
            }

            Canvas.SetLeft(_snappingIndicator, position.X - _snappingIndicator.Width / 2);
            Canvas.SetTop(_snappingIndicator, position.Y - _snappingIndicator.Height / 2);
        }

        private void HideSnappingIndicator()
        {
            if (_snappingIndicator != null)
            {
                DrawingCanvas.Children.Remove(_snappingIndicator);
                _snappingIndicator = null;
            }
        }

    }
}
