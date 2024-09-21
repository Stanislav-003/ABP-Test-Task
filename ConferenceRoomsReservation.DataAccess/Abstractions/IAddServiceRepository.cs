using ConferenceRoomsReservation.Core.Shared;
using ConferenceRoomsReservation.DataAccess.Entities;
using CSharpFunctionalExtensions;

namespace ConferenceRoomsReservation.DataAccess.Abstractions;

public interface IAddServiceRepository
{
    Task<Result<List<AddServiceEntity>, Error>> GetByIdsAsync(List<Guid> serviceIds);
    Task<Result> AddConferenceRoomAddService(ConferenceRoomAddServiceEntity conferenceRoomAddService);
}
