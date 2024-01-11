using Avalonia.Controls;
using PackStudio.ViewModels;
using System;

namespace PackStudio.Views
{
    public partial class Dialog : Window
    {
        DialogViewModel ctx;

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close("OK Clicked!");
        }

        //public event Action onClosed;
        public static Dialog Show(string text, Window caller, string title = "Message")
        {
            Dialog dialog = new Dialog(text, title);
            dialog.ShowDialog(caller);
            return dialog;
        }

        public Dialog(string text, string title = "Message") : this()
        {
            var model = new DialogViewModel();
            model.Title = title;
            model.Message = text;
            DataContext = model;
        }

        public Dialog()
        {
            InitializeComponent();
        }
    }
}
