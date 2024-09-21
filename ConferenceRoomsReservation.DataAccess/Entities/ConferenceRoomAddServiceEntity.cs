namespace ConferenceRoomsReservation.DataAccess.Entities;

public class ConferenceRoomAddServiceEntity
{
    public Guid ConferenceRoomId { get; set; }
    public ConferenceRoomEntity ConferenceRoom { get; set; } = null!;

    public Guid AddServiceId { get; set; }
    public AddServiceEntity AddService { get; set; } = null!;
}
