using PackStudio.Importers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PackStudio
{
    public static class JsonUtility
    {
        public static string ToJson<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static T? FromJson<T>(string obj)
        {
            return JsonSerializer.Deserialize<T>(obj);
        }

        public static Importer LoadMeta(string file)
        {
            var meta = file + ".meta";
            if(Path.Exists(file))
            {
                if (!Path.Exists(meta))
                {
                    var importer = ImportManager.Import(file);
                    importer.Generate(file);
                }
                else
                {
                    
                }
            }
            throw new FileNotFoundException(file);
        }

        public static void SaveMeta(Importer meta)
        {

        }
    }
}
