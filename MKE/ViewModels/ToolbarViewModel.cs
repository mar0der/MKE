using MKE.Commands;
using System.Windows;
using System.Windows.Input;

namespace MKE.ViewModels
{
    public class ToolbarViewModel
    {
        public ICommand NewModelCommand { get; }
        public ICommand SaveModelCommand { get; }
        public ICommand NewNodeCommand { get; }
        public ICommand NewElementCommand { get; }
        public ICommand NewRollingSupportCommand { get; }
        public ICommand NewHingedSupportCommand { get; }
        public ICommand NewFixedSupportCommand { get; }
        public ICommand NewPointLoadCommand { get; }
        public ICommand NewUniformLoadCommand { get; }
        public ICommand CheckStructureCommand { get; }
        public ICommand SolveCommand { get; }
        public ICommand SettingsCommand { get; }

        public ToolbarViewModel()
        {
            NewModelCommand = new RelayCommand(_ => OnNewModel());
            SaveModelCommand = new RelayCommand(_ => OnSaveModel());
            NewNodeCommand = new RelayCommand(_ => OnNewNode());
            NewElementCommand = new RelayCommand(_ => OnNewElement());
            NewRollingSupportCommand = new RelayCommand(_ => OnNewRollingSupport());
            NewHingedSupportCommand = new RelayCommand(_ => OnNewHingedSupport());
            NewFixedSupportCommand = new RelayCommand(_ => OnNewFixedSupport());
            NewPointLoadCommand = new RelayCommand(_ => OnNewPointLoad());
            NewUniformLoadCommand = new RelayCommand(_ => OnNewUniformLoad());
            CheckStructureCommand = new RelayCommand(_ => OnCheckStructure());
            SolveCommand = new RelayCommand(_ => OnSolve());
            SettingsCommand = new RelayCommand(_ => OnSettings());
        }


        private void OnNewModel()
        {
            var a = 5;
            // Handle New Model button click
        }

        private void OnSaveModel()
        {
            var b = 5;
            // Handle Save Model button click
        }

        private void OnNewNode()
        {
            var c = 3;
            // Handle New Node button click
        }

        private void OnNewElement()
        {
            var a = 5;
            // Handle New Element button click
        }

        private void OnNewRollingSupport()
        {
            var b = 5;
            // Handle New Rolling Support button click
        }

        private void OnNewHingedSupport()
        {
            var c = 3;
            // Handle New Hinged Support button click
        }

        private void OnNewFixedSupport()
        {
                var b = 5;
            // Handle New Fixed Support button click
        }

        private void OnNewPointLoad()
        {
            var c = 3;
            // Handle New Point Load button click
        }

        private void OnNewUniformLoad()
        {
            var b = 5;
            // Handle New Uniform Load button click
        }

        private void OnCheckStructure()
        {
            var c = 3;
            // Handle Check Structure button click
        }

        private void OnSolve()
        {
            var b = 5;
            // Handle Solve button click
        }

        private void OnSettings()
        {
            var c = 3;
            // Handle Settings button click
        }
    }
}
