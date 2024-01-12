using Avalonia.Controls;
using PackStudio.Operations;
using PackStudio.ViewModels;

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
