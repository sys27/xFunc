// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace xFunc.Maths.Analyzers.TypeAnalyzers;

/// <summary>
/// Represents an exception when the type of the actual argument does not match the expected parameter type.
/// </summary>
[Serializable]
public class ParameterTypeMismatchException : Exception
{
    private readonly ResultTypes expected;
    private readonly ResultTypes actual;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterTypeMismatchException"/> class.
    /// </summary>
    public ParameterTypeMismatchException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterTypeMismatchException" /> class.
    /// </summary>
    /// <param name="expected">The expected parameter type.</param>
    /// <param name="actual">The actual parameter type.</param>
    public ParameterTypeMismatchException(ResultTypes expected, ResultTypes actual)
        : this(expected, actual, string.Format(CultureInfo.InvariantCulture, Resource.ParameterTypeMismatchExceptionError, expected.ToString(), actual.ToString()))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterTypeMismatchException" /> class.
    /// </summary>
    /// <param name="expected">The expected parameter type.</param>
    /// <param name="actual">The actual parameter type.</param>
    /// <param name="message">The error message.</param>
    public ParameterTypeMismatchException(ResultTypes expected, ResultTypes actual, string message)
        : base(message)
    {
        this.expected = expected;
        this.actual = actual;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ParameterTypeMismatchException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ParameterTypeMismatchException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Gets the expected parameter type.
    /// </summary>
    /// <value>
    /// The expected parameter type.
    /// </value>
    public ResultTypes Expected => expected;

    /// <summary>
    /// Gets the actual parameter type.
    /// </summary>
    /// <value>
    /// The actual parameter type.
    /// </value>
    public ResultTypes Actual => actual;
}