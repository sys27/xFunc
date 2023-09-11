// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents an exception which is thrown if expression doesn't support result type of own argument.
/// </summary>
/// <seealso cref="Exception" />
[Serializable]
public class ExecutionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExecutionException"/> class.
    /// </summary>
    public ExecutionException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExecutionException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ExecutionException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExecutionException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner.</param>
    public ExecutionException(string message, Exception inner)
        : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExecutionException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected ExecutionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Creates an instance of <see cref="ExecutionException"/> with the message of unsupported result.
    /// </summary>
    /// <param name="exp">The expression that encountered unsupported results in its children's expressions.</param>
    /// <returns>The instance of <see cref="ExecutionException"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ExecutionException For(IExpression exp)
        => new ExecutionException(
            string.Format(
                CultureInfo.InvariantCulture,
                Resource.ResultIsNotSupported,
                exp.ToString()));
}