using System.Collections.Generic;
using Newtonsoft.Json;

namespace Victoria.Entities {
	public sealed class SearchResult {
		[JsonProperty("loadType")]
		public LoadType LoadType { get; private set; }

		[JsonProperty("playlistInfo")]
		public PlaylistInfo PlaylistInfo { get; private set; }

		[JsonIgnore]
		public IEnumerable<LavaTrack> Tracks { get; internal set; }

		internal SearchResult() {
		}
	}
}