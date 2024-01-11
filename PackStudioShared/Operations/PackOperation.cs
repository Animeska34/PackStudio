using Avalonia.Controls.Chrome;
using Avalonia.Threading;
using Pack.V2;
using PackStudio.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PackStudio.Operations
{
    internal class PackOperation : LoadingOperationBase
    {
        protected List<string> packages;
        protected List<string> assets = new();
        protected List<string> excluded = new();
        string package;
        string destination;
        string[] paths;


        public Task Execute()
        {
            var task = new Task(ExecuteAsync);
            task.Start();
            return task;
        }


        private async void Error(string data, string title = "Error")
        {
            var d = new Dialog(data, title);
            await d.ShowDialog(operation);
            operation.Close();
        }

        private void AnalyzeDirectory(DirectoryInfo dir)
        {
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                if (!file.Attributes.HasFlag(FileAttributes.Hidden) && !file.Attributes.HasFlag(FileAttributes.System))
                {
                    if (file.Length == 0)
                        excluded.Add(file.FullName);
                    else
                    {
                        assets.Add(file.FullName.Replace("\\", "/"));
                        assets.Add(Path.GetRelativePath(package, file.FullName.Replace("\\", "/")));
                    }
                }
            }
            var dirs = dir.GetDirectories();
            foreach (var subDir in dirs)
            {
                if (!subDir.Attributes.HasFlag(FileAttributes.Hidden) && !subDir.Attributes.HasFlag(FileAttributes.System))
                    AnalyzeDirectory(subDir);
            }
        }
        
        private static void OnPackFileStatic(UInt64 index, object o)
        {
            ((PackOperation) o).OnPackFile(index);
        }

        private void OnPackFile(UInt64 index)
        {
            ctx.Value = (Int32)index;
            ctx.Text = $"({index}/{ctx.Maximum}) Packing: {paths[index * 2 + 1]}";
            Debug.WriteLine(ctx.Text);
        }

        private async void ExecuteAsync()
        {
            //synchronizationContext.Post(_ => OnAsyncEvent("Preparing", 0, 1), null);
            ctx.Text = $"(0/{packages.Count * 2 + 3}) Preparing";
            ctx.Value = 0;
            ctx.Title = "Building";
            ctx.Maximum = packages.Count * 2 + 3;
            /*
            if(!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            */
            Thread.Sleep(10);
            var step = 1;
            foreach (var path in packages)
            {
                ctx.Text = $"({step}/{packages.Count * 2 + 3}) Indexing assets for: {Path.GetFileName(path)}";
                ctx.Value = step;
                step++;
                this.package = path;
                DirectoryInfo package = new(path);
                Thread.Sleep(10);
                AnalyzeDirectory(package);

                ctx.Text = $"({step}/{packages.Count * 2 + 3}) Packing: {Path.GetFileName(path)}";
                ctx.Value = step;
                step++;

                Int32 interation = 0;

                /*
                ctx.Maximum = 1000;
                while (true)
                {
                    //SetValue(interation);
                    //SetText(interation.ToString());

                    ctx.Value = interation;
                    ctx.Text = interation.ToString();

                    //Thread.Sleep(10);
                    interation++;
                }
                */


                /*
                if(excluded.Count > 0)
                {
                    string msg = "Following assets was excluded from build: \n";
                    foreach (var item in excluded)
                    {
                        msg += item + "\n";
                    }
                    Dispatcher.UIThread.Invoke(async () => { Error(msg, "Warning"); });
                }
                */

                var count = assets.Count / 2;
                ctx.Value = 0;
                ctx.Maximum = count;
                ctx.Text = "Starting packing: " + path;
                paths = assets.ToArray();
                var result = PackWriter.PackFiles(path + ".pak", paths, 1f, false, OnPackFileStatic, this);

                Debug.WriteLine("Pack operation complete: " + result.ToString());
                if (result != PackResult.Success)
                {
                    Dispatcher.UIThread.Invoke(async () => { Error(result.ToString()); });
                    return;
                }
                else
                {
                    Dispatcher.UIThread.Invoke(() => { operation.Close(); });
                }
            }

            ctx.Text = $"({packages.Count + 1}/{packages.Count + 3}) Preparing to pack";
            ctx.Value = packages.Count + 1;
        }

        public PackOperation(List<string> packages, string destination)
        {
            this.packages = packages;
            this.destination = destination;
        }
    }
}
