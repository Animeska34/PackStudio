using DynamicData;
using PackStudio.ViewModels;
using System.Diagnostics;


namespace PackStudio.Items
{
    public class PackedFileItem : BaseItem
    {
        private PreviewViewModel vm;
        public ulong id { get; }


        public override bool? selected
        {
            get => m_selected;
            set
            {
                if (value.GetValueOrDefault(true))
                {
                    SetSilent(true);
                    OnPropertyChanged(nameof(selected));
                    vm.UpdateState();
                }
                else
                {
                    SetSilent(false);
                    OnPropertyChanged(nameof(selected));
                    vm.UpdateState();
                }
            }
        }

        protected override void SetSilent(bool value)
        {
            if (value)
            {
                m_selected = true;
                vm.selectedFiles.Add(id);
            }
            else
            {
                m_selected = false;
                vm.selectedFiles.Remove(id);
            }
        }

        public PackedFileItem(string name, ulong id, PreviewViewModel vm)
        {
            this.name = name;
            this.id = id;
            isFolder = false;
            this.vm = vm;
            vm.selectedFiles.Add(id);
        }

    }
}
