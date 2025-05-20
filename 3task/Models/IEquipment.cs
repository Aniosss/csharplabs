namespace CandyProductionApp.Models;
public interface IEquipment
{
    string EquipmentName { get; }
    string Status { get; }
    void Start();
    void Stop();
}
