using ConferenceRoomsReservation.Core.Shared;
using CSharpFunctionalExtensions;

namespace ConferenceRoomsReservation.Core.Models;

public class Booking : Entity<Guid>
{
    public const int MAX_TOTALPRICE_VALUE = 200000;

    private readonly List<AddService> _addService = [];

    public Booking()
    {
        
    }

    private Booking(
        Guid id,
        BookingTime bookingTime,
        TimeSpan duration,
        Guid conferenceRoomId,
        IEnumerable<AddService> addServices) : base(id)
    {
        BookingTime = bookingTime;
        Duration = duration;
        ConferenceRoomId = conferenceRoomId;
        _addService = addServices.ToList();
    }

    public decimal TotalPrice { get; }
    public BookingTime BookingTime { get; }
    public TimeSpan Duration { get; }
    public Guid ConferenceRoomId { get; }
    public ConferenceRoom ConferenceRoom { get; }
    public IReadOnlyList<AddService> AddService => _addService;

    public static Result<Booking, Error> Create(
        Guid id,
        BookingTime bookingTime,
        TimeSpan duration,
        Guid conferenceRoomId,
        IEnumerable<AddService> addServices)
    {
        if (id == Guid.Empty)
            return Errors.General.ValueIsRequired();

        if (bookingTime == null)
            return Errors.General.ValueIsRequired();

        if (duration <= TimeSpan.Zero)
            return Errors.General.InvalidLength("Duration");

        if (conferenceRoomId == Guid.Empty)
            return Errors.General.ValueIsInvalid();

        return new Booking(id, bookingTime, duration, conferenceRoomId, addServices);
    }
}
