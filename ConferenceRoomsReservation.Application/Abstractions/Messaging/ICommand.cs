using CSharpFunctionalExtensions;
using MediatR;

namespace ConferenceRoomsReservation.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
