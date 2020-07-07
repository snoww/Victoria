using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Victoria.Enums;

namespace Victoria.Interfaces {
    /// <inheritdoc />
    public interface ILavaPlayer : ILavaPlayer<ILavaTrack> { }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TLavaTrack"></typeparam>
    public interface ILavaPlayer<TLavaTrack> : IAsyncDisposable where TLavaTrack : ILavaTrack {
        /// <summary>
        /// 
        /// </summary>
        TLavaTrack CurrentTrack { get; }

        /// <summary>
        /// 
        /// </summary>
        int CurrentVolume { get; }

        /// <summary>
        /// 
        /// </summary>
        DateTimeOffset LastUpdate { get; }

        /// <summary>
        /// 
        /// </summary>
        PlayerState PlayerState { get; }

        /// <summary>
        /// 
        /// </summary>
        IReadOnlyCollection<EqualizerBand> Bands { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lavaTrack"></param>
        /// <exception cref="ArgumentNullException">Throws when <paramref name="lavaTrack"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws when start or end time are out of range.</exception>
        /// <exception cref="InvalidOperationException">Throws when star time is bigger than end time.</exception>
        ValueTask PlayAsync(TLavaTrack lavaTrack);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lavaTrack"></param>
        /// <param name="startTime"></param>
        /// <param name="stopTime"></param>
        /// <param name="noReplace"></param>
        /// <exception cref="ArgumentNullException">Throws when <paramref name="lavaTrack"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="startTime"/> is less than <paramref name="lavaTrack"/> start time.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="stopTime"/> is greater than <paramref name="lavaTrack"/> end time.</exception>
        /// <exception cref="InvalidOperationException">Throws when star time is bigger than end time.</exception>
        ValueTask PlayAsync(TLavaTrack lavaTrack, TimeSpan startTime, TimeSpan stopTime, bool noReplace = false);

        /// <summary>
        /// 
        /// </summary>
        ValueTask StopAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws when <see cref="PlayerState"/> is invalid.</exception>
        ValueTask PauseAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws when <see cref="PlayerState"/> is invalid.</exception>
        ValueTask ResumeAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skipAfter"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Throws when <see cref="PlayerState"/> is invalid.</exception>
        ValueTask<(TLavaTrack Skipped, TLavaTrack Current)> SkipAsync(TimeSpan skipAfter = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seekPosition"></param>
        /// <exception cref="InvalidOperationException">Throws when <see cref="PlayerState"/> is invalid.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="seekPosition"/> is greater than <see cref="CurrentTrack"/> length.</exception>
        ValueTask SeekAsync(TimeSpan seekPosition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume"></param>
        ValueTask SetVolumeAsync(int volume);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws when <see cref="PlayerState"/> is invalid.</exception>
        ValueTask EqualizeAsync();
    }
}