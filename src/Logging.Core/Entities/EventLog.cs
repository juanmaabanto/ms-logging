using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sofisoft.Abstractions.Attributes;
using Sofisoft.MongoDb.Models;

namespace Sofisoft.Enterprise.Logging.Core.Entities
{
    [BsonCollection("eventLog")]
    public class EventLog: BaseEntity
    {

        [BsonElement("eventLogTypeId")]
        public int EventLogTypeId { get; private set; }

        [BsonElement("source")]
        public string Source { get; private set; }

        [BsonElement("message")]
        public string Message { get; private set; }

        [BsonElement("trace")]
        public string? Trace { get; private set; }

        [BsonElement("userAgent")]
        public string? UserAgent { get; private set; }

        public EventLog(int eventLogTypeId, string source, string message, string? trace, string? userAgent)
        {
            EventLogTypeId = eventLogTypeId;
            Source = source;
            Message = message;
            Trace = trace;
            UserAgent = userAgent;
        }
    }

}