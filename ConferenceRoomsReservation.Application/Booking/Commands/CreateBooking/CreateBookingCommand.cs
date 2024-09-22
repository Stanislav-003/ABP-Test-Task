using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.Booking.Commands.CreateBooking;

public sealed record CreateBookingCommand(
    Guid conferenceRoomId,
    int year,
    int month,
    int day,
    int hours,
    int minutes,
    double durationHours,
    List<Guid> addServiceIds) : IRequest<Result<decimal>>;
