using System;
using Newtonsoft.Json;

namespace Victoria.Entities.Payloads {
	internal sealed class SessionPayload : BasePayload {
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("timeout")]
		public int Timeout { get; set; }

		public SessionPayload(string key, TimeSpan time) : base("configureResuming") {
			Key = key;
			Timeout = (int) time.TotalMilliseconds;
		}
	}
}