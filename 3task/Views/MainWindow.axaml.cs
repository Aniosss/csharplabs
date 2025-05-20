using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CandyProductionApp.Views;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();
    void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
