// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Runtime.Serialization;

namespace xFunc.Maths.Analyzers.TypeAnalyzers;

/// <summary>
/// Type of binary parameter.
/// </summary>
public enum BinaryParameterType
{
    /// <summary>
    /// The left parameter.
    /// </summary>
    Left,

    /// <summary>
    /// The right parameter.
    /// </summary>
    Right,
}

/// <summary>
/// Represents an exception when the type of the actual argument does not match the expected parameter type.
/// </summary>
/// <seealso cref="ParameterTypeMismatchException" />
[Serializable]
public class BinaryParameterTypeMismatchException : ParameterTypeMismatchException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
    /// </summary>
    public BinaryParameterTypeMismatchException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="expected">The expected.</param>
    /// <param name="actual">The actual.</param>
    /// <param name="parameterType">Type of the parameter.</param>
    public BinaryParameterTypeMismatchException(
        ResultTypes expected,
        ResultTypes actual,
        BinaryParameterType parameterType)
        : base(expected, actual, string.Format(CultureInfo.InvariantCulture, parameterType == BinaryParameterType.Left ? Resource.LeftParameterTypeMismatchExceptionError : Resource.RightParameterTypeMismatchExceptionError, expected.ToString(), actual.ToString()))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="expected">The expected parameter type.</param>
    /// <param name="actual">The actual parameter type.</param>
    /// <param name="message">The error message.</param>
    public BinaryParameterTypeMismatchException(ResultTypes expected, ResultTypes actual, string message)
        : base(expected, actual, message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public BinaryParameterTypeMismatchException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public BinaryParameterTypeMismatchException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryParameterTypeMismatchException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected BinaryParameterTypeMismatchException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}