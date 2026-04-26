using System;
using System.IO;

namespace DraxClient
{
    /// <summary>
    /// Resolves the shared client/service config folder under ProgramData.
    /// Used for files the client writes and the service reads (emailgroups.json,
    /// devices.json). Lives at C:\ProgramData\DraxTechnology so that LocalService
    /// (which can't see an interactive user's %AppData%) can read them.
    /// </summary>
    internal static class Paths
    {
        private const string FolderName = "DraxTechnology";
        private const string LegacyFolderName = "DraxClient";

        public static string StorageDirectory => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            FolderName);

        public static string LegacyStorageDirectory => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            LegacyFolderName);

        public static string GetFile(string filename) =>
            Path.Combine(StorageDirectory, filename);

        /// <summary>
        /// One-shot migration: if the new-location file is missing but the legacy
        /// %AppData%\DraxClient\&lt;filename&gt; exists, copy it. Safe to call repeatedly;
        /// either side (client write or service read) may run it first.
        /// </summary>
        public static void MigrateLegacyFile(string filename)
        {
            try
            {
                string newPath = GetFile(filename);
                if (File.Exists(newPath)) return;

                string oldPath = Path.Combine(LegacyStorageDirectory, filename);
                if (!File.Exists(oldPath)) return;

                Directory.CreateDirectory(StorageDirectory);
                File.Copy(oldPath, newPath);
            }
            catch
            {
                // best-effort; failures fall through to caller's normal load handling
            }
        }
    }
}
