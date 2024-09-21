using ConferenceRoomsReservation.Core.Models;

namespace ConferenceRoomsReservation.DataAccess.Entities;

public class BookingEntity
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingTime BookingTime { get; set; } = null!;
    public TimeSpan Duration { get; set; }
    public Guid ConferenceRoomId { get; set; }
    public ConferenceRoomEntity ConferenceRoom { get; set; } = null!;
}
