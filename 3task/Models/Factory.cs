using System;
using System.Threading;
using System.Threading.Tasks;

namespace CandyProductionApp.Models;

public sealed class Factory : EquipmentBase
{
    public override string EquipmentName { get; }
    private string _status = "Stopped";
    public override string Status { get => _status; protected set => Set(ref _status, value); }

    private int _sugarAmount;
    public int SugarAmount { get => _sugarAmount; private set => Set(ref _sugarAmount, value); }

    readonly Random _rnd = new();
    CancellationTokenSource? _cts;

    public event EventHandler? SugarFinished;
    public event EventHandler? AccidentOccurred;

    public Factory(string name, int initialSugar)
    {
        EquipmentName = name;
        SugarAmount   = initialSugar;
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
            // + сахар каждые 1 с
            SugarAmount += _rnd.Next(1, 4);       

            if (SugarAmount > 300) 
            {
                SugarAmount = 300;
                Status = "Full of sugar";
                SugarFinished?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Status = "Refilling";
            }

            // 10 % авария
            if (_rnd.NextDouble() < 0.10)
            {
                Status = "Accident";
                AccidentOccurred?.Invoke(this, EventArgs.Empty);
            }

            await Task.Delay(1000, ct);
        }
    }
}
