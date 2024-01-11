using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using PackStudio.ViewModels;
using PackStudio.Items;
using PackStudio.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PackStudio.Views;

public partial class MainWindow : Window
{
    public static MainWindow window;
    public static IStorageProvider storageProvider;
    public static MainViewModel ctx => window.DataContext as MainViewModel;
    public MainWindow()
    {
        window = this;
        Debug.WriteLine("Registering");
        AddHandler(DragDrop.DropEvent, Drop);
        AddHandler(DragDrop.DragEnterEvent, DragEnter);
        AddHandler(DragDrop.DragLeaveEvent, DragLeave);
        DragDrop.SetAllowDrop(this, true);
        storageProvider = GetTopLevel(this).StorageProvider;
        InitializeComponent();
    }

    public static async void OpenFile()
    {
        if (storageProvider.CanOpen)
        {
            var items = await storageProvider.OpenFilePickerAsync(new()
            {
                Title = "Select a files",
                AllowMultiple = true,
                FileTypeFilter = new List<FilePickerFileType>() {
                    FileTypes.pack, FileTypes.packStudio
                }
            });

        }
    }

    public static void Build(List<string> packages, string destination)
    {
        PackOperation operation = new(packages, destination);
        LoadingOperation.Create(operation, window);
        operation.Execute();
    }

    public static void Import(string path)
    {
        FileInfo info = new FileInfo(path);

        if (FileTypes.IsPackage(path))
        {
            var context = new PreviewViewModel(path);
            var pv = new PreviewWindow()
            {
                DataContext = context
            };
            context.window = pv;
            pv.Show();
        } else if (FileTypes.IsProject(path))
        {

        } else if (info.Attributes.HasFlag(FileAttributes.Directory))
        {
            ctx.AddPackage(path);
            //window.DataContext.
        }
    }


    private void Drop(object? sender, DragEventArgs e)
    {
        Debug.WriteLine("Dropping " + e + " " + sender + " " + e.Source + " " + e.Data);

        var data = e.Data;
        if(data != null ) 
        {
            var files = data.GetFiles();

            if(files != null)
            {
                 foreach (var file in files)
                {
                    string path = file.Path.LocalPath;
                    Import(path);
                }
            }
 
        }
    }

    private void DragEnter(object? sender, DragEventArgs e)
    {
        Debug.WriteLine("Entering " + e);
    }
    private void DragLeave(object? sender, DragEventArgs e)
    {
        Debug.WriteLine("Leaving " + e);
    }
}
