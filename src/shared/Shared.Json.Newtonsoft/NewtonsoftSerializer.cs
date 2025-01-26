using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Shared.Json.Newtonsoft;

public class NewtonsoftSerializer : IJsonSerializer
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public NewtonsoftSerializer(IOptions<JsonSerializerSettings> jsonSerializerSettings)
    {
        _jsonSerializerSettings = jsonSerializerSettings.Value;
    }

    public string Serialize(object value)
    {
        return JsonConvert.SerializeObject(value, _jsonSerializerSettings);
    }

    public T? Deserialize<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value, _jsonSerializerSettings);
    }

    public object? Deserialize(string value, Type type)
    {
        return JsonConvert.DeserializeObject(value, type, _jsonSerializerSettings);
    }
}
