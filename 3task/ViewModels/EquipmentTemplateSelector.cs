using Avalonia;          
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;               
using CandyProductionApp.Models;

namespace CandyProductionApp.ViewModels;

public sealed class EquipmentTemplateSelector : IDataTemplate
{
    public bool SupportsRecycling => false;

    public Control Build(object? data)
    {
        if (data is Factory)
        {
            var tb = new TextBlock();
            tb.Bind(TextBlock.TextProperty,
                   (IBinding)new Binding("SugarAmount")
                   { StringFormat = "Sugar: {0}" });
            return tb;
        }
        else if (data is Loader)
        {
            var tb = new TextBlock();
            tb.Bind(TextBlock.TextProperty,
                   (IBinding)new Binding("LoadedContainers")
                   { StringFormat = "Loaded: {0}" });
            return tb;
        }

        return new TextBlock { Text = "Unknown" };
    }

    public bool Match(object? data) => data is IEquipment;
}
