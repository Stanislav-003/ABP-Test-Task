namespace ConferenceRoomsReservation.Application.ConferenceRoom.Commands.UpdateConferenceRoom;

public sealed record class UpdateConferenceRoomRequest(
    Guid conferenceRoomId,
    string? name = null,
    int? capacity = null,
    decimal? basePricePerHour = null,
    List<Guid>? addServiceIds = null);
