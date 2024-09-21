using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.Core.Shared;
using ConferenceRoomsReservation.DataAccess.Abstractions;
using ConferenceRoomsReservation.DataAccess.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsReservation.DataAccess.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public BookingRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<Result<decimal, Error>> BookRoomAsync(
        Guid conferenceRoomId,
        BookingTime bookingTime,
        TimeSpan duration,
        List<Guid> selectedServiceIds)
    {
        var conferenceRoom = await _dataBaseContext.ConferenceRooms
            .Include(cr => cr.ConferenceRoomAddServices)
            .FirstOrDefaultAsync(cr => cr.Id == conferenceRoomId);

        if (conferenceRoom == null)
            return Errors.General.NotFound(conferenceRoomId);

        var isRoomBooked = await _dataBaseContext.Bookings
            .AnyAsync(b => b.ConferenceRoomId == conferenceRoomId &&
                           b.BookingTime.Year == bookingTime.Year &&
                           b.BookingTime.Month == bookingTime.Month &&
                           b.BookingTime.Day == bookingTime.Day &&
                           ((b.BookingTime.Hours < bookingTime.Hours + duration.Hours &&
                             b.BookingTime.Hours + b.Duration.Hours > bookingTime.Hours) ||
                            (b.BookingTime.Hours + b.Duration.Hours > bookingTime.Hours &&
                             b.BookingTime.Hours < bookingTime.Hours + duration.Hours)));

        if (isRoomBooked)
            return Errors.General.ValueIsInvalid(); // Зал уже забронирован на это время

        decimal totalPrice = conferenceRoom.BasePricePerHour * (decimal)duration.TotalHours;

        var selectedServices = await _dataBaseContext.Services
            .Where(s => selectedServiceIds.Contains(s.Id))
            .ToListAsync();

        totalPrice += selectedServices.Sum(s => s.Price);

        var booking = new BookingEntity
        {
            Id = Guid.NewGuid(),
            TotalPrice = totalPrice,
            BookingTime = bookingTime,
            Duration = duration,
            ConferenceRoomId = conferenceRoomId
        };

        _dataBaseContext.Bookings.Add(booking);
        await _dataBaseContext.SaveChangesAsync();

        return Result.Success<decimal, Error>(totalPrice);
    }
}
