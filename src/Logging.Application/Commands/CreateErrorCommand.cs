using MediatR;
using Microsoft.Extensions.Logging;
using Sofisoft.Abstractions.Repositories;
using Sofisoft.Enterprise.Logging.Core.Entities;

namespace Sofisoft.Enterprise.Logging.Application.Commands
{
    public record CreateErrorCommand(string Source,
        string Message,
        string Trace,
        string Username,
        string UserAgent) : IRequest<string>
    {
        public class CreateErrorCommandHandler : IRequestHandler<CreateErrorCommand, string?>
        {
            private readonly IBaseRepository<EventLog> _repository;
            private readonly ILogger<CreateErrorCommandHandler> _logger;

            public CreateErrorCommandHandler(IBaseRepository<EventLog> repository, ILogger<CreateErrorCommandHandler> logger)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<string?> Handle(CreateErrorCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var eventLog = new EventLog(EventLogType.Error.Id, request.Source, request.Message,
                        request.Trace, request.UserAgent);

                    await _repository.InsertOneAsync(eventLog, cancellationToken);

                    return eventLog.Id;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error: {Message}", ex.Message);
                    return null;
                }
            }
        }
    }
}