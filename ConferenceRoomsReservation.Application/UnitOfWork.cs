using ConferenceRoomsReservation.Application.Abstractions.Data;
using ConferenceRoomsReservation.DataAccess;

namespace ConferenceRoomsReservation.Application;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataBaseContext _dataBaseContext;

    public UnitOfWork(DataBaseContext dbContext)
    {
        _dataBaseContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dataBaseContext.SaveChangesAsync(cancellationToken);
    }
}
