using ConferenceRoomsReservation.Application.Abstractions.Data;
using ConferenceRoomsReservation.DataAccess.Abstractions;
using CSharpFunctionalExtensions;
using ConferenceRoomsReservation.Core.Models;
using MediatR;
using ConferenceRoomsReservation.Application.ConferenceRoom.Commands.CreateConferenceRoom;
using ConferenceRoomsReservation.DataAccess.Entities;

namespace ConferenceRoomsReservation.Application.ConferenceRooms.Commands.CreateConferenceRoom;

public sealed class CreateConferenceRoomCommandHandler : IRequestHandler<CreateConferenceRoomCommand, Result<Guid>>
{
    private readonly IConferenceRoomRepository _conferenceRoomRepository;
    private readonly IAddServiceRepository _addServiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateConferenceRoomCommandHandler(
        IConferenceRoomRepository conferenceRoomRepository, 
        IAddServiceRepository addServiceRepository, 
        IUnitOfWork unitOfWork)
    {
        _conferenceRoomRepository = conferenceRoomRepository;
        _addServiceRepository = addServiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateConferenceRoomCommand request, CancellationToken cancellationToken)
    {
        var conferenceRoom = Core.Models.ConferenceRoom.Create(
             request.name,
             request.capacity,
             request.basePricePerHour
         );

        if (conferenceRoom.IsFailure)
        {
            return Result.Failure<Guid>(conferenceRoom.Error.Message);
        }

        var services = await _addServiceRepository.GetByIdsAsync(request.addServiceIds);

        if (services.IsFailure)
        {
            return Result.Failure<Guid>(services.Error.Message);
        }

        foreach (var service in services.Value)
        {
            var conferenceRoomAddService = new ConferenceRoomAddServiceEntity
            {
                ConferenceRoomId = conferenceRoom.Value.Id,
                AddServiceId = service.Id
            };

            await _addServiceRepository.AddConferenceRoomAddService(conferenceRoomAddService);
        }

        await _conferenceRoomRepository.CreateAsync(
            conferenceRoom.Value.Id,
            conferenceRoom.Value.Name,
            conferenceRoom.Value.Capacity,
            conferenceRoom.Value.BasePricePerHour);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(conferenceRoom.Value.Id);
    }
}
