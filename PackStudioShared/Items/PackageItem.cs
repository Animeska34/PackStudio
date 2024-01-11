using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackStudio.Items
{
    public class PackageItem : BaseItem
    {
        public string path { get; set; }

        public PackageItem(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            name = dir.Name;
            this.path = dir.FullName;
        }
    }
}