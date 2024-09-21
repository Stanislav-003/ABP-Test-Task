namespace ConferenceRoomsReservation.DataAccess.Entities;

public class AddServiceEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public ICollection<ConferenceRoomAddServiceEntity> ConferenceRoomAddServices { get; set; } = new List<ConferenceRoomAddServiceEntity>();
}
