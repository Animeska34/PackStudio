using System.IO;

namespace PackStudio.Items
{
    public class PackageItem : BaseItem
    {
        public string path { get; set; }

        public PackageItem(string path)
        {
            DirectoryInfo dir = new(path);
            name = dir.Name;
            this.path = dir.FullName;
        }
    }
}