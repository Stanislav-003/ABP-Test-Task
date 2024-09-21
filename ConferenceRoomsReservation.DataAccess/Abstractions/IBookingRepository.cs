using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.Core.Shared;
using CSharpFunctionalExtensions;

namespace ConferenceRoomsReservation.DataAccess.Abstractions;

public interface IBookingRepository
{
    Task<Result<decimal, Error>> BookRoomAsync(Guid conferenceRoomId, BookingTime bookingTime, TimeSpan duration, List<Guid> selectedServiceIds);
}
