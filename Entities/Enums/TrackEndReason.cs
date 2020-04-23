using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Victoria.Entities {
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TrackEndReason {
		[EnumMember(Value = "FINISHED")]
		Finished,

		[EnumMember(Value = "LOAD_FAILED")]
		LoadFailed,

		[EnumMember(Value = "STOPPED")]
		Stopped,

		[EnumMember(Value = "REPLACED")]
		Replaced,

		[EnumMember(Value = "CLEANUP")]
		Cleanup
	}
}