using ConferenceRoomsReservation.DataAccess.Abstractions;
using ConferenceRoomsReservation.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceRoomsReservation.DataAccess;

public static class DI
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IConferenceRoomRepository, ConferenceRoomRepository>();
        services.AddScoped<IAddServiceRepository, AddServiceRepository>();
    }
}
