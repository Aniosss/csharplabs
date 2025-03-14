using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BasicMvvmSample.Views
{
    public partial class BankAccountView : UserControl
    {
        public BankAccountView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
