namespace Victoria.Responses.Search {
    /// <summary>
    /// 
    /// </summary>
    public struct SearchResponse {
        /// <summary>
        /// 
        /// </summary>
        public PlaylistInfo Playlist { get; internal set; }
        
        /// <summary>
        /// 
        /// </summary>
        public SearchException Exception { get; internal set; }
    }
}