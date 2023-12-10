// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Parameters;

/// <summary>
/// Represents the expression that is thrown when the user tries to change the read-only parameter.
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
    /// Gets the parameter name.
    /// </summary>
    public string? ParameterName { get; }
}