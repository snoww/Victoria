using System.Text.Json.Serialization;

namespace Victoria.Responses.Search {
    /// <summary>
    /// 
    /// </summary>
    public struct PlaylistInfo {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("selectedTrack")]
        public int SelectedTrack { get; internal set; }
    }
}