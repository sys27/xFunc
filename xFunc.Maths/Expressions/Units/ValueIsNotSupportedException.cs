// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization;

namespace xFunc.Maths.Expressions.Units;

/// <summary>
/// Represents the exception that is thrown when converter uses unsupported value to convert.
/// </summary>
[Serializable]
public class ValueIsNotSupportedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public ValueIsNotSupportedException(object value)
        : this(value, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ValueIsNotSupportedException(object value, Exception? inner)
        : base(string.Format(Resource.ValueIsNotSupportedException, value, value.GetType().Name), inner)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected ValueIsNotSupportedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <summary>
    /// Gets a value to converts.
    /// </summary>
    public object? Value { get; }
}