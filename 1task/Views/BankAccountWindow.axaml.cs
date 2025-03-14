using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BasicMvvmSample.Views
{
    public partial class BankAccountWindow : Window
    {
        public BankAccountWindow()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
