using System.Text.Json.Serialization;

namespace blogAPI.Data.Entities.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Administrator,
        Subscriber
    }
}
