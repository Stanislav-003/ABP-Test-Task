using ConferenceRoomsReservation.Core.Shared;
using CSharpFunctionalExtensions;
using System.Reflection.Metadata.Ecma335;

namespace ConferenceRoomsReservation.Core.Models;

public class AddService : Entity<Guid>
{
    public const int MAX_NAME_LENGTH = 100;

    public AddService() { }

    private AddService(
        Guid id, 
        string name, 
        decimal price) : base(id)
    {
        Name = name;
        Price = price;
        ConferenceRoomAddServices = new List<ConferenceRoomAddService>();
    }

    public string Name { get; }
    public decimal Price { get; }
    public ICollection<ConferenceRoomAddService> ConferenceRoomAddServices { get; }

    public static Result<AddService, Error> Create(
        Guid id,
        string name,
        decimal price)
    { 
        if (id == Guid.Empty)
            return Errors.General.ValueIsRequired();

        if (string.IsNullOrWhiteSpace(name) == false || name.Length > MAX_NAME_LENGTH)
            return Errors.General.InvalidLength("name");

        if (price < 0)
            return Errors.General.InvalidLengthValue("price");

        return new AddService(id, name, price);
    }
}
