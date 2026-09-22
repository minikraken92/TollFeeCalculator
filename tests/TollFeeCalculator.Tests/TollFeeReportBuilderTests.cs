using System;
using System.Collections.Generic;
using TollFeeCalculator;

namespace TollFeeCalculator.Tests;

public class TollFeeReportBuilderTests
{
    private static DateTime At(int year, int month, int day, int hour, int minute)
    {
        return new DateTime(year, month, day, hour, minute, 0);
    }

    [Fact]
    public void GetDailyFees_GroupsPassesByCalendarDay()
    {
        TollFeeReportBuilder reportBuilder = new TollFeeReportBuilder(new TollCalculator());
        Car car = new Car();
        DateTime[] passes =
        {
            At(2014, 8, 20, 6, 15),
            At(2014, 8, 20, 7, 15),
            At(2014, 8, 21, 7, 0),
        };

        IReadOnlyList<DailyTollFee> dailyFees = reportBuilder.GetDailyFees(car, passes);

        Assert.Equal(2, dailyFees.Count);
        Assert.Equal(new DateTime(2014, 8, 20), dailyFees[0].Date);
        Assert.Equal(18, dailyFees[0].Fee);
        Assert.Equal(new DateTime(2014, 8, 21), dailyFees[1].Date);
        Assert.Equal(18, dailyFees[1].Fee);
    }

    [Fact]
    public void GetDailyFees_ReturnsDaysInChronologicalOrderRegardlessOfInputOrder()
    {
        TollFeeReportBuilder reportBuilder = new TollFeeReportBuilder(new TollCalculator());
        Car car = new Car();
        DateTime[] passes =
        {
            At(2014, 8, 21, 7, 0),
            At(2014, 8, 20, 7, 0),
        };

        IReadOnlyList<DailyTollFee> dailyFees = reportBuilder.GetDailyFees(car, passes);

        Assert.Equal(new DateTime(2014, 8, 20), dailyFees[0].Date);
        Assert.Equal(new DateTime(2014, 8, 21), dailyFees[1].Date);
    }

    [Fact]
    public void GetMonthlySummaries_SumsDailyFeesWithinTheSameMonth()
    {
        TollFeeReportBuilder reportBuilder = new TollFeeReportBuilder(new TollCalculator());
        DailyTollFee[] dailyFees =
        {
            new DailyTollFee(new DateTime(2014, 8, 20), 18),
            new DailyTollFee(new DateTime(2014, 8, 21), 8),
            new DailyTollFee(new DateTime(2014, 9, 1), 13),
        };

        IReadOnlyList<MonthlyTollFeeSummary> summaries = reportBuilder.GetMonthlySummaries(dailyFees);

        Assert.Equal(2, summaries.Count);
        Assert.Equal(2014, summaries[0].Year);
        Assert.Equal(8, summaries[0].Month);
        Assert.Equal(26, summaries[0].TotalFee);
        Assert.Equal(2014, summaries[1].Year);
        Assert.Equal(9, summaries[1].Month);
        Assert.Equal(13, summaries[1].TotalFee);
    }

    [Fact]
    public void GetDailyFeesThenGetMonthlySummaries_EndToEndAcrossTwoMonths()
    {
        TollFeeReportBuilder reportBuilder = new TollFeeReportBuilder(new TollCalculator());
        Car car = new Car();
        DateTime[] passes =
        {
            At(2014, 8, 20, 7, 0),  // 18 kr
            At(2014, 8, 21, 18, 0), // 8 kr
            At(2014, 9, 1, 17, 0),  // 13 kr
        };

        IReadOnlyList<DailyTollFee> dailyFees = reportBuilder.GetDailyFees(car, passes);
        IReadOnlyList<MonthlyTollFeeSummary> summaries = reportBuilder.GetMonthlySummaries(dailyFees);

        Assert.Equal(3, dailyFees.Count);
        Assert.Equal(2, summaries.Count);
        Assert.Equal(26, summaries[0].TotalFee);
        Assert.Equal(13, summaries[1].TotalFee);
    }
}
