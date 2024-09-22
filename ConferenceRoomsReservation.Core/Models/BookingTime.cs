using ConferenceRoomsReservation.Core.Shared;
using CSharpFunctionalExtensions;

namespace ConferenceRoomsReservation.Core.Models;

public class BookingTime : ValueObject
{
    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Day { get; private set; }
    public int Hours { get; private set; }
    public int Minutes { get; private set; }

    private BookingTime(
        int year, 
        int month, 
        int day, 
        int hours, 
        int minutes)
    {
        Year = year;
        Month = month;
        Day = day;
        Hours = hours;
        Minutes = minutes;
    }

    public static Result<BookingTime, Error> Create(int year, int month, int day, int hours, int minutes)
    {
        if (year <= 0)
            return Errors.General.InvalidLength("Year");

        if (month < 1 || month > 12)
            return Errors.General.InvalidLength("Month");

        if (day < 1 || day > DateTime.DaysInMonth(year, month))
            return Errors.General.InvalidLength("Day");

        if (hours < 0 || hours > 23)
            return Errors.General.InvalidLength("Hours");

        if (minutes < 0 || minutes > 59)
            return Errors.General.InvalidLength("Minutes");

        return new BookingTime(year, month, day, hours, minutes);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
        yield return Month;
        yield return Day;
        yield return Hours;
        yield return Minutes;
    }
}
