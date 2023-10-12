using System.Windows;
using System.Windows.Controls;
using MKE.ViewModels;

namespace MKE
{
    public partial class DrawingCanvasControl : UserControl
    {
        public DrawingCanvasControl()
        {
            InitializeComponent();
        }

        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as DrawingCanvasViewModel;
            viewModel?.UpdateGridLines(DrawingCanvas.ActualWidth, DrawingCanvas.ActualHeight);
        }

    }
}
