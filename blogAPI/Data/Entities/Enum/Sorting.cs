
using System.Text.Json.Serialization;

namespace blogAPI.Data.Entities.Enum
{
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        CreateAsc,
        CreateDesc,
        LikeAsc,
        LikeDesc,
    }
}
