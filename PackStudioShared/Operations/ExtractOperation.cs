using Avalonia.Threading;
using Pack.V2;
using PackStudio.Views;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace PackStudio.Operations
{
    public class ExtractOperation : LoadingOperationBase
    {
        protected List<ulong> items;
        string package;
        string destination;

        public Task Execute()
        {
            var task = new Task(ExecuteAsync);
            task.Start();
            return task;
        }


        private async void Error(string data)
        {
            var d = new Dialog(data, "Error");
            await d.ShowDialog(operation);
            operation.Close();
        }

        private void ExecuteAsync()
        {
            //synchronizationContext.Post(_ => OnAsyncEvent("Preparing", 0, 1), null);
            ctx.Text = "(0/1) Preparing";
            Thread.Sleep(10);
            if (Directory.Exists(destination))
            {

                //throw new System.Exception("Directory Already Exists");

                Dispatcher.UIThread.Invoke(async () => { Error("Directory Already Exists"); });

                return;
            }
            else
            {
                Directory.CreateDirectory(destination);
                PackReader reader = new(package);

                int progress = 1;
                ctx.Maximum = items.Count;
                foreach (var item in items)
                {
                    var path = reader.GetItemPath(item);

                    ctx.Text = $"({progress}/{items.Count}) Extracting \"{path}\"";
                    ctx.Value = progress;
                    Debug.Print(progress.ToString());
                    //synchronizationContext.Post(_ => OnAsyncEvent(text, progress, items.Count), null);
                    progress++;

                    var size = reader.GetItemDataSize(item);
                    var buffer = new byte[size];
                    var res = reader.ReadItemData(item, ref buffer);

                    if (res == PackResult.Success)
                    {
                        var dest = Path.Combine(destination, path);

                        var dir = Path.GetDirectoryName(dest);

                        if (dir != null && !Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        File.WriteAllBytes(dest, buffer);

                    }
                    else
                    {
                        Dispatcher.UIThread.Invoke(async () => { Error(res.ToString()); });
                        return;
                    }
                }
            }

            Dispatcher.UIThread.Invoke(() => operation.Close());
        }

        public ExtractOperation(List<ulong> itm, string pkg, string dest)
        {
            items = itm;
            package = pkg;
            destination = dest;
        }
    }
}
