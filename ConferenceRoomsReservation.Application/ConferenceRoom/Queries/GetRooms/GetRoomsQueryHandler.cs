using ConferenceRoomsReservation.Application.Abstractions.Messaging;
using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.DataAccess.Abstractions;
using CSharpFunctionalExtensions;

namespace ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;

public sealed class GetRoomsQueryHandler : IQueryHandler<GetRoomsQuery, GetRoomsResponse>
{
    private readonly IConferenceRoomRepository _conferenceRoomRepository;

    public GetRoomsQueryHandler(IConferenceRoomRepository conferenceRoomRepository)
    {
        _conferenceRoomRepository = conferenceRoomRepository;
    }

    public async Task<Result<GetRoomsResponse>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var date = BookingTime.Create(request.year, request.month, request.day, request.hours, request.minutes);

        var availableRoomsResult = await _conferenceRoomRepository.FindAvailableRoomsAsync(
            date.Value,
            request.startTime,
            request.endTime,
            request.requiredCapacity);

        if (availableRoomsResult.IsFailure)
        {
            return Result.Failure<GetRoomsResponse>(availableRoomsResult.Error.Message);
        }

        var availableRooms = availableRoomsResult.Value.Select(room => new GetRoomResponse(
            room.Id,
            room.Name,
            room.Capacity,
            room.BasePricePerHour,
            room.ConferenceRoomAddServices.Select(service => new AddServicesResponse(
                service.AddServiceId,
                service.AddService.Name, 
                service.AddService.Price 
            )).ToList()
        )).ToList();

        var response = new GetRoomsResponse(availableRooms);

        return Result.Success(response);
    }
}
