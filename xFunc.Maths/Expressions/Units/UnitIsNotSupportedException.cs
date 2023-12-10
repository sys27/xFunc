// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units;

/// <summary>
/// Represents the exception that is thrown when converter uses unsupported unit.
/// </summary>
[Serializable]
public class UnitIsNotSupportedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="unit">The unsupported unit.</param>
    public UnitIsNotSupportedException(string? unit)
        : this(unit, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitIsNotSupportedException"/> class.
    /// </summary>
    /// <param name="unit">The unsupported unit.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public UnitIsNotSupportedException(string? unit, Exception? inner)
        : base(string.Format(Resource.UnitIsNotSupportedException, unit), inner)
    {
        Unit = unit;
    }

    /// <summary>
    /// Gets the unsupported unit.
    /// </summary>
    public string? Unit { get; }
}