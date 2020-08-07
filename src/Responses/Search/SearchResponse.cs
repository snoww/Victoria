using System.Collections.Generic;
using System.Text.Json.Serialization;
using Victoria.Converters;
using Victoria.Interfaces;

namespace Victoria.Responses.Search {
    /// <summary>
    /// 
    /// </summary>
    public struct SearchResponse {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("loadType")]
        public SearchStatus Status { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("playlistInfo")]
        public PlaylistInfo Playlist { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("exception")]
        public SearchException Exception { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("tracks"), JsonConverter(typeof(LavaTracksPropertyConverter))]
        public IReadOnlyCollection<ILavaTrack> Tracks { get; internal set; }
    }
}