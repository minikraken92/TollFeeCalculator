using TollFeeCalculator;

namespace TollFeeCalculator.Tests;

public class VehicleTests
{
    [Fact]
    public void Car_IsNotTollFree()
    {
        Assert.False(new Car().IsTollFree);
    }

    [Fact]
    public void Motorbike_IsTollFree()
    {
        Assert.True(new Motorbike().IsTollFree);
    }

    [Fact]
    public void Tractor_IsTollFree()
    {
        Assert.True(new Tractor().IsTollFree);
    }

    [Fact]
    public void Emergency_IsTollFree()
    {
        Assert.True(new Emergency().IsTollFree);
    }

    [Fact]
    public void Diplomat_IsTollFree()
    {
        Assert.True(new Diplomat().IsTollFree);
    }

    [Fact]
    public void Foreign_IsTollFree()
    {
        Assert.True(new Foreign().IsTollFree);
    }

    [Fact]
    public void Military_IsTollFree()
    {
        Assert.True(new Military().IsTollFree);
    }

    // Every class below has its own IsTollFree test above. If this fails, a new
    // Vehicle implementation was added without a matching test in this file.
    private static readonly Type[] CoveredVehicleTypes =
    {
        typeof(Car),
        typeof(Motorbike),
        typeof(Tractor),
        typeof(Emergency),
        typeof(Diplomat),
        typeof(Foreign),
        typeof(Military),
    };

    [Fact]
    public void AllVehicleImplementations_AreCoveredByAVehicleTest()
    {
        List<Type> uncoveredTypes = typeof(Vehicle).Assembly.GetTypes()
            .Where(t => typeof(Vehicle).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
            .Except(CoveredVehicleTypes)
            .ToList();

        Assert.True(uncoveredTypes.Count == 0,
            $"Add an IsTollFree test (and register it in CoveredVehicleTypes) for: {string.Join(", ", uncoveredTypes.Select(t => t.Name))}");
    }
}
