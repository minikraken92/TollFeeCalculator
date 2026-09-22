using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PublicHoliday;
using TollFeeCalculator;

public class TollCalculator
{
    private static readonly SwedenPublicHoliday SwedenHolidays = new SwedenPublicHoliday();

    // Each bracket owns an explicit start/end, so adding, removing or
    // reordering one can't silently change what its neighbors cover the way
    // a cascading if/else-if chain can.
    internal static readonly TollFeeBracket[] FeeSchedule =
    {
        new(new TimeOnly(0, 0), new TimeOnly(6, 0), 0),
        new(new TimeOnly(6, 0), new TimeOnly(6, 30), 8),
        new(new TimeOnly(6, 30), new TimeOnly(7, 0), 13),
        new(new TimeOnly(7, 0), new TimeOnly(8, 0), 18),
        new(new TimeOnly(8, 0), new TimeOnly(8, 30), 13),
        new(new TimeOnly(8, 30), new TimeOnly(15, 0), 8),
        new(new TimeOnly(15, 0), new TimeOnly(15, 30), 13),
        new(new TimeOnly(15, 30), new TimeOnly(17, 0), 18),
        new(new TimeOnly(17, 0), new TimeOnly(18, 0), 13),
        new(new TimeOnly(18, 0), new TimeOnly(18, 30), 8),
        new(new TimeOnly(18, 30), null, 0),
    };


    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        dates.Sort();
        dates = RemoveDuplicatePasses(dates);
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        int addingFee = 0;
        foreach (DateTime date in dates)
        {
            int currentFee = GetTollFee(date, vehicle);

            double diffInMinutes = (date - intervalStart).TotalMinutes;

            if (diffInMinutes <= 60 && diffInMinutes > 0)
            {
                addingFee = Math.Max(addingFee, currentFee);
            }
            else
            {
                totalFee += addingFee;
                addingFee = currentFee;
                intervalStart = date;
            }
        }
        totalFee += addingFee;

        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    // The same vehicle can't be at two toll cameras at once, so passes this
    // close together are the same physical pass read twice, not two passes.
    private static DateTime[] RemoveDuplicatePasses(DateTime[] sortedDates)
    {
        List<DateTime> distinctDates = new List<DateTime>();
        DateTime? lastCountedDate = null;

        foreach (DateTime date in sortedDates)
        {
            if (lastCountedDate == null || (date - lastCountedDate.Value).TotalSeconds >= 5)
            {
                distinctDates.Add(date);
                lastCountedDate = date;
            }
        }

        return distinctDates.ToArray();
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return vehicle != null && vehicle.IsTollFree;
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        TimeOnly time = TimeOnly.FromDateTime(date);

        return FeeSchedule.First(bracket => bracket.Contains(time)).Fee;
    }

    private Boolean IsTollFreeDate(DateTime date)//doesent take alla helgons dag
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (SwedenHolidays.IsPublicHoliday(date)) return true;
        if (SwedenHolidays.IsPublicHoliday(date.AddDays(1))) return true;

        if(date.Month == 7) return true;

        return false;
    }
}