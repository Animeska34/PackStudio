using PackStudio.Importers;
using PackStudio.Items;
using System;
using System.Collections.Generic;
using System.IO;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PackStudio
{
    public class PSProj
    {
        private static PSProj current;
        [JsonInclude]
        private string m_path;

        public static Action projectLoaded;
        public static ProjectImporter config { get; private set; }
        public static string path => current.m_path;
        public static string assets => Path.Combine(path, "/Assets/");
        public static string library => Path.Combine(path, "/Library/");

        public static void Load(string project)
        {
            config = JsonSerializer.Deserialize<ProjectImporter>(File.ReadAllText(project));
            projectLoaded.Invoke();
        }
        public static void Save()
        {
            config.Save();
        }

        public static void SaveAs()
        {

        }
        public List<PackageItem> GetPackages()
        {
            if ( string.IsNullOrEmpty(path))
                return [];
            List<string> ret = new();

            var assets = Path.Combine(path + "/Assets/");
            if(!Path.Exists(assets))
                Directory.CreateDirectory(assets);
            DirectoryInfo assetDir = new DirectoryInfo(assets);
            
            var dirs = assetDir.GetDirectories();
            List<PackageImporter> importers;

            foreach (var dir in dirs)
            {
                //dir.FullName
            }

            return null;
        }

        private PSProj()
        {
            current = this;
        }
    }



    internal class PackageImporter
    {
    }
}
