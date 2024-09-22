using ConferenceRoomsReservation.Application.Abstractions.Messaging;
using MediatR;

namespace ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;

public sealed record GetRoomsQuery(
    int year,
    int month,
    int day,
    int hours,
    double durationHours,
    int minutes,
    int requiredCapacity) : IQuery<GetRoomsResponse>;
