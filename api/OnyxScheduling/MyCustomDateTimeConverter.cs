using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class MyCustomDateTimeConverter : JsonConverter<DateTime>
{
    private const string Format = "MM-dd-yyyy hh:mm:ss tt"; // Your desired date format

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (DateTime.TryParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
            {
                return dateTime;
            }
        }

        return reader.GetDateTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}