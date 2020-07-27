using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Victoria.Enums;
using Victoria.Interfaces;
using Victoria.WebSocket;

namespace Victoria {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TLavaTrack"></typeparam>
    public abstract class AbstractLavaPlayer<TLavaTrack> : ILavaPlayer<TLavaTrack> where TLavaTrack : ILavaTrack {
        /// <inheritdoc />
        public TLavaTrack CurrentTrack { get; }

        /// <inheritdoc />
        public int CurrentVolume { get; }

        /// <inheritdoc />
        public DateTimeOffset LastUpdate { get; }

        /// <inheritdoc />
        public PlayerState PlayerState { get; }

        /// <inheritdoc />
        public IReadOnlyCollection<object> Bands
            => _bands.Values as IReadOnlyCollection<object>;

        /// <inheritdoc />
        public LavaQueue<TLavaTrack> Queue { get; }

        private readonly IDictionary<int, object> _bands;
        private readonly WebSocketClient _webSocketClient;

        /// <summary>
        /// 
        /// </summary>
        protected AbstractLavaPlayer(WebSocketClient webSocketClient) {
            _webSocketClient = webSocketClient;
            _bands = new Dictionary<int, object>(15);
            Queue = new LavaQueue<TLavaTrack>();
        }

        /// <inheritdoc />
        public async ValueTask PlayAsync(TLavaTrack lavaTrack) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask PlayAsync(TLavaTrack lavaTrack, TimeSpan startTime, TimeSpan stopTime,
                                         bool noReplace = false) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask StopAsync() {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask PauseAsync() {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask ResumeAsync() {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask<(TLavaTrack Skipped, TLavaTrack Current)> SkipAsync(TimeSpan skipAfter = default) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask SeekAsync(TimeSpan seekPosition) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask SetVolumeAsync(int volume) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask EqualizeAsync() {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync() {
            await StopAsync();
            
            Queue.Clear();
            GC.SuppressFinalize(this);
        }
    }
}