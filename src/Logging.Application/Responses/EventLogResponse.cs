namespace Sofisoft.Enterprise.Logging.Application.Responses
{
    public record EventLogResponse(string? Id,
        DateTime CreatedAt,
        int EventLogTypeId,
        string Source,
        string Message,
        string? Trace,
        string? Username,
        string? UserAgent);
}