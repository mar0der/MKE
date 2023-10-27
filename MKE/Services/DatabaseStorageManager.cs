using MKE.Configuration;
using MKE.Data;
using System;
using System.IO;
using System.Text.Json;
using Microsoft.Win32;
using MKE.Models.Messages;
using System.Text.Json.Serialization;

namespace MKE.Services
{
    public sealed class DatabaseStorageManager
    {
        #region Public Properties
        public string CurrentFileName { get; private set; } = "Structure1.mke";
        public string CurrentFilePath { get; private set; } = AppSettings.DefaultSaveDirectory;
        public bool IsFileEverSaved { get; private set; } = false;
        public bool IsModifiedSinceLastSave { get; set; } = false;
        #endregion

        #region Singleton
        private static readonly Lazy<DatabaseStorageManager> _instance = new Lazy<DatabaseStorageManager>(() => new DatabaseStorageManager());
        public static DatabaseStorageManager Instance => _instance.Value;
        private DatabaseStorageManager() { }
        #endregion

        #region Public DB Acces Methods

        public Database ResetState()
        {
            var newDatabase = new Database(); // This initializes the empty model
                                                 // Further operations like resetting the current file name and path can be added
            CurrentFileName = "Structure1.mke";
            CurrentFilePath = AppSettings.DefaultSaveDirectory;
            IsFileEverSaved = false;
            IsModifiedSinceLastSave = false;

            return newDatabase;
        }

        // Not sure we need this yet
        public void SetCurrentFile(string path, string name)
        {
            CurrentFilePath = path;
            CurrentFileName = name;
        }
         
        public void Save(Database database)
        {
            SerializeAndSave(database, Path.Combine(CurrentFilePath, CurrentFileName));
        }

        public void SaveAs(Database database, string chosenPath)
        {
            CurrentFilePath = Path.GetDirectoryName(chosenPath);
            CurrentFileName = Path.GetFileName(chosenPath);

            SerializeAndSave(database, chosenPath);
            IsFileEverSaved = true;
        }

        public Database Open(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var json = File.ReadAllText(filePath);
            IsFileEverSaved = true;

            return JsonSerializer.Deserialize<Database>(json);
        }


        #endregion

        #region Other Private Methods
        private void SerializeAndSave(Database database, string path)
        {
            var json = JsonSerializer.Serialize(database);
            File.WriteAllText(path, json);
            IsModifiedSinceLastSave = false;
        }
        #endregion
    }
}
