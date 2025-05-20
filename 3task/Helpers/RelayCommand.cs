using System.Windows.Input;
using System;                
using System.Windows.Input;


namespace CandyProductionApp.Helpers;

public sealed class RelayCommand : ICommand
{
    readonly Action _execute;
    readonly Func<bool>? _can;
    public RelayCommand(Action exec, Func<bool>? can = null)
    { _execute = exec; _can = can; }
    public bool CanExecute(object? _) => _can?.Invoke() ?? true;
    public void Execute(object? _)    => _execute();
    public event EventHandler? CanExecuteChanged;
}
