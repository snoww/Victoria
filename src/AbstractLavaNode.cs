using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Victoria.Enums;
using Victoria.Interfaces;
using Victoria.WebSocket;
using Victoria.Wrappers;

namespace Victoria {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TLavaPlayer"></typeparam>
    /// <typeparam name="TLavaTrack"></typeparam>
    public abstract class AbstractLavaNode<TLavaPlayer, TLavaTrack> : ILavaNode<TLavaPlayer, TLavaTrack>
        where TLavaPlayer : ILavaPlayer<TLavaTrack>
        where TLavaTrack : ILavaTrack {
        /// <inheritdoc />
        public bool IsConnected
            => Volatile.Read(ref _isConnected);

        /// <inheritdoc />
        public IReadOnlyCollection<TLavaPlayer> Players
            => _players.Values as IReadOnlyCollection<TLavaPlayer>;

        private bool _isConnected;
        private readonly DiscordClientWrapper _discordClientWrapper;
        private readonly WebSocketClient _webSocketClient;
        private readonly ConcurrentDictionary<ulong, TLavaPlayer> _players;

        /// <summary>
        /// 
        /// </summary>
        protected AbstractLavaNode(DiscordClientWrapper discordClientWrapper) {
            _discordClientWrapper = discordClientWrapper;
            _webSocketClient = new WebSocketClient("", 0, "ws");
            _players = new ConcurrentDictionary<ulong, TLavaPlayer>();
        }

        /// <inheritdoc />
        public async ValueTask ConnectAsync() {
            if (Volatile.Read(ref _isConnected)) {
                throw new InvalidOperationException(
                    $"A connection is already established. Please call {nameof(DisconnectAsync)} before calling {nameof(ConnectAsync)}.");
            }

            await _webSocketClient.ConnectAsync();
        }

        /// <inheritdoc />
        public async ValueTask DisconnectAsync() {
            if (!Volatile.Read(ref _isConnected)) {
                throw new InvalidOperationException("Cannot disconnect when no connection is established.");
            }

            await _webSocketClient.DisconnectAsync();
        }

        /// <inheritdoc />
        public async ValueTask JoinAsync() {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask LeaveAsync() {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask MoveAsync() {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public async ValueTask SearchAsync(SearchType searchType, string query) {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool HasPlayer(ulong guildId) {
            return _players.ContainsKey(guildId);
        }

        /// <inheritdoc />
        public bool TryGetPlayer(ulong guildId, out TLavaPlayer lavaPlayer) {
            return _players.TryGetValue(guildId, out lavaPlayer);
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync() {
            throw new System.NotImplementedException();
        }
    }
}