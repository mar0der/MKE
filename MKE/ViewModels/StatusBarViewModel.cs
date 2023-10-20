using MKE.Models.Messages;
using MKE.Services;
using System.ComponentModel;
using System.Windows;

namespace MKE.ViewModels
{
    public class StatusBarViewModel : INotifyPropertyChanged
    {
        private readonly EventAggregator _eventAggregator;

        public event PropertyChangedEventHandler PropertyChanged;

        public string StatusMessage { get; set; }

        public string CoordinateDisplayText { get; set; }

        public StatusBarViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<StatusBarDataMessage>(HandleStatusBarDataMessage);
        }

        private void HandleStatusBarDataMessage(StatusBarDataMessage message)
        {
            StatusMessage = message.StatusMessage;
            CoordinateDisplayText = $"X: {message.Coordinate.X}, Y: {message.Coordinate.Y}";
            OnPropertyChanged(nameof(CoordinateDisplayText));
            OnPropertyChanged(nameof(StatusMessage));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
