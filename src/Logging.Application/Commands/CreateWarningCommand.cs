using MediatR;
using Microsoft.Extensions.Logging;
using Sofisoft.Abstractions.Repositories;
using Sofisoft.Enterprise.Logging.Core.Entities;

namespace Sofisoft.Enterprise.Logging.Application.Commands
{
    public record CreateWarningCommand(string Source,
        string Message,
        string Username,
        string UserAgent) : IRequest<string>
    {
        public class CreateWarningCommandHandler : IRequestHandler<CreateWarningCommand, string?>
        {
            private readonly IBaseRepository<EventLog> _repository;
            private readonly ILogger<CreateWarningCommandHandler> _logger;

            public CreateWarningCommandHandler(IBaseRepository<EventLog> repository, ILogger<CreateWarningCommandHandler> logger)
            {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<string?> Handle(CreateWarningCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var eventLog = new EventLog(EventLogType.Warning.Id, request.Source, request.Message,
                        null, request.UserAgent);

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