using System.Text.RegularExpressions;
using MediatR;
using Sofisoft.Abstractions.Repositories;
using Sofisoft.Enterprise.Logging.Application.Responses;
using Sofisoft.Enterprise.Logging.Core.Entities;

namespace Sofisoft.Enterprise.Logging.Application.Queries
{
    public record GetEventLogsListQuery(string? Sort, int PageSize, int Start)
        : IRequest<(long total, IEnumerable<EventLogResponse> data)>
    {
        public class GetEventLogsListQueryHandler
            : IRequestHandler<GetEventLogsListQuery, (long total, IEnumerable<EventLogResponse> data)>
        {
            private readonly IBaseRepository<EventLog> _repository;

            public GetEventLogsListQueryHandler(IBaseRepository<EventLog> repository)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            }

            public async Task<(long total, IEnumerable<EventLogResponse> data)> Handle(GetEventLogsListQuery request,
                CancellationToken cancellationToken)
            {
                var regex = new Regex($".*{string.Empty}.*", RegexOptions.IgnoreCase);
                
                var taskTotal = _repository.CountAsync(f => regex.IsMatch(f.Message)
                    , cancellationToken: cancellationToken);
                var taskList = _repository.PaginatedAsync(f => regex.IsMatch(f.Message),
                    p => new EventLogResponse(p.Id,
                        p.CreatedAt,
                        p.EventLogTypeId,
                        p.Source,
                        p.Message,
                        p.Trace,
                        p.CreatedBy,
                        p.UserAgent),
                    request.Sort ?? string.Empty, request.PageSize, request.Start, cancellationToken: cancellationToken);

                return (await taskTotal, await taskList);
            }
        }
    }
}