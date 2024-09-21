using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.ConferenceRoom.Commands.CreateConferenceRoom;

public sealed record CreateConferenceRoomCommand(
    string name,
    int capacity,
    decimal basePricePerHour,
    List<Guid> addServiceIds) : IRequest<Result<Guid>>;
