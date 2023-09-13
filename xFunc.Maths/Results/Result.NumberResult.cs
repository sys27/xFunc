// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="NumberValue"/> to <see cref="NumberResult"/>.
    /// </summary>
    /// <param name="numberValue">The number value.</param>
    /// <returns>The number result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(NumberValue numberValue)
        => new NumberResult(numberValue);

    /// <summary>
    /// Gets the number value.
    /// </summary>
    /// <param name="number">The number value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="NumberValue"/>, otherwise <c>false</c>.</returns>
    public bool TryGetNumber([NotNullWhen(true)] out NumberValue? number)
    {
        if (this is NumberResult numberResult)
        {
            number = numberResult.Number;
            return true;
        }

        number = null;
        return false;
    }

    /// <summary>
    /// Gets the number value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="NumberResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual NumberValue Number
        => ((NumberResult)this).Number;

    /// <summary>
    /// Represents the number result.
    /// </summary>
    public sealed class NumberResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.NumberResult"/> class.
        /// </summary>
        /// <param name="number">The number value.</param>
        public NumberResult(double number)
            : this(new NumberValue(number))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result.NumberResult"/> class.
        /// </summary>
        /// <param name="number">The number value.</param>
        public NumberResult(NumberValue number)
            => Number = number;

        /// <summary>
        /// Converts <see cref="Result.NumberResult"/> to <see cref="NumberValue"/>.
        /// </summary>
        /// <param name="numberResult">The number result.</param>
        /// <returns>The number value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator NumberValue(NumberResult numberResult)
            => numberResult.Number;

        /// <inheritdoc />
        public override string ToString()
            => Number.ToString();

        /// <summary>
        /// Gets the number value.
        /// </summary>
        public override NumberValue Number { get; }
    }
}