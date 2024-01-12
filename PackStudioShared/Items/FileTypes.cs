using Avalonia.Platform.Storage;
using System.IO;

namespace PackStudio.Items
{
    public class FileTypes
    {

        public static FilePickerFileType pack = new("Package")
        {
            Patterns = new[] { "*.pack", "*package", "*pkg", "*pack2" }
        };

        public static FilePickerFileType packStudio = new("Pack Project")
        {
            Patterns = new[] { "*.psproj", "*psp" }
        };

        public static bool IsPackage(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == ".pak" || ext == ".pack" || ext == ".package" || ext == ".pkg" || ext == ".pack2";
        }

        public static bool IsProject(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == ".psproj" || ext == ".psp";
        }
    }
}
