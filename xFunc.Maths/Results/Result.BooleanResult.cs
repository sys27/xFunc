// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="bool"/> to <see cref="BooleanResult"/>.
    /// </summary>
    /// <param name="boolean">The boolean value.</param>
    /// <returns>The boolean result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(bool boolean)
        => new BooleanResult(boolean);

    /// <summary>
    /// Gets the boolean value.
    /// </summary>
    /// <param name="boolValue">The boolean value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="bool"/>, otherwise <c>false</c>.</returns>
    public bool TryGetBoolean([NotNullWhen(true)] out bool? boolValue)
    {
        if (this is BooleanResult boolResult)
        {
            boolValue = boolResult.Bool;
            return true;
        }

        boolValue = null;
        return false;
    }

#pragma warning disable SA1623
    /// <summary>
    /// Gets the boolean value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="BooleanResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual bool Bool
        => ((BooleanResult)this).Bool;
#pragma warning restore SA1623

    /// <summary>
    /// Represents the boolean result.
    /// </summary>
    public sealed class BooleanResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.BooleanResult"/> class.
        /// </summary>
        /// <param name="boolean">The boolean value.</param>
        public BooleanResult(bool boolean)
            => Bool = boolean;

        /// <summary>
        /// Converts <see cref="Result.BooleanResult"/> to <see cref="bool"/>.
        /// </summary>
        /// <param name="booleanResult">The boolean result.</param>
        /// <returns>The boolean value.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator bool(BooleanResult booleanResult)
            => booleanResult.Bool;

        /// <inheritdoc />
        public override string ToString()
            => Bool.ToString();

#pragma warning disable SA1623
        /// <summary>
        /// Gets the boolean value.
        /// </summary>
        public override bool Bool { get; }
#pragma warning restore SA1623
    }
}