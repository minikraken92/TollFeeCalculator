using System;
using TollFeeCalculator;

namespace TollFeeCalculator.Tests;

public class TollCalculatorTests
{
    // A regular, non-holiday Monday in 2013.
    private static readonly DateTime Weekday = new DateTime(2013, 1, 7);

    private static DateTime At(int hour, int minute)
    {
        return Weekday.Date.AddHours(hour).AddMinutes(minute);
    }

    [Theory]
    [InlineData(6, 0, 8)]
    [InlineData(6, 29, 8)]
    [InlineData(6, 30, 13)]
    [InlineData(6, 59, 13)]
    [InlineData(7, 0, 18)]
    [InlineData(7, 59, 18)]
    [InlineData(8, 0, 13)]
    [InlineData(8, 29, 13)]
    [InlineData(8, 30, 8)]
    [InlineData(8, 59, 8)]
    [InlineData(9, 30, 8)]
    [InlineData(14, 30, 8)]
    [InlineData(14, 59, 8)]
    [InlineData(15, 0, 13)]
    [InlineData(15, 29, 13)]
    [InlineData(15, 30, 18)]
    [InlineData(16, 0, 18)]
    [InlineData(16, 59, 18)]
    [InlineData(17, 0, 13)]
    [InlineData(17, 59, 13)]
    [InlineData(18, 0, 8)]
    [InlineData(18, 29, 8)]
    [InlineData(18, 30, 0)]
    [InlineData(21, 0, 0)]
    [InlineData(2, 0, 0)]
    public void GetTollFee_ForSinglePass_MatchesFeeTableForTimeOfDay(int hour, int minute, int expectedFee)
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();

        int fee = calculator.GetTollFee(At(hour, minute), car);

