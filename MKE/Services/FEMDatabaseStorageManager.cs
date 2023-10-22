using MKE.Configuration;
using MKE.Data;
using System;
using System.IO;
using System.Text.Json;
using Microsoft.Win32;

namespace MKE.Services
{
    public sealed class FEMDatabaseStorageManager
    {
        

        #region Public Properties
        public string CurrentFileName { get; private set; } = "Structure1.mke";
        public string CurrentFilePath { get; private set; } = AppSettings.DefaultSaveDirectory;
        public bool IsFileEverSaved { get; private set; } = false;
        public bool IsModifiedSinceLastSave { get; set; } = false;
        #endregion

        #region Singleton
        private static readonly Lazy<FEMDatabaseStorageManager> _instance = new Lazy<FEMDatabaseStorageManager>(() => new FEMDatabaseStorageManager());
        public static FEMDatabaseStorageManager Instance => _instance.Value;
        private FEMDatabaseStorageManager() { }
        #endregion

        public void SetCurrentFile(string path, string name)
        {
            CurrentFilePath = path;
            CurrentFileName = name;
        }
         
        public void Save(FEMDatabase database)
        {
            //if (!IsFileEverSaved)
            //{
            //    SaveAs(database);
            //    return;
            //}

            SerializeAndSave(database, Path.Combine(CurrentFilePath, CurrentFileName));
        }

        public void SaveAs(FEMDatabase database, string chosenPath)
        {
            CurrentFilePath = Path.GetDirectoryName(chosenPath);
            CurrentFileName = Path.GetFileName(chosenPath);

            SerializeAndSave(database, chosenPath);
            IsFileEverSaved = true;
        }

        public FEMDatabase Load()
        {
            string filePath = Path.Combine(CurrentFilePath, CurrentFileName);

            if (!File.Exists(filePath))
                return null;

            var json = File.ReadAllText(filePath);
            IsFileEverSaved = true; // Since the file exists and was loaded, it means it was saved before.
            return JsonSerializer.Deserialize<FEMDatabase>(json);
        }

        public FEMDatabase CreateNewModel()
        {
            var newDatabase = new FEMDatabase(); // This initializes the empty model
                                                 // Further operations like resetting the current file name and path can be added
            CurrentFileName = "Structure1.mke";
            CurrentFilePath = AppSettings.DefaultSaveDirectory;
            IsFileEverSaved = false;
            IsModifiedSinceLastSave = false;

            return newDatabase;
        }

        private void SerializeAndSave(FEMDatabase database, string path)
        {
            var json = JsonSerializer.Serialize(database);
            File.WriteAllText(path, json);
            IsModifiedSinceLastSave = false;
        }
    }
}
