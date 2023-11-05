using System.Windows.Input;


namespace CompanionApp.Controls;

public partial class HeaderView : ContentView
{
    public static readonly BindableProperty CommandOnMenuProperty = BindableProperty.Create("CommandOnMenu", typeof(ICommand), typeof(HeaderView), null);
    public static readonly BindableProperty CommandOnParameterProperty = BindableProperty.Create("CommandOnParameter", typeof(object), typeof(HeaderView), null);
    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create("HeaderText", typeof(string), typeof(HeaderView), string.Empty);
    public event EventHandler Clicked;

    public HeaderView()
    {
        InitializeComponent();
        lblHeader.SetBinding(Label.TextProperty, new Binding(nameof(HeaderText), source: this));
        btnMenu.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() => {
                Clicked?.Invoke(this, EventArgs.Empty);
                if (CommandOnMenu != null)
                {
                    if (CommandOnMenu.CanExecute(CommandOnParameter))
                        CommandOnMenu.Execute(CommandOnParameter);
                }
            })
        });
    }

    public string HeaderText
    {
        get { return (string)GetValue(HeaderTextProperty); }
        set { SetValue(HeaderTextProperty, value); }
    }

    public ICommand CommandOnMenu
    {
        get
        {
            return (ICommand)GetValue(CommandOnMenuProperty);
        }
        set
        {
            SetValue(CommandOnMenuProperty, value);
        }
    }

    public object CommandOnParameter
    {
        get
        {
            return (object)GetValue(CommandOnParameterProperty);
        }
        set
        {
            SetValue(CommandOnParameterProperty, value);
        }
    }
}