        Assert.Equal(expectedFee, fee);
    }

    // These two slots (9:00-9:29 and 11:00-14:29) fell between the fee table's
    // if/else branches and were never matched, so they are charged as free even
    // though they sat inside otherwise-tolled hours. 
    // Now controll them so they are not free.
    [Theory]
    [InlineData(9, 15)]
    [InlineData(11, 0)]
    [InlineData(14, 0)]
    public void GetTollFee_ForSinglePass_HadUncoveredGapsInsideRushHours(int hour, int minute)
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();

        int fee = calculator.GetTollFee(At(hour, minute), car);

        Assert.NotEqual(0, fee);
    }

    [Fact]
    public void GetTollFee_ForVehicleWithIsTollFreeTrue_IsAlwaysZero()
    {
        // TollCalculator only cares about Vehicle.IsTollFree, not the concrete
        // type - each vehicle class's own IsTollFree value is covered by
        // VehicleTests instead.
        TollCalculator calculator = new TollCalculator();
        GenericVehicle vehicle = new GenericVehicle("AnyType", isTollFree: true);

        int fee = calculator.GetTollFee(At(7, 0), vehicle);

        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_ForVehicleWithIsTollFreeFalse_IsChargedNormally()
    {
        TollCalculator calculator = new TollCalculator();
        GenericVehicle vehicle = new GenericVehicle("AnyType", isTollFree: false);

        int fee = calculator.GetTollFee(At(7, 0), vehicle);

        Assert.Equal(18, fee);
    }

    [Fact]
    public void GetTollFee_ForCarDuringRushHour_IsCharged()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();

        int fee = calculator.GetTollFee(At(7, 0), car);

        Assert.Equal(18, fee);
    }

    [Theory]
    [InlineData(2013, 1, 5)]  // Saturday
    [InlineData(2013, 1, 6)]  // Sunday
    public void GetTollFee_OnWeekend_IsAlwaysZero(int year, int month, int day)
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime date = new DateTime(year, month, day, 7, 0, 0);

        int fee = calculator.GetTollFee(date, car);

        Assert.Equal(0, fee);
    }

    [Theory]
    [InlineData(2013, 1, 1)]   // New Year's Day
    [InlineData(2013, 3, 29)]  // Good Friday
    [InlineData(2013, 5, 1)]   // May Day
    [InlineData(2013, 6, 21)]  // Midsummer's Eve
    [InlineData(2013, 7, 01)]  // Whole of July is toll-free
    [InlineData(2013, 7, 15)]  // Whole of July is toll-free
    [InlineData(2013, 7, 31)]  // Whole of July is toll-free
    [InlineData(2013, 12, 24)] // Christmas Eve
    public void GetTollFee_OnPublicHoliday_IsAlwaysZero(int year, int month, int day)
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime date = new DateTime(year, month, day, 7, 0, 0);

        int fee = calculator.GetTollFee(date, car);

        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_ForOrdinaryWeekdayOutsideJulyAndHolidays_IsCharged()
    {
        // IsTollFreeDate has no year-locked cases anymore: weekends, real public
        // holidays, the day before a holiday, and all of July are toll-free in
        // any year. This is a sanity check that an otherwise ordinary weekday
        // (outside all of those) still gets charged normally.
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime date = new DateTime(2014, 8, 20, 7, 0, 0);

        int fee = calculator.GetTollFee(date, car);

        Assert.Equal(18, fee);
    }

    [Theory]
    [InlineData(2015, 1, 6)]  // Epiphany - a weekday in 2015, unlike in 2013
    [InlineData(2016, 6, 6)]  // National Day - a weekday in 2016, unlike in 2013
    public void GetTollFee_OnPublicHolidayInYearsOtherThan2013_IsAlsoZero(int year, int month, int day)
    {
        // These come from the SwedenPublicHoliday library, not the 2013
        // hard-coded list, and so demonstrate the calculator now recognizes
        // real Swedish public holidays in any year, not just 2013.
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime date = new DateTime(year, month, day, 7, 0, 0);

        int fee = calculator.GetTollFee(date, car);

        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_Daily_ForSinglePass_ReturnsThatPassesFee()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes = { At(7, 0) };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(18, fee);
    }

    [Fact]
    public void GetTollFee_Daily_ForTwoPassesCloseTogether_ChargesOnlyTheHigherFee()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes = { At(6, 15), At(7, 15) };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(18, fee);
    }

    [Fact]
    public void GetTollFee_Daily_PassesOutOfChronologicalOrder_MergeWithinHourWindow()
    {
        // Same two passes as GetTollFee_Daily_ForTwoPassesCloseTogether_ChargesOnlyTheHigherFee
        // (06:15 and 07:15, 60 minutes apart), but given latest-first. GetTollFee walks the
        // array in the given order and merges into the running interval when the next
        // date is later than intervalStart, so this out-of-order pair is  merging into the higher single fee (18) 
        // instead of charged as two separate passes (8 + 18 = 26).
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes = { At(7, 15), At(6, 15) };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(18, fee);
    }

    [Fact]
    public void GetTollFee_Daily_ForTwoPassesCloseTogetherAndFirstIsOutOfHourScope_ChargesOnlyTheHigherFee()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes = { At(6, 15), At(7, 45), At(8, 45) };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(26, fee);
    }
    [Fact]
    public void GetTollFee_Daily_ForTwoPassesCloseTogetherAndFirstAndLastIsOutOfHourScope_ChargesOnlyTheHigherFeeOfTheInterval()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes = { At(6, 15), At(7, 45), At(8, 45), At(17, 45) };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(39, fee);
    }
    [Fact]
    public void GetTollFee_Daily_ForTwoPassesCloseTogetherAndFirstAndLastIsOutOfHourScopeButLastIsInScopeOfSecondToLast_ChargesOnlyTheHigherFeeOfTheInterval()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes = { At(6, 15), At(7, 45), At(8, 45), At(9, 15) };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(34, fee);
    }
    [Fact]
    public void GetTollFee_Daily_ForTwoPassesFarApart_StillOnlyChargesTheHigherFee()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes = { At(7, 15), At(15, 15) };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(31, fee);
    }

    [Fact]
    public void GetTollFee_Daily_CapsTotalAtSixty()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes =
        {
            At(2, 0),
            At(7, 0),
            At(8, 5),
            At(9, 10),
            At(10, 15),
            At(11, 30),
            At(15, 0),
            At(16, 15)
        };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(60, fee);
    }


    [Fact]
    public void GetTollFee_Daily_ForTollFreeVehicle_IsAlwaysZeroAcrossManyPasses()
    {
        TollCalculator calculator = new TollCalculator();
        GenericVehicle vehicle = new GenericVehicle("Emergency", isTollFree: true);
        DateTime[] passes = { At(6, 15), At(7, 15), At(15, 45), At(17, 0) };

        int fee = calculator.GetTollFee(vehicle, passes);

        Assert.Equal(0, fee);
    }

    [Fact]
    public void GetTollFee_Daily_OnTollFreeDay_IsAlwaysZeroAcrossManyPasses()
    {
        TollCalculator calculator = new TollCalculator();
        Car car = new Car();
        DateTime[] passes =
        {
            new DateTime(2013, 1, 5, 7, 0, 0),
            new DateTime(2013, 1, 5, 15, 45, 0),
        };

        int fee = calculator.GetTollFee(car, passes);

        Assert.Equal(0, fee);
    }
}
