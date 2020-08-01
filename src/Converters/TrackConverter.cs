using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Victoria.Interfaces;

namespace Victoria.Converters {
    internal sealed class TrackConverter : JsonConverter<ILavaTrack> {
        public override ILavaTrack Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            string hash = null,
                   id = null,
                   title = null,
                   author = null,
                   url = default;

            long duration = default;
            bool isSeekable = false,
                 isLiveStream = false;

            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndObject) {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName
                    && reader.ValueTextEquals("track")
                    && reader.Read()) {
                    hash = reader.GetString();
                }

                if (reader.TokenType != JsonTokenType.StartObject) {
                    continue;
                }

                while (reader.Read()) {
                    if (reader.TokenType == JsonTokenType.EndObject) {
                        break;
                    }

                    if (reader.TokenType != JsonTokenType.PropertyName) {
                        continue;
                    }

                    if (reader.ValueTextEquals("identifier") && reader.Read()) {
                        id = reader.GetString();
                    }
                    else if (reader.ValueTextEquals("isSeekable") && reader.Read()) {
                        isSeekable = reader.GetBoolean();
                    }
                    else if (reader.ValueTextEquals("author") && reader.Read()) {
                        author = reader.GetString();
                    }
                    else if (reader.ValueTextEquals("length") && reader.Read()) {
                        duration = reader.GetInt64();
                    }
                    else if (reader.ValueTextEquals("isStream") && reader.Read()) {
                        isLiveStream = reader.GetBoolean();
                    }
                    else if (reader.ValueTextEquals("title") && reader.Read()) {
                        title = reader.GetString();
                    }
                    else if (reader.ValueTextEquals("uri") && reader.Read()) {
                        url = reader.GetString();
                    }
                }
            }

            return new AbstractLavaTrack(
                hash,
                id,
                title,
                author,
                url,
                default,
                duration < TimeSpan.MaxValue.Ticks
                    ? TimeSpan.FromMilliseconds(duration)
                    : TimeSpan.MaxValue,
                isSeekable,
                isLiveStream
            );
        }

        public override void Write(Utf8JsonWriter writer, ILavaTrack value, JsonSerializerOptions options) {
            throw new NotImplementedException();
        }
    }
}