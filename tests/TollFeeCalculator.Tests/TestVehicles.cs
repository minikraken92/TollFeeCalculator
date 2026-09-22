using TollFeeCalculator;

namespace TollFeeCalculator.Tests;

public sealed class GenericVehicle : Vehicle
{
    private readonly string _vehicleType;

    public GenericVehicle(string vehicleType, bool isTollFree)
    {
        _vehicleType = vehicleType;
        IsTollFree = isTollFree;
    }

    public string GetVehicleType()
    {
        return _vehicleType;
    }

    public bool IsTollFree { get; }
}
