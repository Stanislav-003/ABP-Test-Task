namespace ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;

public sealed record GetRoomResponse(
    Guid Id,
    string Name,
    int Capacity,
    decimal BasePricePerHour,
    List<AddServicesResponse> Services);