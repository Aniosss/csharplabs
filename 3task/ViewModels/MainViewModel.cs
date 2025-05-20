using CandyProductionApp.Helpers;
using CandyProductionApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;


namespace CandyProductionApp.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    int _counter = 1;
    public ObservableCollection<IEquipment> Equipments { get; } = new();

    public ICommand AddFactoryCommand { get; }
    public ICommand AddLoaderCommand  { get; }

    public MainViewModel()
    {
        AddFactoryCommand = new RelayCommand(AddFactory);
        AddLoaderCommand  = new RelayCommand(AddLoader);
    }

    void AddFactory()
    {
        var f = new Factory($"Factory #{_counter++}", 100);
        f.SugarFinished    += (_,__) => {};
        f.AccidentOccurred += (_,__) => { };
        f.Start();
        Equipments.Add(f);
    }

    void AddLoader()
    {
        var l = new Loader($"Loader #{_counter++}");
        l.AccidentOccurred += (_,__) => { };
        l.Start();
        Equipments.Add(l);
    }
}
