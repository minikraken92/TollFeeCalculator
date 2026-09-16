using System;
using System.Globalization;
using PublicHoliday;
using TollFeeCalculator;

public class TollCalculator
{
    private static readonly SwedenPublicHoliday SwedenHolidays = new SwedenPublicHoliday();


    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
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

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        TimeOnly time = TimeOnly.FromDateTime(date);

        return time switch
        {
            _ when time.Hour < 6 => 0,
            _ when time.Hour == 6 && time.Minute <= 29 => 8,
            _ when time.Hour == 6 && time.Minute <= 59 => 13,
            _ when time.Hour == 7 => 18,
            _ when time.Hour == 8 && time.Minute <= 29 => 13,
            _ when time.Hour < 15 => 8,
            _ when time.Hour == 15 && time.Minute <= 29 => 13,
            _ when time.Hour == 15 || time.Hour == 16 => 18,
            _ when time.Hour == 17 => 13,
            _ when time.Hour == 18 && time.Minute <= 29 => 8,
            _ => 0
        };
    }

    private Boolean IsTollFreeDate(DateTime date)
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

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}