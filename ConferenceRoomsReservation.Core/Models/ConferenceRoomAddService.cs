namespace ConferenceRoomsReservation.Core.Models;

public class ConferenceRoomAddService
{
    public Guid ConferenceRoomId { get; set; }
    public ConferenceRoom ConferenceRoom { get; set; }

    public Guid AddServiceId { get; set; }
    public AddService AddService { get; set; }
}
