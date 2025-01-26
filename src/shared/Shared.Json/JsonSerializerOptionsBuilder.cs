namespace Shared.Json;

public class JsonSerializerOptionsBuilder
{
    protected JsonSerializerOptions JsonSerializerOptions { get; set; }

    public JsonSerializerOptionsBuilder()
    {
        JsonSerializerOptions = new JsonSerializerOptions();
    }

    internal JsonSerializerOptions Build()
    {
        return JsonSerializerOptions;
    }
}
