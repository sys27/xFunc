// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace xFunc.Maths;

/// <summary>
/// The exception that is thrown in the process of parsing expression tree.
/// </summary>
[Serializable]
public class ParseException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParseException"/> class.
    /// </summary>
    public ParseException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">A <see cref="string"/> that describes the error.</param>
    public ParseException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">A <see cref="string"/> that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public ParseException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseException"/> class with serialized data.
    /// </summary>
    /// <param name="info">The object that holds the serialized object data.</param>
    /// <param name="context">The contextual information about the source or destination.</param>
    protected ParseException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}