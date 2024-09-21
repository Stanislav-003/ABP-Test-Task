using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.ConferenceRoom.Commands.UpdateConferenceRoom;

public sealed record UpdateConferenceRoomCommand(
    Guid conferenceRoomId,
    string? name = null,
    int? capacity = null,
    decimal? basePricePerHour = null,
    List<Guid>? addServiceIds = null) : IRequest<Result<string>>;