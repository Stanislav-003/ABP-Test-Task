namespace ConferenceRoomsReservation.DataAccess.Entities;

public class ConferenceRoomEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public decimal BasePricePerHour { get; set; }
    public ICollection<ConferenceRoomAddServiceEntity> ConferenceRoomAddServices { get; set; } = new List<ConferenceRoomAddServiceEntity>();
}
