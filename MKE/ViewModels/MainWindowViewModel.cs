using MKE.Data;
using MKE.Services;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using MKE.Models.Messages;

namespace MKE.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Database _currentModel;
        private readonly EventAggregator _eventAggregator;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // This represents the current FEM data
        public Database CurrentModel
        {
            get { return _currentModel; }
            set
            {
                if (_currentModel != value)
                {
                    _currentModel = value;
                    OnPropertyChanged(nameof(CurrentModel));
                }
            }
        }

        // This handles save/load operations
        public DatabaseStorageManager StorageManager { get; }

        public MainWindowViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Use the storage manager to assign a new model
            CurrentModel = DatabaseStorageManager.Instance.ResetState();

            var databaseService = DatabaseService.Instance;
            databaseService.CurrentDatabase = CurrentModel;

            // Notify all subscribers that the database has been created
            _eventAggregator.Publish(new DatabaseCreatedMessage(CurrentModel));
        }

    }
}
