// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="PowerValue"/> to <see cref="PowerResult"/>.
    /// </summary>
    /// <param name="power">The power value.</param>
    /// <returns>The power result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(PowerValue power)
        => new PowerResult(power);

    /// <summary>
    /// Gets the power value.
    /// </summary>
    /// <param name="powerValue">The power value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="PowerValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetPower([NotNullWhen(true)] out PowerValue? powerValue)
    {
        if (this is PowerResult powerResult)
        {
            powerValue = powerResult.Power;
            return true;
        }

        powerValue = null;
        return false;
    }

    /// <summary>
    /// Gets the power value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="PowerResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual PowerValue Power
        => ((PowerResult)this).Power;

    /// <summary>
    /// Represents the power result.
    /// </summary>
    public sealed class PowerResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.PowerResult"/> class.
        /// </summary>
        /// <param name="power">The power value.</param>
        public PowerResult(PowerValue power)
            => Power = power;

        /// <summary>
        /// Converts <see cref="Result.PowerResult"/> to <see cref="PowerValue"/>.
        /// </summary>
        /// <param name="powerResult">The power result.</param>
        /// <returns>The power value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator PowerValue(PowerResult powerResult)
            => powerResult.Power;

        /// <inheritdoc />
        public override string ToString()
            => Power.ToString();

        /// <summary>
        /// Gets the power value.
        /// </summary>
        public override PowerValue Power { get; }
    }
}