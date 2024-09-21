namespace ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;

public sealed record class AddServicesResponse(Guid Id,
    string Name,
    decimal Price);
