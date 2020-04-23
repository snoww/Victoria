using System;
using Newtonsoft.Json;
using Victoria.Queue;

namespace Victoria.Entities {
	public class LavaTrack : IQueueObject {
		[JsonIgnore]
		public TimeSpan Length
			=> TimeSpan.FromMilliseconds(TrackLength);

		[JsonIgnore]
		public string Provider
			=> Uri.GetProvider();

		[JsonProperty("author")]
		public string Author { get; internal set; }

		[JsonIgnore]
		internal string Hash { get; set; }

		[JsonProperty("isSeekable")]
		public bool IsSeekable { get; internal set; }

		[JsonProperty("isStream")]
		public bool IsStream { get; internal set; }

		[JsonIgnore]
		public TimeSpan Position {
			get => new TimeSpan(TrackPosition);
			internal set => TrackPosition = value.Ticks;
		}

		[JsonProperty("title")]
		public string Title { get; internal set; }

		[JsonProperty("length")]
		internal long TrackLength { get; set; }

		[JsonProperty("position")]
		internal long TrackPosition { get; set; }

		[JsonProperty("uri")]
		public Uri Uri { get; internal set; }

		[JsonProperty("identifier")]
		public string Id { get; internal set; }

        /// <summary>
        /// </summary>
        public void ResetPosition() {
			Position = TimeSpan.Zero;
		}
	}
}