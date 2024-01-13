using System.IO;
using System.Text.Json;

namespace PackStudio.Importers
{
    public class ProjectImporter
    {
        public string projectName = "Project.psproj";
        public float compression = 0.1f;
        public string outputPath = "Build/";
        public string fullOutputPath
        {
            get
            {
                if (!Path.IsPathFullyQualified(outputPath))
                    return Path.Combine(PSProj.path, outputPath);
                else return outputPath;
            }
        }

        public virtual void Save()
        {
            var meta = JsonSerializer.Serialize(this, GetType());
            File.WriteAllText(fullOutputPath + ".psproj", meta);
        }

        private ProjectImporter() { }
    }
}
 