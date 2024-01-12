using ReactiveUI;


namespace PackStudio.ViewModels;

public class LoadingOperationViewModel : ViewModelBase
{
    int min = 0, max = 100, val = 0;
    string text = "", title = "Loading";
    public int Minimun
    {
        get => min;
        set
        {
            min = value;
            Refresh(nameof(Minimun));
        }
    }
    public int Maximum
    {
        get => max;
        set
        {
            max = value;
            Refresh(nameof(Maximum));
        }
    }
    public int Value
    {
        get => val;
        set
        {
            val = value;
            Refresh(nameof(Value));
        }
    }
    public string Text
    {
        get => text;
        set
        {
            text = value;
            Refresh(nameof(Text));
        }
    }
    public string Title
    {
        get => title;
        set
        {
            title = value;
            Refresh(nameof(Title));
        }
    }

    private void Refresh(string name)
    {
        (this as IReactiveObject).RaisePropertyChanged(name);
    }
}
