using TollFeeCalculator;

namespace TollFeeCalculator.Tests;

public sealed class GenericVehicle : Vehicle
{
    private readonly string _vehicleType;

    public GenericVehicle(string vehicleType)
    {
        _vehicleType = vehicleType;
    }

    public string GetVehicleType()
    {
        return _vehicleType;
    }
}
