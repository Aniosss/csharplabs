using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AircraftApp.Views
{
    public partial class AircraftView : UserControl
    {
        public AircraftView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
