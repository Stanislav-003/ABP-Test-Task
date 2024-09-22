using ConferenceRoomsReservation.Application.Booking.Commands.CreateBooking;
using ConferenceRoomsReservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomsReservation.Presentation.Controllers;

public class BookingController : ApiController
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("book-room")]
    public async Task<IActionResult> BookRoom([FromBody] CreateBookingRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid booking request.");
        }

        var command = new CreateBookingCommand(
            request.conferenceRoomId,
            request.year,
            request.month,
            request.day,
            request.hours,
            request.minutes,
            request.durationHours,
            request.addServiceIds);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error); 
        }

        return Ok(result.Value); 
    }
}
