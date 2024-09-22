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
        // Перевірка наявності залу
        var conferenceRoom = await _dataBaseContext.ConferenceRooms
            .Include(cr => cr.ConferenceRoomAddServices)
            .FirstOrDefaultAsync(cr => cr.Id == conferenceRoomId);

        if (conferenceRoom == null)
            return Errors.General.NotFound(conferenceRoomId); // Якщо зал не знайдений

        // Перевіряємо чи зал вже заброньований на цей час
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
            return Errors.General.ValueIsInvalid(); // Якщо зал вже заброньований на цей час

        decimal totalPrice = 0;
        decimal basePrice = conferenceRoom.BasePricePerHour;

        // розрахунок вартості оренди
        for (var currentHour = bookingTime.Hours; currentHour < bookingTime.Hours + duration.TotalHours; currentHour++)
        {
            if (currentHour >= 6 && currentHour < 9)
                totalPrice += basePrice * 0.9m; // ранкові години (знижка 10%)
            else if (currentHour >= 9 && currentHour < 12)
                totalPrice += basePrice; // стандартні години
            else if (currentHour >= 12 && currentHour < 14)
                totalPrice += basePrice * 1.15m; // пікові години (націнка 15%)
            else if (currentHour >= 14 && currentHour < 18)
                totalPrice += basePrice; // стандартні години
            else if (currentHour >= 18 && currentHour < 23)
                totalPrice += basePrice * 0.8m; // вечірні години (знижка 20%)
            else
                totalPrice += basePrice; // усі інші години
        }

        // Додавання вартості обраних послуг
        var selectedServices = await _dataBaseContext.Services
            .Where(s => selectedServiceIds.Contains(s.Id))
            .ToListAsync();

        if (selectedServices == null || selectedServices.Count == 0)
            return Errors.General.NotFound("Selected services not found");

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
