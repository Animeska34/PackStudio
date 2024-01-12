using PackStudio.Items;
using PackStudio.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace PackStudio.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    private PackageItem m_SelectedPackages;
    public PackageItem SelectedPackages
    {
        get => m_SelectedPackages; set
        {

            m_SelectedPackages = value;
            LoadHierarhy(value);
        }
    }
    public ObservableCollection<BaseItem> Hierarchy { get; set; } = new();
    public ObservableCollection<BaseItem> SelectedFiles { get; set; } = new();
    public ObservableCollection<PackageItem> Packages { get; set; } = new();

    public ICommand BuildAll => ReactiveCommand.Create(() =>
    {
        if(Packages.Count == 0)
        {
            Dialog.Show("No directories loaded", MainWindow.window, "Error");
            return;
        }
        List<string> packages = new();
        foreach (var item in Packages)
        {
            if (item.selected.GetValueOrDefault(false))
            {
                packages.Add(item.path);
            }
        }
        MainWindow.Build(packages, "");
    });

    public ICommand BuildCurrent => ReactiveCommand.Create(() =>
    {
        if (SelectedPackages == null)
        {
            Dialog.Show("No directory selected", MainWindow.window, "Error");
            return;
        }
        List<string> packages = new();
        packages.Add(SelectedPackages.path);
        MainWindow.Build(packages, "");
    });

    public ICommand About => ReactiveCommand.Create(() =>
    {
    });
    public ICommand Help => ReactiveCommand.Create(() =>
    {
        Process.Start(new ProcessStartInfo("https://github.com/Animeska34/PackStudio/wiki") { UseShellExecute = true });
    });
    public ICommand OnItemPressed => ReactiveCommand.Create(() =>
    {
    });

    public ICommand OpenCommand => ReactiveCommand.Create(() =>
    {
        MainWindow.OpenFile();
    });


    public void AddPackage(string path)
    {
        foreach (PackageItem item in Packages)
        {
            if (item.path == path)
                return;
        }
        Debug.WriteLine("Adding package " + path);
        Packages.Add(new PackageItem(path));
    }

    public void LoadHierarhy(PackageItem item)
    {
        DirectoryInfo dir = new(item.path);
        var dirs = dir.GetDirectories();

        Hierarchy.Clear();
        foreach (var subDir in dirs)
        {
            if (!subDir.Attributes.HasFlag(FileAttributes.Hidden) && !subDir.Attributes.HasFlag(FileAttributes.System))
                Hierarchy.Add(new DirectoryItem(subDir.Name, subDir.FullName));
        }
        var files = dir.GetFiles();
        foreach (var file in files)
        {
            if (!file.Attributes.HasFlag(FileAttributes.Hidden) && !file.Attributes.HasFlag(FileAttributes.System))
                Hierarchy.Add(new FileItem(file.Name, file.FullName, null));
        }
    }
    public MainViewModel()
    {

    }
}
