using System.Text.Json.Serialization;

namespace Victoria.Responses.Search {
    /// <summary>
    /// 
    /// </summary>
    public struct SearchException {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("severity")]
        public string Severity { get; private set; }
    }
}