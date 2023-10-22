using MKE.Commands;
using MKE.Models;
using MKE.Models.Messages;
using MKE.Services;
using MKE.UIModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

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

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Construtors
        public DrawingCanvasViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
            // Event Subscriptions
            _eventAggregator.Subscribe<EnterNodeCreationModeMessage>(OnEnterNodeCreationModeMessage);
            _eventAggregator.Subscribe<DatabaseUpdatedMessage>(OnDatabaseUpdatedMessage);

            // Creates the command that listens for all clicks on the canvas and dispetches the actions based on flags
            CanvasClickCommand = new RelayCommand(OnCanvasClicked);
            CanvasMouseMoveCommand = new RelayCommand(OnCanvasMouseMove);

            UpdateGridLines();
        }

        #endregion

        #region Command Implementations
        /// <summary>
        /// Handles the mouse move event on the canvas. When the Node Creation mode is active, 
        /// it computes the snapped position based on the current mouse position and updates 
        /// the SnapPosition property. The SnapPosition is used to guide the user by showing 
        /// where the node will be placed on the grid when they click.
        /// </summary>
        /// <param name="parameter"></param>
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
                    SnapPosition = new Point(-100, -100);
                    OnPropertyChanged(nameof(IsNodeCreationModeActive));
                    OnPropertyChanged(nameof(SnapPosition));
                    Application.Current.MainWindow.Cursor = Cursors.Arrow;
                }
            }
        }
        #endregion

        #region Messages Subscriptions
        /// <summary>
        /// Handles the mouse click event on the canvas. If the Node Creation mode is active, 
        /// it creates a new node at the snapped position determined by the most recent mouse move event.
        /// After the node is created, it resets the mode, updates the status bar message, and 
        /// changes the cursor back to its default state.
        /// </summary>
        /// <param name="data"></param>
        private void OnEnterNodeCreationModeMessage(EnterNodeCreationModeMessage data)
        {
            IsNodeCreationModeActive = true;
            StatusBarMessage = "Please select a point to insert a node:";
            Application.Current.MainWindow.Cursor = Cursors.Cross;
            OnPropertyChanged(nameof(IsNodeCreationModeActive));
        }

        /// <summary>
        /// Gets message from the database do update the database because it has been changed
        /// </summary>
        /// <param name="message"></param>
        private void OnDatabaseUpdatedMessage(DatabaseUpdatedMessage message)
        {
            // Assuming DatabaseService provides a method to get all nodes
            var nodesFromDb = FEMDatabaseService.Instance.GetAllNodes();
            Nodes.Clear();
            foreach (var node in nodesFromDb)
            {
                Nodes.Add(node);
            }
        }
        #endregion

        #region Other private methods
        /// <summary>
        /// Updates the grid lines for the canvas. Clears the existing lines and creates new ones based on
        /// the current Width, Height, and GridSize properties.
        /// </summary>
        private void UpdateGridLines()
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

        /// <summary>
        /// Gets executed when the click event is triggered on the canvas and the flag is being set for adding nodes
        /// </summary>
        /// <param name="position"></param>
        private void CreateNodeAtPosition(Point position)
        {
            Node newNode = new Node(position.X, position.Y);
            Nodes.Add(newNode);
            FEMDatabaseService.Instance.AddNode(newNode);
            OnPropertyChanged(nameof(Nodes));
        }

        /// <summary>
        /// Handles the mouse move event on the canvas. When the Node Creation mode is active, 
        /// it computes the snapped position based on the current mouse position and updates 
        /// the SnapPosition property. The SnapPosition is used to guide the user by showing 
        /// where the node will be placed on the grid when they click.
        /// </summary>
        /// <param name="parameter">The MouseEventArgs that contains the event data.</param>
        private Point GetSnappedPosition(Point originalPosition)
        {
            double correctedY = originalPosition.Y - 60;

            double snappedX = Math.Round(originalPosition.X / GridSize) * GridSize;
            double snappedY = Math.Round(correctedY / GridSize) * GridSize;

            System.Diagnostics.Debug.WriteLine($"Original positions: {originalPosition.X}, {originalPosition.Y}");
            System.Diagnostics.Debug.WriteLine($"Corrected positions: {originalPosition.X}, {correctedY}");

            var snappedPoint = new Point(snappedX, snappedY);

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