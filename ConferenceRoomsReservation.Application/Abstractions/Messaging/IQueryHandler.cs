using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
