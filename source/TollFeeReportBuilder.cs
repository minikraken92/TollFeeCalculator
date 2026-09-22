using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator
{
    public class TollFeeReportBuilder
    {
        private readonly TollCalculator _tollCalculator;

        public TollFeeReportBuilder(TollCalculator tollCalculator)
        {
            _tollCalculator = tollCalculator;
        }

        public IReadOnlyList<DailyTollFee> GetDailyFees(Vehicle vehicle, IEnumerable<DateTime> passes)
        {
            return passes
                .GroupBy(pass => pass.Date)
                .OrderBy(dayGroup => dayGroup.Key)
                .Select(dayGroup => new DailyTollFee(dayGroup.Key, _tollCalculator.GetTollFee(vehicle, dayGroup.ToArray())))
                .ToList();
        }

        public IReadOnlyList<MonthlyTollFeeSummary> GetMonthlySummaries(IEnumerable<DailyTollFee> dailyFees)
        {
            return dailyFees
                .GroupBy(dailyFee => new { dailyFee.Date.Year, dailyFee.Date.Month })
                .OrderBy(monthGroup => monthGroup.Key.Year)
                .ThenBy(monthGroup => monthGroup.Key.Month)
                .Select(monthGroup => new MonthlyTollFeeSummary(monthGroup.Key.Year, monthGroup.Key.Month, monthGroup.Sum(dailyFee => dailyFee.Fee)))
                .ToList();
        }
    }
}
