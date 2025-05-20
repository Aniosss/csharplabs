using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CandyProductionApp.Models;

public abstract class EquipmentBase : IEquipment, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void Set<T>(ref T field, T value, [CallerMemberName] string? prop = null)
    {
        if (!Equals(field, value))
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public abstract string EquipmentName { get; }
    public abstract string Status         { get; protected set; }

    public abstract void Start();
    public abstract void Stop();
}
