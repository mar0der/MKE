using MKE.Commands;
using MKE.UIModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MKE.ViewModels
{
    public class DrawingCanvasViewModel : INotifyPropertyChanged
    {
        public int GridSize { get; set; } = 20;

        public ObservableCollection<GridLine> VerticalLines { get; } = new ObservableCollection<GridLine>();
        public ObservableCollection<GridLine> HorizontalLines { get; } = new ObservableCollection<GridLine>();


        #region Command Registration
        public ICommand AddNodeCommand { get; }

#endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DrawingCanvasViewModel()
        {
        }

        public void UpdateGridLines(double width, double height)
        {
            VerticalLines.Clear();
            HorizontalLines.Clear();

            //HorizontalLines.Add(new GridLine(0, 50, 1100, 50));
            //VerticalLines.Add(new GridLine(50, 0, 50, 750));
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

#endregion
    }
}