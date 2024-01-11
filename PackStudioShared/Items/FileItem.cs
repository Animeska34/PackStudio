
using PackStudio.ViewModels;

using System.IO;


namespace PackStudio.Items
{
    internal class FileItem : BaseItem
    {
        DirectoryItem parent;
        public string path { get; }

        public override bool? selected
        {
            get => m_selected;
            set
            {
                if (value.GetValueOrDefault(true))
                {
                    SetSilent(true);
                    OnPropertyChanged(nameof(selected));
                }
                else
                {
                    SetSilent(false);
                    OnPropertyChanged(nameof(selected));
                }
                parent?.FastUpdate();
            }
        }

        protected override void SetSilent(bool value)
        {
            if (value)
            {
                m_selected = true;
            }
            else
            {
                m_selected = false;
            }
        }

        public FileItem(string name, string path, DirectoryItem parent)
        {
            this.name = name;
            this.path = Path.GetFullPath(path);
            isFolder = false;
            this.parent = parent;
        }
    }
}
