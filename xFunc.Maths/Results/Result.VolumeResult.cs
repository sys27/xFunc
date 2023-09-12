// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="VolumeValue"/> to <see cref="VolumeResult"/>.
    /// </summary>
    /// <param name="volume">The volume value.</param>
    /// <returns>The volume result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(VolumeValue volume)
        => new VolumeResult(volume);

    /// <summary>
    /// Gets the volume value.
    /// </summary>
    /// <param name="volumeValue">The volume value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="VolumeValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetVolume([NotNullWhen(true)] out VolumeValue? volumeValue)
    {
        if (this is VolumeResult volumeResult)
        {
            volumeValue = volumeResult.Volume;
            return true;
        }

        volumeValue = null;
        return false;
    }

    /// <summary>
    /// Gets the volume value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="VolumeResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual VolumeValue Volume
        => ((VolumeResult)this).Volume;

    /// <summary>
    /// Represents the volume result.
    /// </summary>
    public sealed class VolumeResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.VolumeResult"/> class.
        /// </summary>
        /// <param name="volume">The volume value.</param>
        public VolumeResult(VolumeValue volume)
            => Volume = volume;

        /// <summary>
        /// Converts <see cref="Result.VolumeResult"/> to <see cref="VolumeValue"/>.
        /// </summary>
        /// <param name="volumeResult">The volume result.</param>
        /// <returns>The volume value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator VolumeValue(VolumeResult volumeResult)
            => volumeResult.Volume;

        /// <inheritdoc />
        public override string ToString()
            => Volume.ToString();

        /// <summary>
        /// Gets the volume value.
        /// </summary>
        public override VolumeValue Volume { get; }
    }
}