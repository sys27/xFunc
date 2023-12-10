// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace xFunc.Maths;

/// <summary>
/// The exception that is thrown in the process of tokenization.
/// </summary>
/// <seealso cref="Exception" />
[ExcludeFromCodeCoverage]
[Serializable]
public class TokenizeException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TokenizeException"/> class.
    /// </summary>
    public TokenizeException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenizeException"/> class.
    /// </summary>
    /// <param name="symbol">The unsupported symbol.</param>
    public TokenizeException(char symbol)
        : this(string.Format(CultureInfo.InvariantCulture, Resource.NotSupportedSymbol, symbol))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenizeException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">A <see cref="string"/> that describes the error.</param>
    public TokenizeException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenizeException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">A <see cref="string"/> that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public TokenizeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}