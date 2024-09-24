using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sofisoft.Abstractions.Responses;
using Sofisoft.Enterprise.Logging.Api.Infrastructure.Attributes;
using Sofisoft.Enterprise.Logging.Application.Commands;
using Sofisoft.Enterprise.Logging.Application.Queries;
using Sofisoft.Enterprise.Logging.Application.Responses;

namespace Sofisoft.Enterprise.Logging.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Gets

        /// <summary>
        /// Returns paginated list of event logs.
        /// </summary>
        /// <param name="sort">Ordering criterion.</param>
        /// <param name="pageSize">Number of records returned.</param>
        /// <param name="start">Index of the initial record.</param>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType()]
        public async Task<ActionResult<PaginatedResponse<EventLogResponse>>> GetList([FromQuery]string? sort, 
            [FromQuery]int pageSize = 50, [FromQuery]int start = 0, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetEventLogsListQuery(sort, pageSize, start), cancellationToken);

            return new PaginatedResponse<EventLogResponse>(start, pageSize, result.total, result.data);
        }

        /// <summary>
        /// Returns the event log by Id.
        /// </summary>
        /// <param name="eventLogId">Id of the event.</param>
        [HttpGet]
        [Route("{eventLogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType()]
        public async Task<ActionResult<EventLogResponse>> Get(string eventLogId, CancellationToken cancellationToken)
            => await _mediator.Send(new GetEventLogByIdQuery(eventLogId), cancellationToken);

        #endregion

        #region Posts

        /// <summary>
        /// Create a event of type error.
        /// </summary>
        /// <param name="command">Object to be created.</param>
        [BasicAuthorize]
        [Route("errors")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType()]
        public async Task<ActionResult<string>> PostErrorAsync([FromBody]CreateErrorCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result is not null)
            {
                return CreatedAtAction(nameof(Get), new { eventLogId = result}, result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create a event of type information.
        /// </summary>
        /// <param name="command">Object to be created.</param>
        [BasicAuthorize]
        [Route("informations")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType()]
        public async Task<ActionResult<string>> PostInformationAsync([FromBody]CreateInformationCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result is not null)
            {
                return CreatedAtAction(nameof(Get), new { eventLogId = result}, result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Create a event of type warning.
        /// </summary>
        /// <param name="command">Object to be created.</param>
        [BasicAuthorize]
        [Route("warnings")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType()]
        public async Task<ActionResult<string>> PostWarningAsync([FromBody]CreateWarningCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result is not null)
            {
                return CreatedAtAction(nameof(Get), new { eventLogId = result}, result);
            }

            return BadRequest();
        }

        #endregion
    }
}