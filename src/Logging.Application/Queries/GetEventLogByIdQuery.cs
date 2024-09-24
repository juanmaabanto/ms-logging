using System.Text.RegularExpressions;
using MediatR;
using Sofisoft.Abstractions.Repositories;
using Sofisoft.Enterprise.Logging.Application.Responses;
using Sofisoft.Enterprise.Logging.Core.Entities;

namespace Sofisoft.Enterprise.Logging.Application.Queries
{
    public record GetEventLogByIdQuery(string EventLogId)
        : IRequest<EventLogResponse>
    {
        public class GetEventLogByIdQueryHandler
            : IRequestHandler<GetEventLogByIdQuery, EventLogResponse>
        {
            private readonly IBaseRepository<EventLog> _repository;

            public GetEventLogByIdQueryHandler(IBaseRepository<EventLog> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public async Task<EventLogResponse> Handle(GetEventLogByIdQuery request,
                CancellationToken cancellationToken)
            {
                var eventLog = await _repository.FindByIdAsync(request.EventLogId, cancellationToken);

                var response = new EventLogResponse(eventLog.Id,
                    eventLog.CreatedAt,
                    eventLog.EventLogTypeId,
                    eventLog.Source,
                    eventLog.Message,
                    eventLog.Trace,
                    eventLog.CreatedBy,
                    eventLog.UserAgent);

                return response;
            }
        }
    }
}