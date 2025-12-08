using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Drax360Client.Panels.Email
{
    internal class PanelEmailClient
    {
        private readonly object _fileLock = new();
        private static readonly string FileName = "emailgroups.json";
        private static readonly string StorageDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Drax360Client");
        private static readonly string StorageFilePath = Path.Combine(StorageDirectory, FileName);

        public List<Group> Groups = new List<Group>();

        public PanelEmailClient()
        {
            // Attempt to load persisted groups; if that fails, populate defaults.
            if (!Load())
            {
                CreateDefaultSample();
            }
        }

        /// <summary>
        /// Load groups from disk (JSON). Returns true on success.
        /// </summary>
        public bool Load()
        {
            lock (_fileLock)
            {
                try
                {
                    if (!File.Exists(StorageFilePath)) return false;

                    string json = File.ReadAllText(StorageFilePath);
                    if (string.IsNullOrWhiteSpace(json)) return false;

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReadCommentHandling = JsonCommentHandling.Skip,
                        AllowTrailingCommas = true
                    };

                    var loaded = JsonSerializer.Deserialize<List<Group>>(json, options);
                    if (loaded != null)
                    {
                        Groups = loaded;
                        return true;
                    }

                    return false;
                }
                catch
                {
                    // swallow exceptions for resiliency; callers can decide next steps.
                    return false;
                }
            }
        }

        /// <summary>
        /// Save groups to disk (JSON). Returns true on success.
        /// </summary>
        public bool Save()
        {
            lock (_fileLock)
            {
                try
                {
                    if (!Directory.Exists(StorageDirectory))
                    {
                        Directory.CreateDirectory(StorageDirectory);
                    }

                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                    string json = JsonSerializer.Serialize(Groups, options);

                    // Write atomically: write to temp then move
                    string temp = StorageFilePath + ".tmp";
                    File.WriteAllText(temp, json, System.Text.Encoding.UTF8);
                    File.Copy(temp, StorageFilePath, overwrite: true);
                    File.Delete(temp);

                    return true;
                }
                catch
                {
                    // swallow exceptions for resiliency
                    return false;
                }
            }
        }

        private void CreateDefaultSample()
        {
            Groups.Clear();

            Group a = new Group();
            a.Name = "Default Group";
            a.Description = "This is the default group.";

            Address addr1 = new Address();
            addr1.Email = "mike.holmes@draxtechnology.com";
            addr1.Pin = "1234";
            a.Addresses.Add(addr1);

            Address addr2 = new Address();
            addr2.Email = "richard@binaryrefinery.com";
            addr2.Pin = "9876";
            a.Addresses.Add(addr2);
            Groups.Add(a);

            Group b = new Group();
            b.Name = "This is second Group";
            b.Description = "";
            Groups.Add(b);
        }
    }
}
