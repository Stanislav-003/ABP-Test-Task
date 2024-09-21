namespace ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;

public sealed record GetRoomsResponse(List<GetRoomResponse> AvailableRooms);