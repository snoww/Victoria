using Victoria.Wrappers;

namespace Victoria {
    /// <summary>
    /// 
    /// </summary>
    public class NodeConfiguration {
        /// <summary>
        /// 
        /// </summary>
        public DiscordClientWrapper DiscordClient { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Hostname { get; set; } = "127.0.0.1";

        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; } = 2333;

        /// <summary>
        /// 
        /// </summary>
        public bool IsSecure { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public string Authorization { get; set; } = "youshallnotpass";

        internal string SocketEndpoint
            => (IsSecure ? "wss" : "ws") + Endpoint;

        internal string HttpEndpoint
            => (IsSecure ? "https" : "http") + Endpoint;

        internal string Endpoint
            => $"://{Hostname}:{Port}/";
    }
}