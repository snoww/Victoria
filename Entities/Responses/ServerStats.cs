using System;
using Newtonsoft.Json;

namespace Victoria.Entities {
	public sealed class ServerStats {
		[JsonIgnore]
		public TimeSpan Uptime => TimeSpan.FromMilliseconds(RawUptime);

		[JsonProperty("cpu")]
		public Cpu Cpu { get; private set; }

		[JsonProperty("frameStats")]
		public Frames Frames { get; private set; }

		[JsonProperty("memory")]
		public Memory Memory { get; private set; }

		[JsonProperty("players")]
		public int PlayerCount { get; private set; }

		[JsonProperty("playingPlayers")]
		public int PlayingPlayers { get; private set; }

		[JsonProperty("uptime")]
		private long RawUptime { get; set; }

		internal ServerStats() {
		}
	}
}