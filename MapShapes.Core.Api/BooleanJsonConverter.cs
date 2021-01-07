namespace MapShapes.Core.Api
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    ///     Handles converting JSON string values into a C# boolean data type.
    /// </summary>
    public class BooleanJsonConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            switch (Type.GetTypeCode(typeToConvert))
            {
                case TypeCode.Boolean:
                    return true;
                default:
                    return false;
            }
        }

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var s = reader.GetString();
                return bool.TryParse(s, out var i) ? i : throw new Exception($"unable to parse {s} to boolean");
            }

            if (reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False)
            {
                return reader.GetBoolean();
            }

            using var document = JsonDocument.ParseValue(ref reader);
            throw new Exception($"unable to parse {document.RootElement} to number");
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            var
                str = value.ToString(); // I don't want to write int/decimal/double/...  for each case, so I just convert it to string . You might want to replace it with strong type version.
            if (bool.TryParse(str, out var i))
            {
                writer.WriteBooleanValue(i);
            }
            else
            {
                throw new Exception($"unable to parse {str} to boolean");
            }
        }
    }
}