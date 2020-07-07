using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Victoria.Enums;

namespace Victoria.Interfaces {
    /// <inheritdoc />
    public interface ILavaNode : ILavaNode<ILavaPlayer> { }

    /// <inheritdoc />
    public interface ILavaNode<TLavaPlayer> : ILavaNode<TLavaPlayer, ILavaTrack>
        where TLavaPlayer : ILavaPlayer<ILavaTrack> { }

    /// <summary>
    /// 
    /// </summary>
    public interface ILavaNode<TLavaPlayer, TLavaTrack> : IAsyncDisposable
        where TLavaPlayer : ILavaPlayer<TLavaTrack>
        where TLavaTrack : ILavaTrack {
        /// <summary>
        /// 
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 
        /// </summary>
        ICollection<TLavaPlayer> Players { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ValueTask ConnectAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ValueTask DisconnectAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ValueTask JoinAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ValueTask LeaveAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ValueTask MoveAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ValueTask SearchAsync(SearchType searchType, string query);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool HasPlayer();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lavaPlayer"></param>
        /// <returns></returns>
        bool TryGetPlayer(out TLavaPlayer lavaPlayer);
    }
}