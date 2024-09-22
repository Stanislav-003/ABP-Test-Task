namespace ConferenceRoomsReservation.Application.Booking.Commands.CreateBooking;

public sealed record CreateBookingRequest(
    Guid conferenceRoomId,
    int year,
    int month,
    int day,
    int hours,
    int minutes,
    double durationHours,
    List<Guid> addServiceIds);
