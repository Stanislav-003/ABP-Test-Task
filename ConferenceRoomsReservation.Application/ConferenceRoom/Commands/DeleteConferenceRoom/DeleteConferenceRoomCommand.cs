using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.ConferenceRoom.Commands.DeleteConferenceRoom;

public sealed record DeleteConferenceRoomCommand(
    Guid conferenceRoomId) : IRequest<Result<string>>;
