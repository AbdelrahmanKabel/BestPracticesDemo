using BestPracticesDemo.Application.Features.Events.Commands.CreateEvent;
using BestPracticesDemo.Application.Features.Events.Commands.DeleteEvent;
using BestPracticesDemo.Application.Features.Events.Commands.UpdateEvent;
using BestPracticesDemo.Application.Features.Events.Queries.GetEventDetails;
using BestPracticesDemo.Application.Features.Events.Queries.GetEventsExport;
using BestPracticesDemo.Application.Features.Events.Queries.GetEventsList;
using BestPracticesDemo.API.Utility;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BestPracticesDemo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllEvents", Name = "GetAllEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<EventListVm>>> GetAllEvents()
        {
            var dtos = await _mediator.Send(new GetEventListQuery());
            return Ok(dtos);
        }

        [HttpGet("GetEventById/{id}", Name = "GetEventById")]
        public async Task<ActionResult<EventDetailsVm>> GetEventById(Guid id)
        {
            var getEventDetailQuery = new GetEventDetailsQuery() { Id = id };
            return Ok(await _mediator.Send(getEventDetailQuery));
        }

        [HttpPost("AddEvent", Name = "AddEvent")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand createEventCommand)
        {
            var id = await _mediator.Send(createEventCommand);
            return Ok(id);
        }

        [HttpPut("UpdateEvent", Name = "UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
        {
            await _mediator.Send(updateEventCommand);
            return NoContent();
        }

        [HttpDelete("DeleteEvent/{id}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteEventCommand() { EventId = id };
            await _mediator.Send(deleteEventCommand);
            return NoContent();
        }

        [HttpGet("export", Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEvents()
        {
            var fileDto = await _mediator.Send(new GetEventsExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFileName);
        }
    }
}
