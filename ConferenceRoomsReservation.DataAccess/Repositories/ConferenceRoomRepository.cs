using ConferenceRoomsReservation.Core.Models;
using ConferenceRoomsReservation.Core.Shared;
using ConferenceRoomsReservation.DataAccess;
using ConferenceRoomsReservation.DataAccess.Abstractions;
using ConferenceRoomsReservation.DataAccess.Entities;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsReservation.DataAccess.Repositories;

public class ConferenceRoomRepository : IConferenceRoomRepository
{
    private readonly DataBaseContext _dataBaseContext;

    public ConferenceRoomRepository(DataBaseContext dataBaseContext)
    {
        _dataBaseContext = dataBaseContext;
    }

    public async Task<Result<Guid, Error>> CreateAsync(
        Guid id,
        string name,
        int capacity,
        decimal basePricePerHour)
    {
        var conferenceRoom = new ConferenceRoomEntity
        {
            Id = id,
            Name = name,
            Capacity = capacity,
            BasePricePerHour = basePricePerHour
        };

        _dataBaseContext.ConferenceRooms.Add(conferenceRoom);

        await _dataBaseContext.SaveChangesAsync();

        return Result.Success<Guid, Error>(conferenceRoom.Id);
    }

    public async Task<Result<Error>> UpdateAsync(
        Guid conferenceRoomId,
        string? name = null,
        int? capacity = null,
        decimal? basePricePerHour = null,
        List<Guid>? addServiceIds = null)
    {
        var conferenceRoom = await _dataBaseContext.ConferenceRooms
            .Include(cr => cr.ConferenceRoomAddServices)
            .FirstOrDefaultAsync(cr => cr.Id == conferenceRoomId);

        if (conferenceRoom == null)
            return Errors.General.NotFound();

        if (!string.IsNullOrWhiteSpace(name))
            conferenceRoom.Name = name;

        if (capacity.HasValue)
            conferenceRoom.Capacity = capacity.Value;

        if (basePricePerHour.HasValue)
            conferenceRoom.BasePricePerHour = basePricePerHour.Value;

        if (addServiceIds != null && addServiceIds.Any())
        {
            var addServices = await _dataBaseContext.Services
                .Where(s => addServiceIds.Contains(s.Id))
                .ToListAsync();

            if (!addServices.Any())
                return Errors.General.NotFound();

            conferenceRoom.ConferenceRoomAddServices.Clear();
            conferenceRoom.ConferenceRoomAddServices = addServices.Select(s => new ConferenceRoomAddServiceEntity
            {
                AddServiceId = s.Id,
                ConferenceRoomId = conferenceRoom.Id
            }).ToList();
        }

        await _dataBaseContext.SaveChangesAsync();

        return Result.Success(Errors.General.UpdateSuccess());
    }

    public async Task<Result<Error>> DeleteAsync(Guid conferenceRoomId)
    {
        var conferenceRoom = await _dataBaseContext.ConferenceRooms
            .Include(cr => cr.ConferenceRoomAddServices)
            .FirstOrDefaultAsync(cr => cr.Id == conferenceRoomId);

        if (conferenceRoom == null)
            return Errors.General.NotFound(conferenceRoomId);

        _dataBaseContext.ConferenceRooms.Remove(conferenceRoom);
        await _dataBaseContext.SaveChangesAsync();

        return Result.Success(Errors.General.DeleteSuccess());
    }

    public async Task<Result<List<ConferenceRoomEntity>, Error>> FindAvailableRoomsAsync(
        BookingTime date,
        double durationHours,
        int requiredCapacity)
    {
        var availableRooms = await _dataBaseContext.ConferenceRooms
            .Where(cr => cr.Capacity >= requiredCapacity)
            .Include(cr => cr.ConferenceRoomAddServices)
            .ToListAsync();

        var bookedRoomsIds = await _dataBaseContext.Bookings
            .Where(b => b.BookingTime.Year == date.Year &&
                        b.BookingTime.Month == date.Month &&
                        b.BookingTime.Day == date.Day &&
                        ((b.BookingTime.Hours < date.Hours + durationHours && b.BookingTime.Hours + b.Duration.Hours > date.Hours) ||
                        (b.BookingTime.Hours + b.Duration.Hours > date.Hours && b.BookingTime.Hours < date.Hours + durationHours)))
            .Select(b => b.ConferenceRoomId)
            .ToListAsync();

        var result = availableRooms.Where(cr => !bookedRoomsIds.Contains(cr.Id)).ToList();

        return Result.Success<List<ConferenceRoomEntity>, Error>(result);
    }
}
