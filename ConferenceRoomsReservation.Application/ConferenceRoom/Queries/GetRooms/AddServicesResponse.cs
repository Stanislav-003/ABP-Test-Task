namespace ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;

public sealed record AddServicesResponse(
    Guid Id,
    string Name,
    decimal Price);
