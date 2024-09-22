namespace ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;

public sealed record class GetRoomsRequest(
    int year,
    int month,
    int day,
    int hours,
    int minutes,
    int requiredCapacity);
