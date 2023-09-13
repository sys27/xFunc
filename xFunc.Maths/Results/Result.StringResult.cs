// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Results;

public abstract partial class Result
{
    /// <summary>
    /// Converts <see cref="string"/> to <see cref="StringResult"/>.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>The string result.</returns>
    [ExcludeFromCodeCoverage]
    public static implicit operator Result(string str)
        => new StringResult(str);

    /// <summary>
    /// Gets the string.
    /// </summary>
    /// <param name="stringValue">The string value.</param>
    /// <returns><c>true</c> if the current instance contains <see cref="string"/>, otherwise <c>false</c>.</returns>
    public bool TryGetString([NotNullWhen(true)] out string? stringValue)
    {
        if (this is StringResult stringResult)
        {
            stringValue = stringResult.String;
            return true;
        }

        stringValue = null;
        return false;
    }

    /// <summary>
    /// Gets the string value.
    /// </summary>
    /// <exception cref="InvalidCastException">Thrown when the current instance is not <see cref="StringResult"/>.</exception>
    [ExcludeFromCodeCoverage]
    public virtual string String
        => ((StringResult)this).String;

    /// <summary>
    /// Represents the string result.
    /// </summary>
    public sealed class StringResult : Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result.StringResult"/> class.
        /// </summary>
        /// <param name="str">The string value.</param>
        public StringResult(string str)
            => String = str ?? throw new ArgumentNullException(nameof(str));

        /// <summary>
        /// Converts <see cref="Result.StringResult"/> to <see cref="string"/>.
        /// </summary>
        /// <param name="stringResult">The string result.</param>
        /// <returns>The string.</returns>
        [ExcludeFromCodeCoverage]
        public static implicit operator string(StringResult stringResult)
            => stringResult.String;

        /// <inheritdoc />
        public override string ToString()
            => String;

        /// <summary>
        /// Gets the string value.
        /// </summary>
        public override string String { get; }
    }
}