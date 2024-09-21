using ConferenceRoomsReservation.Application.Abstractions.Data;
using ConferenceRoomsReservation.DataAccess.Abstractions;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.ConferenceRoom.Commands.UpdateConferenceRoom;

public sealed class UpdateConferenceRoomCommandHandler : IRequestHandler<UpdateConferenceRoomCommand, Result<string>>
{
    private readonly IConferenceRoomRepository _conferenceRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateConferenceRoomCommandHandler(
        IConferenceRoomRepository conferenceRoomRepository, 
        IUnitOfWork unitOfWork)
    {
        _conferenceRoomRepository = conferenceRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateConferenceRoomCommand request, CancellationToken cancellationToken)
    {
        var updateResult = await _conferenceRoomRepository.UpdateAsync(
            request.conferenceRoomId,
            request.name,
            request.capacity,
            request.basePricePerHour,
            request.addServiceIds);

        return updateResult.IsFailure
            ? Result.Failure<string>(updateResult.Error)
            : Result.Success("Conference room updated successfully.");
    }
}
