﻿using MKE.Commands;
using MKE.Models.Messages;
using MKE.Services;
using System;
using System.Diagnostics.Tracing;
using System.Windows.Input;

namespace MKE.ViewModels
{
    public class ToolbarViewModel
    {
        private readonly EventAggregator _eventAggregator;
        #region Events registration
        public event Action AddNodeRequested;
        #endregion

        #region Commands Registration
        public ICommand NewModelCommand { get; }
        public ICommand OpenModelCommand { get; }
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
        #endregion

        public ToolbarViewModel(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            NewModelCommand = new RelayCommand(_ => OnNewModel());
            OpenModelCommand = new RelayCommand(_ => OnOpenModel());
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

        #region button click methods
        private void OnNewModel()
        {
            // Handle New Model button click
        }

        private void OnOpenModel()
        {

        }

        private void OnSaveModel()
        {
            var databaseInstance = FEMDatabaseService.Instance.CurrentDatabase;

            if (!FEMDatabaseStorageManager.Instance.IsFileEverSaved)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Structure"; // Default file name
                dlg.DefaultExt = ".mke"; // Default file extension
                dlg.Filter = "MKE Files (.mke)|*.mke"; // Filter files by extension

                // Display the save file dialog
                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    // Retrieve the chosen path
                    string chosenPath = dlg.FileName;

                    // Pass the database and the chosen path to FEMStorageManager
                    FEMDatabaseStorageManager.Instance.SaveAs(databaseInstance, chosenPath);
                }
            }
            else
            {
                FEMDatabaseStorageManager.Instance.Save(databaseInstance);
            }
        }

        private void OnNewNode()
        {
            _eventAggregator.Publish(new EnterNodeCreationModeMessage());
        }

        private void OnNewElement()
        {
            // Handle New Element button click
        }

        private void OnNewRollingSupport()
        {
            // Handle New Rolling Support button click
        }

        private void OnNewHingedSupport()
        {
            // Handle New Hinged Support button click
        }

        private void OnNewFixedSupport()
        {
            // Handle New Fixed Support button click
        }

        private void OnNewPointLoad()
        {
            // Handle New Point Load button click
        }

        private void OnNewUniformLoad()
        {
            // Handle New Uniform Load button click
        }

        private void OnCheckStructure()
        {
            // Handle Check Structure button click
        }

        private void OnSolve()
        {
            // Handle Solve button click
        }

        private void OnSettings()
        {
            // Handle Settings button click
        }
        #endregion
    }
}
