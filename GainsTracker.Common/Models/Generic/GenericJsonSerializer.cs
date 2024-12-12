using System.Text.Json;

namespace GainsTracker.Common.Models.Generic;

public static class GenericJsonSerializer
{
    public static JsonDocument SerializeObjectToJson(object objectToSerialize)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.SerializeToDocument(objectToSerialize, objectToSerialize.GetType()!, options);
    }
}
