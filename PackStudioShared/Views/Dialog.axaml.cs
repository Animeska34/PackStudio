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
        public static Dialog Show(string text, Window caller, string title = "Message", string color = "Black")
        {
            Dialog dialog = new(text, title, string.IsNullOrEmpty(color) ? "Black" : color);
            dialog.ShowDialog(caller);
            return dialog;
        }

        public Dialog(string text, string title = "Message", string color = "Black") : this()
        {
            var model = new DialogViewModel();
            model.title = title;
            model.message = text;
            DataContext = model;
        }

        public Dialog()
        {
            InitializeComponent();
        }
    }
}
