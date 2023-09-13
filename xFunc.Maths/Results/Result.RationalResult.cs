// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="RationalValue"/> to <see cref="RationalResult"/>.
    /// </summary>
    /// <param name="rational">The rational value.</param>
    /// <returns>The rational result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(RationalValue rational)
        => new RationalResult(rational);

    /// <summary>
    /// Gets the rational value.
    /// </summary>
    /// <param name="rationalValue">The rational value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="RationalValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetRational([NotNullWhen(true)] out RationalValue? rationalValue)
    {
        if (this is RationalResult rationalResult)
        {
            rationalValue = rationalResult.Rational;
            return true;
        }

        rationalValue = null;
        return false;
    }

    /// <summary>
    /// Gets the rational value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="RationalResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual RationalValue Rational
        => ((RationalResult)this).Rational;

    /// <summary>
    /// Represents the rational result.
    /// </summary>
    public sealed class RationalResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.RationalResult"/> class.
        /// </summary>
        /// <param name="rational">The rational value.</param>
        public RationalResult(RationalValue rational)
            => Rational = rational;

        /// <summary>
        /// Converts <see cref="Result.RationalResult"/> to <see cref="RationalValue"/>.
        /// </summary>
        /// <param name="rationalResult">The rational result.</param>
        /// <returns>The rational value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator RationalValue(RationalResult rationalResult)
            => rationalResult.Rational;

        /// <inheritdoc />
        public override string ToString()
            => Rational.ToString();

        /// <summary>
        /// Gets the rational value.
        /// </summary>
        public override RationalValue Rational { get; }
    }
}