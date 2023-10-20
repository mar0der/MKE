using MKE.Commands;
using MKE.UIModels;
using MKE.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MKE.Services;
using MKE.Models.Messages;
using System.Windows;

namespace MKE.ViewModels
{
    public class DrawingCanvasViewModel : INotifyPropertyChanged
    {
#region Private Fields
        private readonly EventAggregator _eventAggregator;
#endregion

#region Public properties
        public int GridSize { get; set; } = 20;
        public int Height { get; set; } = 800;
        public int Width { get; set; } = 1200;

        public bool IsNodeCreationModeActive { get; set; } = false;
 #endregion

#region Obsrvable Collections
        public ObservableCollection<GridLine> VerticalLines { get; } = new ObservableCollection<GridLine>();

        public ObservableCollection<GridLine> HorizontalLines { get; } = new ObservableCollection<GridLine>();

        public ObservableCollection<Node> Nodes { get; } = new ObservableCollection<Node>();

        public ObservableCollection<Element> Elements { get; } = new ObservableCollection<Element>();
#endregion

#region Command Registration
        public RelayCommand CanvasClickCommand { get; private set; }

        public ICommand AddNodeCommand { get; }

#endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DrawingCanvasViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<EnterNodeCreationMode>(OnEnterNodeCreationMode);

            CanvasClickCommand = new RelayCommand(OnCanvasClicked);
            // Some mock data
            var node1 = new Node(50,50);
            var node2 = new Node(50,100);
            Nodes.Add(node1); 
            Nodes.Add(node2);
            UpdateGridLines();
        }

        public void UpdateGridLines()
        {
            VerticalLines.Clear();
            HorizontalLines.Clear();

            for (double i = 0; i <= Width; i += GridSize)
            {
                VerticalLines.Add(new GridLine(i, 0, i, Height));
            }


            for (double i = 0; i <= Height; i += GridSize)
            {
                HorizontalLines.Add(new GridLine(0, i, Width, i));
            }


            OnPropertyChanged(nameof(VerticalLines));
            OnPropertyChanged(nameof(HorizontalLines));
        }


        #region Command Implementations
        /// <summary>
        /// Canvas click listener. calls other methods based on the activated mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnCanvasClicked(object parameter)
        {
            if (parameter is MouseButtonEventArgs args)
            {
                if (IsNodeCreationModeActive)
                {
                    Point clickPosition = args.GetPosition(null);
                    CreateNodeAtPosition(clickPosition);
                    IsNodeCreationModeActive = false; // Optionally, deactivate the mode after one node is added.
                }
            }
        }

            private void OnEnterNodeCreationMode(EnterNodeCreationMode data)
        {
            IsNodeCreationModeActive = true;
        }



        private void CreateNodeAtPosition(Point position)
        {
            Node newNode = new Node(position.X, position.Y);
            Nodes.Add(newNode);
            OnPropertyChanged(nameof(Nodes));
        }

        #endregion
    }
}