using System;
using System.Threading;
using System.Threading.Tasks;

namespace CandyProductionApp.Models;

public sealed class Loader : EquipmentBase
{
    public override string EquipmentName { get; }
    private string _status = "Stopped";
    public override string Status { get => _status; protected set => Set(ref _status, value); }

    private int _loadedContainers;
    public int LoadedContainers { get => _loadedContainers; private set => Set(ref _loadedContainers, value); }

    readonly Random _rnd = new();
    CancellationTokenSource? _cts;

    public event EventHandler? AccidentOccurred;

    public Loader(string name, int firstBatch = 50)
    {
        EquipmentName    = name;
        LoadedContainers = firstBatch;  
    }

    public override void Start()
    {
        _cts = new CancellationTokenSource();
        Status = "Running";
        Task.Run(() => Loop(_cts.Token));
    }
    public override void Stop() => _cts?.Cancel();

    async Task Loop(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            // - контейнер каждую секунду
            LoadedContainers--;

            if (LoadedContainers <= 0)
            {
                Status = "Empty – taking new batch";
                LoadedContainers = _rnd.Next(30, 60); 
            }
            else
            {
                Status = "Unloading";
            }

            // 5 % авария
            if (_rnd.NextDouble() < 0.05)
            {
                Status = "Accident";
                AccidentOccurred?.Invoke(this, EventArgs.Empty);
            }

            await Task.Delay(1000, ct);
        }
    }
}
