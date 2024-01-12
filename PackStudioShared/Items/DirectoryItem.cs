using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace PackStudio.Items
{
    public class DirectoryItem : BaseItem
    {
        public override string title => "📁 " + name;

        public override bool? selected
        {
            get => m_selected;
            set
            {
                Debug.Print("Setting: " + value);
                var activate = value.GetValueOrDefault(true);
                m_selected = activate;

                var child = value.GetValueOrDefault(true);
                Debug.Print("Setting Childs: " + child + ". Childs count " + SubNodes.Count);
                foreach (var item in SubNodes)
                {
                    Debug.Print("Updating node: " + item.name);
                    item.selected = child;
                }
            }
        }

        public void FastUpdate()
        {
            bool allTrue = true;
            bool allFalse = true;

            int ts = 0, fs = 0;

            Debug.Print("Total SubNodes: " + SubNodes.Count);


            foreach (var item in SubNodes)
            {
                if (item is DirectoryItem dir2)
                    break;

                var state = item.selected;

                if (state == null || state.Value == false)
                {
                    fs++;
                    allTrue = false;
                }
                else
                {
                    ts++;
                    allFalse = false;
                }

                if (!allTrue && !allFalse)
                    break;
            }

            if (allTrue)
                m_selected = true;
            else if (allFalse)
                m_selected = false;
            else
                m_selected = null;

            OnPropertyChanged(nameof(selected));
        }
        public void Update()
        {

            bool allTrue = true;
            bool allFalse = true;

            int ts = 0, fs = 0;

            foreach (var item in SubNodes)
            {
                if (item is DirectoryItem dir2)
                    dir2.Update();

                var state = item.selected;

                if (state == null || state.Value == false)
                {
                    fs++;
                    allTrue = false;
                }
                else
                {
                    ts++;
                    allFalse = false;
                }

                if (!allTrue && !allFalse)
                    break;
            }

            if (allTrue)
                m_selected = true;
            else if (allFalse)
                m_selected = false;
            else
                m_selected = null;

            OnPropertyChanged(nameof(selected));
        }

        public DirectoryItem(string name)
        {
            this.name = name;
            isFolder = true;
        }

        public DirectoryItem(string name, string path)
        {

            DirectoryInfo dir = new(path);
            this.name = name;

            var dirs = dir.GetDirectories();

            foreach (var subDir in dirs)
            {
                if (!subDir.Attributes.HasFlag(FileAttributes.Hidden) && !subDir.Attributes.HasFlag(FileAttributes.System))
                    SubNodes.Add(new DirectoryItem(subDir.Name, subDir.FullName));
            }
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                if (!file.Attributes.HasFlag(FileAttributes.Hidden) && !file.Attributes.HasFlag(FileAttributes.System))
                    SubNodes.Add(new FileItem(file.Name, file.FullName, this));
            }
        }

        public DirectoryItem(string name, ObservableCollection<BaseItem> subNodes)
        {
            this.name = name;
            SubNodes = subNodes;
            isFolder = true;
        }
    }
}
