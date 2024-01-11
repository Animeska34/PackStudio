using Avalonia.Controls;
using Avalonia.Media;
using PackStudio.ViewModels;
using PackStudio.Operations;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PackStudio.Views
{
    public partial class LoadingOperation : Window
    {
        public static void Create(LoadingOperationBase operation, Window caller)
        {
            var ctx = new LoadingOperationViewModel();

            LoadingOperation window = new()
            {
                DataContext = ctx,
            };

            window.ShowDialog(caller);
            operation.Setup(window);
        }

        
        public LoadingOperation()
        {
            InitializeComponent();
        }
    }
}
