using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.Core.Shared;
using ConferenceRoomsReservation.DataAccess.Entities;
using CSharpFunctionalExtensions;

namespace ConferenceRoomsReservation.DataAccess.Abstractions;

public interface IConferenceRoomRepository
{
    Task<Result<Guid, Error>> CreateAsync(Guid id, string name, int capacity, decimal basePricePerHour);
    Task<Result<Error>> DeleteAsync(Guid conferenceRoomId);
    Task<Result<List<ConferenceRoomEntity>, Error>> FindAvailableRoomsAsync(BookingTime date, TimeSpan startTime, TimeSpan endTime, int requiredCapacity);
    Task<Result<Error>> UpdateAsync(Guid conferenceRoomId, string? name = null, int? capacity = null, decimal? basePricePerHour = null, List<Guid>? addServiceIds = null);
}
