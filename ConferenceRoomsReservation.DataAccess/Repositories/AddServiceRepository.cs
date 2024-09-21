using ConferenceRoomsReservation.Core.Shared;
using ConferenceRoomsReservation.DataAccess.Abstractions;
using ConferenceRoomsReservation.DataAccess.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsReservation.DataAccess.Repositories;

public class AddServiceRepository : IAddServiceRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public AddServiceRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<Result<List<AddServiceEntity>, Error>> GetByIdsAsync(List<Guid> serviceIds)
    {
        if (serviceIds == null || !serviceIds.Any())
        {
            return Result.Success<List<AddServiceEntity>, Error>(new List<AddServiceEntity>());
        }

        var addServices = await _dataBaseContext.Services
            .Where(s => serviceIds.Contains(s.Id))
            .ToListAsync();
        
        if (!addServices.Any())
        {
            return Errors.General.NotFound("None of the services were found with the provided IDs.");
        }

        return Result.Success<List<AddServiceEntity>, Error>(addServices);
    }

    public async Task<Result> AddConferenceRoomAddService(ConferenceRoomAddServiceEntity conferenceRoomAddService)
    {
        await _dataBaseContext.ConferenceRoomAddServices.AddAsync(conferenceRoomAddService);
        return Result.Success();
    }
}
