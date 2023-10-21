using System;

namespace MKE.Configuration
{
    public static class AppSettings
    {
        // Default directory where the "Save As" dialog will open initially
        public static string DefaultSaveDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // ... Additional settings ...
    }
}
