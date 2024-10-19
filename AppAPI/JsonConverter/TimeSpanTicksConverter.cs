using Newtonsoft.Json;
using System;

public class TimeSpanTicksConverter : JsonConverter<TimeSpan>
{
    public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("ticks");
        writer.WriteValue(value.Ticks);
        writer.WriteEndObject();
    }

    public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        long ticks = 0;
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "ticks")
            {
                reader.Read();
                ticks = (long)reader.Value;
            }
            else if (reader.TokenType == JsonToken.EndObject)
            {
                break;
            }
        }
        return TimeSpan.FromTicks(ticks);
    }
}
