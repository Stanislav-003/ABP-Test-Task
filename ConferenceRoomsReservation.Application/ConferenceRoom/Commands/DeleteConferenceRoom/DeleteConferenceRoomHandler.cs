using ConferenceRoomsReservation.Application.Abstractions.Data;
using ConferenceRoomsReservation.DataAccess.Abstractions;
using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.ConferenceRoom.Commands.DeleteConferenceRoom;

public class DeleteConferenceRoomHandler : IRequestHandler<DeleteConferenceRoomCommand, Result<string>>
{
    private readonly IConferenceRoomRepository _conferenceRoomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteConferenceRoomHandler(
        IConferenceRoomRepository conferenceRoomRepository, 
        IUnitOfWork unitOfWork)
    {
        _conferenceRoomRepository = conferenceRoomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteConferenceRoomCommand request, CancellationToken cancellationToken)
    {
        var deleteResult = await _conferenceRoomRepository.DeleteAsync(request.conferenceRoomId);

        if (deleteResult.IsFailure)
        {
            return Result.Failure<string>(deleteResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success("Conference room deleted successfully.");
    }
}
