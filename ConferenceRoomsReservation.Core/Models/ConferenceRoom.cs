using ConferenceRoomsReservation.Core.Shared;
using CSharpFunctionalExtensions;

namespace ConferenceRoomsReservation.Core.Models;

public class ConferenceRoom : Entity<Guid>
{
    public const int MAX_NAME_LENGTH = 200;
    public const int MAX_CAPACITY_VALUE = 500;

    private ConferenceRoom(
        Guid id,
        string name,
        int capacity,
        decimal basePricePerHour) : base(id)
    {
        Name = name;
        Capacity = capacity;
        BasePricePerHour = basePricePerHour;
        ConferenceRoomAddServices = new List<ConferenceRoomAddService>();
    }

    public string Name { get; }
    public int Capacity { get; }
    public decimal BasePricePerHour { get; }
    public ICollection<ConferenceRoomAddService> ConferenceRoomAddServices { get; }

    public static Result<ConferenceRoom, Error> Create(
        string name,
        int capacity,
        decimal basePricePerHour)
    {
        if (string.IsNullOrWhiteSpace(name) != false || name.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidLength("name");

        if (capacity < 0 || capacity > MAX_CAPACITY_VALUE)
            return Errors.General.InvalidLengthValue("capacity");

        if (basePricePerHour < 0)
            return Errors.General.InvalidLengthValue("base price per hour");

        return new ConferenceRoom(Guid.NewGuid(), name, capacity, basePricePerHour);
    }
}
