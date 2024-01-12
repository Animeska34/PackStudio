using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PackStudio.ViewModels;
using PackStudio.Views;
using System;
using System.IO;

namespace PackStudio;

public partial class App : Application
{

    public static Window? main { get; private set; }

    public static void Preview(string path)
    {
        var ext = Path.GetExtension(path);
        if (ext != ".pack" || ext != ".package" || ext != ".pak" || ext != ".pack2")
            return;

        var context = new PreviewViewModel(path);

        var pv = new PreviewWindow()
        {
            DataContext = context
        };

        if (main != null)
            pv.ShowDialog(main);
        else
            pv.Show();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var args = Environment.GetCommandLineArgs();
        if (args.Length == 2)
        {
            var arg = args[1];

            if (File.Exists(arg))
            {
                var ext = Path.GetExtension(arg);
                if (ext == ".pack" || ext == ".package" || ext == ".pak" || ext == ".pack2")
                {
                    var context = new PreviewViewModel(arg);

                    var pv = new PreviewWindow()
                    {
                        DataContext = context
                    };

                    context.window = pv;

                    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                    {
                        main = desktop.MainWindow = pv;
                    }
                }
                else if (Path.GetExtension(arg) == ".packproj")
                {
                }
            }
        }

        //Preview("D:\\repos\\pack\\build\\Release\\testPack.pack");

        /*
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }
        */

        if (main == null)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                main = desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel()
                };
            }
        }

        base.OnFrameworkInitializationCompleted();
    }
}
