using PackStudio.ViewModels;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PackStudio.Items
{
    public class BaseItem : ViewModelBase
    {
        public ObservableCollection<BaseItem> SubNodes { get; protected set; } = new();
        public string name { get; protected set; } = "";
        public bool isFolder { get; protected set; }
        public virtual string title => name;

        public bool? m_selected = true;

        public bool enabled { get; protected set; } = true;
        public virtual bool? selected
        {
            get => m_selected;
            set
            {
                m_selected = value;
                OnPropertyChanged(nameof(selected));
            }
        }

        public void SetActive(bool active)
        {
            Debug.Print("Setting active " + name + " " + active);
            enabled = active;
            OnPropertyChanged(nameof(enabled));

            foreach (var item in SubNodes)
            {
                item.SetActive(active);
            }
        }
        protected virtual void SetSilent(bool value)
        {
            m_selected = value;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            (this as IReactiveObject).RaisePropertyChanged(new(propertyName));
        }
    }
}
