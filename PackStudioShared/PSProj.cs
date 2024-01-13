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
        [JsonInclude]
        private string m_path;

        public Action projectLoaded;
        public ProjectImporter config { get; private set; }
        public string path => m_path;
        public string assets => Path.Combine(path, "/Assets/");
        public string cache => Path.Combine(path, "/.Cache/");

        public void Load(string project)
        {
            config =  JsonSerializer.Deserialize<ProjectImporter>(File.ReadAllText(project));
            projectLoaded.Invoke();
        }
        public void Save()
        {
            config.Save();
        }

        public void SaveAs()
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
    }

    internal class PackageImporter
    {
    }
}
