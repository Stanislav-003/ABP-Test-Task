using ConferenceRoomsReservation.Application.Abstractions.Messaging;
using MediatR;

namespace ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;

public sealed record GetRoomsQuery(
    int year,
    int month,
    int day,
    int hours,
    int minutes,
    TimeSpan startTime,
    TimeSpan endTime,
    int requiredCapacity) : IQuery<GetRoomsResponse>;
