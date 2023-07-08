// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace xFunc.Maths.Expressions.Matrices;

/// <summary>
/// Thrown in matrix building.
/// </summary>
[Serializable]
public class InvalidMatrixException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidMatrixException"/> class.
    /// </summary>
    public InvalidMatrixException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidMatrixException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidMatrixException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidMatrixException"/> class.
    /// </summary>
    /// <param name="message">A <see cref="string"/> that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public InvalidMatrixException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidMatrixException"/> class.
    /// </summary>
    /// <param name="info">The object that holds the serialized object data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
    protected InvalidMatrixException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}