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
        private FEMDatabase _currentModel;
        private readonly EventAggregator _eventAggregator;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // This represents the current FEM data
        public FEMDatabase CurrentModel
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
        public FEMStorageManager StorageManager { get; }

        public MainWindowViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Use the storage manager to assign a new model
            CurrentModel = FEMStorageManager.Instance.CreateNewModel();

            var databaseService = FEMDatabaseService.Instance;
            databaseService.CurrentDatabase = CurrentModel;

            // Notify all subscribers that the database has been created
            _eventAggregator.Publish(new DatabaseCreatedMessage(CurrentModel));
        }

    }
}
