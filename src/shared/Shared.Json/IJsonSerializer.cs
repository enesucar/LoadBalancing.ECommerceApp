namespace Shared.Json;

public interface IJsonSerializer
{
    string Serialize(object value);

    T? Deserialize<T>(string jsonString);

    object? Deserialize(string jsonString, Type type);
}
