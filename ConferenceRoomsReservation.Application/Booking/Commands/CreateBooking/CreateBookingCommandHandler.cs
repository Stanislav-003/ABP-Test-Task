using ConferenceRoomsReservation.Application.ConferenceRoom.Commands.CreateConferenceRoom;
using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.DataAccess.Abstractions;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.Booking.Commands.CreateBooking;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<decimal>>
{
    private readonly IBookingRepository _bookingRepository;

    public CreateBookingCommandHandler(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Result<decimal>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingTimeResult = BookingTime.Create(
            request.year,
            request.month,
            request.day,
            request.hours,
            request.minutes);

        if (bookingTimeResult.IsFailure)
        {
            return Result.Failure<decimal>(bookingTimeResult.Error.Message);
        }

        var bookingResult = await _bookingRepository.BookRoomAsync(
            request.conferenceRoomId,
            bookingTimeResult.Value,
            TimeSpan.FromHours(request.durationHours),
            request.addServiceIds);

        if (bookingResult.IsFailure)
        {
            return Result.Failure<decimal>(bookingResult.Error.Message);
        }

        return Result.Success(bookingResult.Value);
    }
}
