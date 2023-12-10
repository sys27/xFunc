// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace xFunc.Maths.Analyzers.TypeAnalyzers;

/// <summary>
/// Represents an exception when the type of the actual argument does not match the expected parameter type.
/// </summary>
[Serializable]
public class DifferentParameterTypeMismatchException : ParameterTypeMismatchException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException"/> class.
    /// </summary>
    public DifferentParameterTypeMismatchException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException" /> class.
    /// </summary>
    /// <param name="expected">The expected parameter type.</param>
    /// <param name="actual">The actual parameter type.</param>
    /// <param name="index">The index of parameter.</param>
    public DifferentParameterTypeMismatchException(ResultTypes expected, ResultTypes actual, int index)
        : base(expected, actual, string.Format(CultureInfo.InvariantCulture, Resource.DifferentParameterTypeMismatchExceptionError, expected.ToString(), actual.ToString(), index + 1))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DifferentParameterTypeMismatchException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DifferentParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public DifferentParameterTypeMismatchException(string message, Exception inner)
        : base(message, inner)
    {
    }
}