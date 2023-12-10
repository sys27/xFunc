// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths;

/// <summary>
/// Throws when a result is invalid.
/// </summary>
[Serializable]
public class InvalidResultException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResultException"/> class.
    /// </summary>
    public InvalidResultException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResultException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidResultException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResultException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The inner exception.</param>
    public InvalidResultException(string message, Exception inner)
        : base(message, inner)
    {
    }
}