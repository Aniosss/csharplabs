using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;


namespace CandyProductionApp.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? p = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}
