// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Analyzers.TypeAnalyzers;

/// <summary>
/// Represents results of expressions.
/// </summary>
[Flags]
public enum ResultTypes
{
    /// <summary>
    /// The expression doesn't return anything.
    /// </summary>
    None = 0,

    /// <summary>
    /// The expression returns undefined result.
    /// </summary>
    Undefined = 1,

    /// <summary>
    /// The expression returns a number.
    /// </summary>
    Number = 1 << 1,

    /// <summary>
    /// The expression returns a complex number.
    /// </summary>
    ComplexNumber = 1 << 2,

    /// <summary>
    /// The expression returns a boolean (true or false).
    /// </summary>
    Boolean = 1 << 3,

    /// <summary>
    /// The expression returns a vector.
    /// </summary>
    Vector = 1 << 4,

    /// <summary>
    /// The expression returns a matrix.
    /// </summary>
    Matrix = 1 << 5,

    /// <summary>
    /// The expression returns other expression.
    /// </summary>
    Expression = 1 << 6,

    /// <summary>
    /// The expression returns an angle.
    /// </summary>
    AngleNumber = 1 << 7,

    /// <summary>
    /// The expression returns a string.
    /// </summary>
#pragma warning disable CA1720
    String = 1 << 8,
#pragma warning restore CA1720

    /// <summary>
    /// The expression returns a power number.
    /// </summary>
    PowerNumber = 1 << 9,

    /// <summary>
    /// The expression returns a temperature number.
    /// </summary>
    TemperatureNumber = 1 << 10,

    /// <summary>
    /// The expression returns a mass number.
    /// </summary>
    MassNumber = 1 << 11,

    /// <summary>
    /// The expression returns a length number.
    /// </summary>
    LengthNumber = 1 << 12,

    /// <summary>
    /// The expression returns a time number.
    /// </summary>
    TimeNumber = 1 << 13,

    /// <summary>
    /// The expression returns a area number.
    /// </summary>
    AreaNumber = 1 << 14,

    /// <summary>
    /// The expression returns a volume number.
    /// </summary>
    VolumeNumber = 1 << 15,

    /// <summary>
    /// The expression returns a number or a complex number.
    /// </summary>
    NumberOrComplex = Number | ComplexNumber,

    /// <summary>
    /// The expression returns a number or a angle number.
    /// </summary>
    NumberOrAngle = Number | AngleNumber,

    /// <summary>
    /// The expression returns a number or a angle number or a complex number.
    /// </summary>
    NumberOrAngleOrComplex = NumberOrAngle | ComplexNumber,

    /// <summary>
    /// The expression returns a number or a vector or a matrix.
    /// </summary>
    NumberOrVectorOrMatrix = Number | Vector | Matrix,

    /// <summary>
    /// The expression returns any type of number.
    /// </summary>
    Numbers = Number |
              AngleNumber |
              PowerNumber |
              TemperatureNumber |
              MassNumber |
              LengthNumber |
              TimeNumber |
              AreaNumber |
              VolumeNumber,

    /// <summary>
    /// The expression returns a number or a angle number or a complex number or a vector.
    /// </summary>
    NumbersOrComplexOrVector = Numbers | ComplexNumber | Vector,
}