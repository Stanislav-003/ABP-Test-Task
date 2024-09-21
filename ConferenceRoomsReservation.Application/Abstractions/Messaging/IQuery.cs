using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
