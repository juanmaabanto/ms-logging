using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sofisoft.Abstractions.Attributes;

namespace Sofisoft.Enterprise.Logging.Core.Entities
{
    [BsonCollection("eventLogType")]
    public class EventLogType 
    {
        #region "Variables"

        public static readonly EventLogType Error = new EventLogType(1, nameof(Error));
        public static readonly EventLogType Information = new EventLogType(2, nameof(Information));
        public static readonly EventLogType Warning = new EventLogType(3, nameof(Warning));
        
        #endregion

        #region "Propiedades"

        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; private set; }
        
        [BsonElement("name")]
        public string Name { get; private set; }

        #endregion

        #region "Constructor"

        public EventLogType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region "Metodos"

        public static IEnumerable<EventLogType> List() =>
            new [] { Information, Warning, Error };

        #endregion
    }
}