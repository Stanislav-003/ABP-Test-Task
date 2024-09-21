namespace ConferenceRoomsReservation.Application.ConferenceRoom.Commands.CreateConferenceRoom;

public sealed record CreateConferenceRoomRequest(
    string name,
    int capacity,
    decimal basePricePerHour,
    List<Guid> addServiceIds);
