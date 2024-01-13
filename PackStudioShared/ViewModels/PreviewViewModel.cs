using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Pack;
using PackStudio.Items;
using PackStudio.Operations;
using PackStudio.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace PackStudio.ViewModels
{
    public class PreviewViewModel : ViewModelBase
    {
        public List<ulong> selectedFiles = new();

        internal Window window;

        public ObservableCollection<BaseItem> Nodes { get; } = new();
        public ObservableCollection<BaseItem> SelectedNodes { get; } = new();


        public void UpdateState()
        {
            foreach (var node in Nodes)
            {
                if (node is DirectoryItem dir)
                    dir.Update();

            }
        }

        private string path = "/";
        public ICommand Extract => ReactiveCommand.Create(() =>
        {
            Debug.Print("Exporting " + selectedFiles.Count + " assets;");

            var dir = Path.GetDirectoryName(path);
            var exportPath = Path.Combine(dir, Path.GetFileNameWithoutExtension(path));
            ExtractSelected(exportPath);
        });
        public ICommand ExtractTo => ReactiveCommand.Create(async () =>
        {
            if (!window.StorageProvider.CanPickFolder)
            {
                throw new PlatformNotSupportedException();
            }
            var dir = await window.StorageProvider.SaveFilePickerAsync(
                new()
                {
                    Title = "Select extraction path",
                    SuggestedFileName = Path.GetFileNameWithoutExtension(path),
                    SuggestedStartLocation = await window.StorageProvider.TryGetFolderFromPathAsync(Path.GetDirectoryName(path)),
                });
            ExtractSelected(dir.Path.AbsolutePath);
        });


        private void ExtractSelected(string destination)
        {
            ExtractOperation op = new(selectedFiles, path, destination);

            LoadingOperation.Create(op, window);
            op.Execute();
        }

        public string Title { get; set; } = "Package Preview";

        public PreviewViewModel() { }
        public PreviewViewModel(string path)
        {
            this.path = path;
            var res = Common.GetPackInfo(path, out var info);

            if (res == PackResult.Success)
            {
                Title = Path.GetFileNameWithoutExtension(path);

                PackReader reader = new(path);
                var count = reader.ItemCount;

                if (count != 0)
                {
                    for (ulong i = 0; i < count; i++)
                    {
                        var item = reader.GetItemPath(i);
                        var folders = item.Split('/', '\\');

                        ObservableCollection<BaseItem> currentNode = Nodes;

                        for (int f = 0; f < folders.Length; f++)
                        {

                            var folder = folders[f];

                            if (f == folders.Length - 1)
                            {
                                currentNode.Add(new PackedFileItem(folder, i, this));
                                break;
                            }

                            DirectoryItem? selected = null;
                            foreach (var node in currentNode)
                            {
                                if (node.name == folder && node is DirectoryItem dir)
                                {
                                    selected = dir;
                                    currentNode = dir.SubNodes;
                                    break;
                                }
                            }
                            if (selected == null)
                            {
                                selected = new DirectoryItem(folder);
                                currentNode.Add(selected);

                            }

                            currentNode = selected.SubNodes;
                        }
                        //Nodes.Add(new FileItem(item));
                    }
                    UpdateState();
                    //var moth = Nodes.Last().SubNodes?.Last();
                    //if (moth != null) SelectedNodes.Add(moth);
                }
            }
        }
    }

}
