// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace xFunc.Maths.Expressions.Collections;

/// <summary>
/// Trying to change a read-only variable.
/// </summary>
[Serializable]
public class ParameterIsReadOnlyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
    /// </summary>
    public ParameterIsReadOnlyException()
        : this(Resource.ReadOnlyError)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ParameterIsReadOnlyException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner.</param>
    public ParameterIsReadOnlyException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="parameterName">The parameter name.</param>
    public ParameterIsReadOnlyException(string message, string parameterName)
        : this(message, parameterName, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="parameterName">The parameter name.</param>
    /// <param name="inner">The inner.</param>
    public ParameterIsReadOnlyException(string message, string parameterName, Exception? inner)
        : base(message, inner)
    {
        ParameterName = parameterName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterIsReadOnlyException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected ParameterIsReadOnlyException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Gets the parameter name.
    /// </summary>
    public string? ParameterName { get; }
}