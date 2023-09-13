// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="LengthValue"/> to <see cref="LengthResult"/>.
    /// </summary>
    /// <param name="length">The length value.</param>
    /// <returns>The length result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(LengthValue length)
        => new LengthResult(length);

    /// <summary>
    /// Gets the length value.
    /// </summary>
    /// <param name="lengthValue">The length value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="LengthValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetLength([NotNullWhen(true)] out LengthValue? lengthValue)
    {
        if (this is LengthResult lengthResult)
        {
            lengthValue = lengthResult.Length;
            return true;
        }

        lengthValue = null;
        return false;
    }

    /// <summary>
    /// Gets the length value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="LengthResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual LengthValue Length
        => ((LengthResult)this).Length;

    /// <summary>
    /// Represents the length result.
    /// </summary>
    public sealed class LengthResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.LengthResult"/> class.
        /// </summary>
        /// <param name="length">The length value.</param>
        public LengthResult(LengthValue length)
            => Length = length;

        /// <summary>
        /// Converts <see cref="Result.LengthResult"/> to <see cref="LengthValue"/>.
        /// </summary>
        /// <param name="lengthResult">The length result.</param>
        /// <returns>The length value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator LengthValue(LengthResult lengthResult)
            => lengthResult.Length;

        /// <inheritdoc />
        public override string ToString()
            => Length.ToString();

        /// <summary>
        /// Gets the length value.
        /// </summary>
        public override LengthValue Length { get; }
    }
}