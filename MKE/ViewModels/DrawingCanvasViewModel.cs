using MKE.Commands;
using MKE.UIModels;
using MKE.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MKE.Services;

namespace MKE.ViewModels
{
    public class DrawingCanvasViewModel : INotifyPropertyChanged
    {
        public int GridSize { get; set; } = 20;

#region Obsrvable Collections
        public ObservableCollection<GridLine> VerticalLines { get; } = new ObservableCollection<GridLine>();

        public ObservableCollection<GridLine> HorizontalLines { get; } = new ObservableCollection<GridLine>();

        public ObservableCollection<Node> Nodes { get; } = new ObservableCollection<Node>();

        public ObservableCollection<Element> Elements { get; } = new ObservableCollection<Element>();
#endregion

#region Command Registration
        public ICommand AddNodeCommand { get; }

#endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DrawingCanvasViewModel(EventAggregator eventAggregator)
        {
            // Some mock data
            var node1 = new Node(50,50);
            var node2 = new Node(50,100);
            Nodes.Add(node1); Nodes.Add(node2);
        }

        public void UpdateGridLines(double width, double height)
        {
            VerticalLines.Clear();
            HorizontalLines.Clear();

            for (double i = 0; i <= width; i += GridSize)
            {
                VerticalLines.Add(new GridLine(i, 0, i, height));
            }


            for (double i = 0; i <= height; i += GridSize)
            {
                HorizontalLines.Add(new GridLine(0, i, width, i));
            }


            OnPropertyChanged(nameof(VerticalLines));
            OnPropertyChanged(nameof(HorizontalLines));
        }


        #region Command Implementations
        private void AddNode()
        {
            // Logic for adding a new node
            var newNode = new Node(100, 100);  // Example position
            Nodes.Add(newNode);
            OnPropertyChanged(nameof(Nodes));
        }

#endregion
    }
}