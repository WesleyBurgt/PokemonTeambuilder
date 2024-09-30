using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class CamelCaseEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString();
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        string enumString = value.ToString();
        string camelCaseString = char.ToLower(enumString[0], CultureInfo.InvariantCulture) + enumString.Substring(1);
        writer.WriteStringValue(camelCaseString);
    }
}
