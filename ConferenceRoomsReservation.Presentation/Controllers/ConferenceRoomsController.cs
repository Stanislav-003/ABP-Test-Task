using ConferenceRoomsReservation.Application.ConferenceRoom.Commands.CreateConferenceRoom;
using ConferenceRoomsReservation.Application.ConferenceRoom.Commands.DeleteConferenceRoom;
using ConferenceRoomsReservation.Application.ConferenceRoom.Commands.UpdateConferenceRoom;
using ConferenceRoomsReservation.Application.ConferenceRoom.Queries.GetRooms;
using ConferenceRoomsReservation.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomsReservation.Presentation.Controllers;

public class ConferenceRoomsController : ApiController
{
    private readonly IMediator _mediator;

    public ConferenceRoomsController(IMediator mediator) : base(mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add-conference-room")]
    public async Task<IActionResult> AddConferenceRoom([FromBody] CreateConferenceRoomRequest request)
    {
        var command = new CreateConferenceRoomCommand(
            request.name,
            request.capacity,
            request.basePricePerHour,
            request.addServiceIds);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("update-conference-room")]
    public async Task<IActionResult> UpdateConferenceRoom([FromBody] UpdateConferenceRoomRequest request)
    {
        var command = new UpdateConferenceRoomCommand(
            request.conferenceRoomId,
            request.name,
            request.capacity,
            request.basePricePerHour,
            request.addServiceIds);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete-conference-room/{conferenceRoomId:guid}")]
    public async Task<IActionResult> DeleteConferenceRoom(Guid conferenceRoomId)
    {
        var command = new DeleteConferenceRoomCommand(conferenceRoomId);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("available-rooms")]
    public async Task<IActionResult> GetAvailableRooms(
        [FromQuery] int year,
        [FromQuery] int month,
        [FromQuery] int day,
        [FromQuery] int hours,
        [FromQuery] double durationHours,
        [FromQuery] int minutes,
        [FromQuery] int requiredCapacity)
    {
        var query = new GetRoomsQuery(
            year,
            month,
            day,
            hours,
            durationHours,
            minutes,
            requiredCapacity);

        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value); 
    }
}
