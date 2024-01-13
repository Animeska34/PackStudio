using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PackStudio.Importers
{
    public abstract class Importer
    {
        public string guid;
        public string type;
        public string cached;
        [JsonIgnore]
        public string asset;
        [JsonIgnore]
        public virtual bool cacheable => false;

        public bool IsCacheValid()
        {
            DateTime cacheTime = DateTime.Parse(cached);
            FileInfo assetInfo = new FileInfo(asset);

            if (assetInfo.LastWriteTimeUtc > cacheTime)
                return false;

            FileInfo metaInfo = new(asset + ".meta");
            if (metaInfo.LastWriteTimeUtc > cacheTime)
                return false;
            return true;
        }

        public virtual void Save()
        {
            var meta = JsonSerializer.Serialize(this, GetType());
            File.WriteAllText(asset + ".meta", meta);
        }
        public void Cache()
        {
            if (!cacheable)
                return;
            cached = DateTime.UtcNow.Ticks.ToString();
            Cache(Path.Combine(PSProj.cache, guid));
        }

        public void Update()
        {
            if(!IsCacheValid())
                Cache();
        }

        protected virtual void Cache(string destination) {
        
        }

        public virtual void Generate(string path)
        {
            asset = path;
            guid = Guid.NewGuid().ToString();
            type = ImportManager.GetName(GetType());
            Save();
        }
    }
}
