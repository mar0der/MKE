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
using System;

namespace MKE.ViewModels
{
    public class DrawingCanvasViewModel : INotifyPropertyChanged
    {
#region Private Fields
        private readonly EventAggregator _eventAggregator;
#endregion

#region Public properties
        public int GridSize { get; set; } = 20;
        public Point SnapPosition { get; set; }
        public int Height { get; set; } = 800;
        public int Width { get; set; } = 1200;
        public string StatusBarMessage { get; set; }

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

        public RelayCommand CanvasMouseMoveCommand { get; private set; }

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

            // Creates the command that listens for all clicks on the canvas and dispetches the actions based on flags
            CanvasClickCommand = new RelayCommand(OnCanvasClicked);
            CanvasMouseMoveCommand = new RelayCommand(OnCanvasMouseMove);

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

        public void OnCanvasMouseMove(object parameter)
        {
            if (parameter is MouseEventArgs args && IsNodeCreationModeActive)
            {
                Point mousePosition = args.GetPosition(null);
                Point snappedPosition = GetSnappedPosition(mousePosition);
                SnapPosition = snappedPosition;
                System.Diagnostics.Debug.WriteLine($"SnapPosition: {SnapPosition.X}, {SnapPosition.Y}");
                OnPropertyChanged(nameof(SnapPosition));
            }
        }

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
                    CreateNodeAtPosition(SnapPosition);
                    IsNodeCreationModeActive = false;
                    StatusBarMessage = string.Empty;
                    _eventAggregator.Publish(new StatusBarDataMessage
                    {
                        StatusMessage = StatusBarMessage,
                        Coordinate = new Point(0, 0)
                    });
                    Application.Current.MainWindow.Cursor = Cursors.Arrow;
                }
            }
        }

        /// <summary>
        /// Sets the flag that we are expecting to start adding nodes
        /// </summary>
        /// <param name="data"></param>
        private void OnEnterNodeCreationMode(EnterNodeCreationMode data)
        {
            IsNodeCreationModeActive = true;
            StatusBarMessage = "Please select a point to insert a node:";
            Application.Current.MainWindow.Cursor = Cursors.Cross;
        }

        /// <summary>
        /// Gets executed when the click event is triggered on the canvas and the flag is being set for adding nodes
        /// </summary>
        /// <param name="position"></param>
        private void CreateNodeAtPosition(Point position)
        {
            Node newNode = new Node(position.X, position.Y);
            Nodes.Add(newNode);
            OnPropertyChanged(nameof(Nodes));
        }

        private Point GetSnappedPosition(Point originalPosition)
        {
            double correctedY = originalPosition.Y - 60;

            double snappedX = Math.Round(originalPosition.X / GridSize) * GridSize;
            double snappedY = Math.Round(correctedY / GridSize) * GridSize;

            System.Diagnostics.Debug.WriteLine($"Original positions: {originalPosition.X}, {originalPosition.Y}");
            System.Diagnostics.Debug.WriteLine($"Corrected positions: {originalPosition.X}, {correctedY}");

            var snappedPoint = new Point( snappedX, snappedY );

            _eventAggregator.Publish(new StatusBarDataMessage
            {
                StatusMessage = StatusBarMessage,
                Coordinate = snappedPoint 
            });

            return new Point(snappedX, snappedY);
        }
        #endregion
    }
}