// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="TimeValue"/> to <see cref="TimeResult"/>.
    /// </summary>
    /// <param name="time">The time value.</param>
    /// <returns>The time result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(TimeValue time)
        => new TimeResult(time);

    /// <summary>
    /// Gets the time value.
    /// </summary>
    /// <param name="timeValue">The time value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="TimeValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetTime([NotNullWhen(true)] out TimeValue? timeValue)
    {
        if (this is TimeResult timeResult)
        {
            timeValue = timeResult.Time;
            return true;
        }

        timeValue = null;
        return false;
    }

    /// <summary>
    /// Gets the time value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="TimeResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual TimeValue Time
        => ((TimeResult)this).Time;

    /// <summary>
    /// Represents the time result.
    /// </summary>
    public sealed class TimeResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.TimeResult"/> class.
        /// </summary>
        /// <param name="time">The time value.</param>
        public TimeResult(TimeValue time)
            => Time = time;

        /// <summary>
        /// Converts <see cref="Result.TimeResult"/> to <see cref="TimeValue"/>.
        /// </summary>
        /// <param name="timeResult">The time result.</param>
        /// <returns>The time value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator TimeValue(TimeResult timeResult)
            => timeResult.Time;

        /// <inheritdoc />
        public override string ToString()
            => Time.ToString();

        /// <summary>
        /// Gets the time value.
        /// </summary>
        public override TimeValue Time { get; }
    }
}