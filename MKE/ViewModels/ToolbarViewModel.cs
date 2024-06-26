﻿using MKE.Commands;
using MKE.Data;
using MKE.Models.Messages;
using MKE.Services;
using System;
using System.Diagnostics.Tracing;
using System.Windows.Input;

namespace MKE.ViewModels
{
    public class ToolbarViewModel
    {
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

            // Step 1: Initialize a new FEMDatabase instance.
            var newDatabase = new Database();

            // Step 2: Update the global or shared instances to reflect the new model.
            DatabaseService.Instance.CurrentDatabase = newDatabase;
            IdGeneratorService.Instance.InitializeWithDatabase(newDatabase);

            // Resetting the storage manager's state, especially if it tracks whether a file has been saved before.
            DatabaseStorageManager.Instance.ResetState();

        }

        private void OnOpenModel()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mke";
            dlg.Filter = "MKE Files (.mke)|*.mke";

            // Display OpenFileDialog by calling ShowDialog method
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                string fullPath = dlg.FileName;
                string directoryPath = System.IO.Path.GetDirectoryName(fullPath);
                string fileName = System.IO.Path.GetFileName(fullPath);

                // Use the FEMStorageManager to open and deserialize the file into a FEMDatabase instance
                var database = DatabaseStorageManager.Instance.Open(fullPath);

                if (database != null)
                {
                    DatabaseService.Instance.CurrentDatabase = database;
                    DatabaseStorageManager.Instance.SetCurrentFile(directoryPath, fileName);
                    IdGeneratorService.Instance.InitializeWithDatabase(database);
                    
                    EventAggregator.Instance.Publish(new DatabaseUpdatedMessage(database));
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private void OnSaveModel()
        {
            var databaseInstance = DatabaseService.Instance.CurrentDatabase;

            if (!DatabaseStorageManager.Instance.IsFileEverSaved)
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
                    DatabaseStorageManager.Instance.SaveAs(databaseInstance, chosenPath);
                }
            }
            else
            {
                DatabaseStorageManager.Instance.Save(databaseInstance);
            }
        }

        private void OnNewNode()
        {
            EventAggregator.Instance.Publish(new EnterNodeCreationModeMessage());
        }

        private void OnNewElement()
        {
            EventAggregator.Instance.Publish(new EnterElementCreationModeMessage());
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
