using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Victoria.Entities {
	[JsonConverter(typeof(StringEnumConverter))]
	internal enum EventType {
		[EnumMember(Value = "TrackStartEvent")]
		TrackStart,

		[EnumMember(Value = "TrackEndEvent")]
		TrackEnd,

		[EnumMember(Value = "TrackStuckEvent")]
		TrackStuck,

		[EnumMember(Value = "TrackExceptionEvent")]
		TrackException,

		[EnumMember(Value = "WebSocketClosedEvent")]
		WebSocketClosed
	}
}