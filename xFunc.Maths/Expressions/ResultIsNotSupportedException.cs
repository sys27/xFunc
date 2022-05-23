// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Runtime.Serialization;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents an exception which is thrown if expression doesn't support result type of own argument.
/// </summary>
/// <seealso cref="Exception" />
[Serializable]
public class ResultIsNotSupportedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
    /// </summary>
    public ResultIsNotSupportedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="exp">The expression with wrong argument type.</param>
    /// <param name="first">The result of argument calculation.</param>
    public ResultIsNotSupportedException(IExpression? exp, object first)
        : this(string.Format(
            CultureInfo.InvariantCulture,
            Resource.ResultIsNotSupported,
            exp?.GetType().Name,
            first))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="exp">The expression with wrong argument type.</param>
    /// <param name="first">The first result of argument calculation.</param>
    /// <param name="second">The second result of argument calculation.</param>
    public ResultIsNotSupportedException(IExpression? exp, object first, object second)
        : this(string.Format(
            CultureInfo.InvariantCulture,
            Resource.ResultIsNotSupported,
            exp?.GetType().Name,
            $"{first}, {second}"))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ResultIsNotSupportedException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner.</param>
    public ResultIsNotSupportedException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected ResultIsNotSupportedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}