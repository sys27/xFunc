// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.AngleUnits;

/// <summary>
/// Represents a number with angle measurement unit.
/// </summary>
public readonly struct AngleValue : IEquatable<AngleValue>, IComparable<AngleValue>, IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AngleValue"/> struct.
    /// </summary>
    /// <param name="angle">The value.</param>
    /// <param name="unit">The unit of number.</param>
    public AngleValue(NumberValue angle, AngleUnit unit)
    {
        Angle = angle;
        Unit = unit;
    }

    /// <summary>
    /// Creates the <see cref="AngleValue"/> struct with <c>Degree</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The angle.</returns>
    public static AngleValue Degree(double value)
        => Degree(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AngleValue"/> struct with <c>Degree</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The angle.</returns>
    public static AngleValue Degree(NumberValue numberValue)
        => new AngleValue(numberValue, AngleUnit.Degree);

    /// <summary>
    /// Creates the <see cref="AngleValue"/> struct with <c>Radian</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The angle.</returns>
    public static AngleValue Radian(double value)
        => Radian(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AngleValue"/> struct with <c>Radian</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The angle.</returns>
    public static AngleValue Radian(NumberValue numberValue)
        => new AngleValue(numberValue, AngleUnit.Radian);

    /// <summary>
    /// Creates the <see cref="AngleValue"/> struct with <c>Gradian</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The angle.</returns>
    public static AngleValue Gradian(double value)
        => Gradian(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AngleValue"/> struct with <c>Gradian</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The angle.</returns>
    public static AngleValue Gradian(NumberValue numberValue)
        => new AngleValue(numberValue, AngleUnit.Gradian);

    /// <inheritdoc />
    public bool Equals(AngleValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.Equals(otherInBase.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is AngleValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(AngleValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.CompareTo(otherInBase.Value);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
        => obj switch
        {
            null => 1,
            AngleValue other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(AngleValue)}"),
        };

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Angle, Unit);

    /// <inheritdoc />
    public override string ToString()
        => $"{Angle} {Unit}";

    /// <summary>
    /// Determines whether two specified instances of <see cref="AngleValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(AngleValue left, AngleValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="AngleValue"/> are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(AngleValue left, AngleValue right)
        => !left.Equals(right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(AngleValue left, AngleValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(AngleValue left, AngleValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(AngleValue left, AngleValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left angle.</param>
    /// <param name="right">The right angle.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(AngleValue left, AngleValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Adds two objects of <see cref="AngleValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static AngleValue operator +(AngleValue left, AngleValue right)
    {
        right = right.To(left.Unit);

        return new AngleValue(left.Angle + right.Angle, left.Unit);
    }

    /// <summary>
    /// Subtracts two objects of <see cref="AngleValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static AngleValue operator -(AngleValue left, AngleValue right)
    {
        right = right.To(left.Unit);

        return new AngleValue(left.Angle - right.Angle, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="AngleValue"/>.
    /// </summary>
    /// <param name="angleValue">The angle.</param>
    /// <returns>The negative of <paramref name="angleValue"/>.</returns>
    public static AngleValue operator -(AngleValue angleValue)
        => new AngleValue(-angleValue.Angle, angleValue.Unit);

    private AreaValue ToBase()
        => new AreaValue(Angle * Unit.Factor, AreaUnit.Meter);

    /// <summary>
    /// Converts the current object to the specified <paramref name="newUnit"/>.
    /// </summary>
    /// <param name="newUnit">The unit to convert to.</param>
    /// <returns>The angle which is converted to the specified <paramref name="newUnit"/>.</returns>
    public AngleValue To(AngleUnit newUnit)
    {
        var inBase = Angle * Unit.Factor;
        var converted = inBase / newUnit.Factor;

        return new AngleValue(converted, newUnit);
    }

    /// <summary>
    /// Converts the current object to degrees.
    /// </summary>
    /// <returns>The angle which is converted to degrees.</returns>
    public AngleValue ToDegree()
        => To(AngleUnit.Degree);

    /// <summary>
    /// Converts the current object to radians.
    /// </summary>
    /// <returns>The angle which is converted to radians.</returns>
    public AngleValue ToRadian()
        => To(AngleUnit.Radian);

    /// <summary>
    /// Converts the current object to gradians.
    /// </summary>
    /// <returns>The angle which is converted to gradians.</returns>
    public AngleValue ToGradian()
        => To(AngleUnit.Gradian);

    /// <summary>
    /// Normalizes the current angle between [0, 2pi).
    /// </summary>
    /// <returns>The normalized angle.</returns>
    public AngleValue Normalize()
    {
        const double degreeFullCircle = 360.0;
        const double radianFullCircle = 2 * Math.PI;
        const double gradianFullCircle = 400.0;

        static double NormalizeInternal(double value, double circle)
        {
            value %= circle;
            if (value < 0)
                value += circle;

            return value;
        }

        if (Unit == AngleUnit.Degree)
            return Degree(NormalizeInternal(Angle.Number, degreeFullCircle));

        if (Unit == AngleUnit.Radian)
            return Radian(NormalizeInternal(Angle.Number, radianFullCircle));

        // if (Unit == AngleUnit.Gradian)
        Debug.Assert(Unit == AngleUnit.Gradian, "Should be Gradian");

        return Gradian(NormalizeInternal(Angle.Number, gradianFullCircle));
    }

    /// <summary>
    /// Returns the absolute value of a specified angle.
    /// </summary>
    /// <param name="angleValue">The angle.</param>
    /// <returns>The angle, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
    public static AngleValue Abs(AngleValue angleValue)
        => new AngleValue(NumberValue.Abs(angleValue.Angle), angleValue.Unit);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified angle number.
    /// </summary>
    /// <param name="angleValue">The angle.</param>
    /// <returns>The smallest integral value.</returns>
    public static AngleValue Ceiling(AngleValue angleValue)
        => new AngleValue(NumberValue.Ceiling(angleValue.Angle), angleValue.Unit);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified angle number.
    /// </summary>
    /// <param name="angleValue">The angle.</param>
    /// <returns>The largest integral value.</returns>
    public static AngleValue Floor(AngleValue angleValue)
        => new AngleValue(NumberValue.Floor(angleValue.Angle), angleValue.Unit);

    /// <summary>
    /// Calculates the integral part of a specified angle number.
    /// </summary>
    /// <param name="angleValue">An angle to truncate.</param>
    /// <returns>The integral part of angle number.</returns>
    public static AngleValue Truncate(AngleValue angleValue)
        => new AngleValue(NumberValue.Truncate(angleValue.Angle), angleValue.Unit);

    /// <summary>
    /// Returns the fractional part of the angle number.
    /// </summary>
    /// <param name="angleValue">The angle number.</param>
    /// <returns>The fractional part.</returns>
    public static AngleValue Frac(AngleValue angleValue)
        => new AngleValue(NumberValue.Frac(angleValue.Angle), angleValue.Unit);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="angleValue">The angle number.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to <paramref name="angleValue"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="angleValue"/> is returned unchanged.</returns>
    public static AngleValue Round(AngleValue angleValue, NumberValue digits)
        => new AngleValue(NumberValue.Round(angleValue.Angle, digits), angleValue.Unit);

    /// <summary>
    /// The 'sin' function.
    /// </summary>
    /// <param name="angleValue">The angle number.</param>
    /// <returns>The result of sine function.</returns>
    public static NumberValue Sin(AngleValue angleValue)
    {
        var angle = angleValue.Normalize().Angle;

        // 0
        if (angle == 0)
            return NumberValue.Zero;

        // 30
        if (angle == Math.PI / 6)
            return NumberValue.Half;

        // 45
        if (angle == Math.PI / 4)
            return NumberValue.Sqrt2By2;

        // 60
        if (angle == Math.PI / 3)
            return NumberValue.Sqrt3By2;

        // 90
        if (angle == Math.PI / 2)
            return NumberValue.One;

        // 120
        if (angle == 2 * Math.PI / 3)
            return NumberValue.Sqrt3By2;

        // 135
        if (angle == 3 * Math.PI / 4)
            return NumberValue.Sqrt2By2;

        // 150
        if (angle == 5 * Math.PI / 6)
            return NumberValue.Half;

        // 180
        if (angle == Math.PI)
            return NumberValue.Zero;

        // 210
        if (angle == 7 * Math.PI / 6)
            return -NumberValue.Half;

        // 225
        if (angle == 5 * Math.PI / 4)
            return -NumberValue.Sqrt2By2;

        // 240
        if (angle == 4 * Math.PI / 3)
            return -NumberValue.Sqrt3By2;

        // 270
        if (angle == 3 * Math.PI / 2)
            return -NumberValue.One;

        // 300
        if (angle == 5 * Math.PI / 3)
            return -NumberValue.Sqrt3By2;

        // 315
        if (angle == 7 * Math.PI / 4)
            return -NumberValue.Sqrt2By2;

        // 330
        if (angle == 11 * Math.PI / 6)
            return -NumberValue.Half;

        return new NumberValue(Math.Sin(angleValue.Angle.Number));
    }

    /// <summary>
    /// The 'cos' function.
    /// </summary>
    /// <param name="angleValue">The angle number.</param>
    /// <returns>The result of cosine function.</returns>
    public static NumberValue Cos(AngleValue angleValue)
    {
        var angle = angleValue.Normalize().Angle;

        // 0
        if (angle == 0)
            return NumberValue.One;

        // 30
        if (angle == Math.PI / 6)
            return NumberValue.Sqrt3By2;

        // 45
        if (angle == Math.PI / 4)
            return NumberValue.Sqrt2By2;

        // 60
        if (angle == Math.PI / 3)
            return NumberValue.Half;

        // 90
        if (angle == Math.PI / 2)
            return NumberValue.Zero;

        // 120
        if (angle == 2 * Math.PI / 3)
            return -NumberValue.Half;

        // 135
        if (angle == 3 * Math.PI / 4)
            return -NumberValue.Sqrt2By2;

        // 150
        if (angle == 5 * Math.PI / 6)
            return -NumberValue.Sqrt3By2;

        // 180
        if (angle == Math.PI)
            return -NumberValue.One;

        // 210
        if (angle == 7 * Math.PI / 6)
            return -NumberValue.Sqrt3By2;

        // 225
        if (angle == 5 * Math.PI / 4)
            return -NumberValue.Sqrt2By2;

        // 240
        if (angle == 4 * Math.PI / 3)
            return -NumberValue.Half;

        // 270
        if (angle == 3 * Math.PI / 2)
            return NumberValue.Zero;

        // 300
        if (angle == 5 * Math.PI / 3)
            return NumberValue.Half;

        // 315
        if (angle == 7 * Math.PI / 4)
            return NumberValue.Sqrt2By2;

        // 330
        if (angle == 11 * Math.PI / 6)
            return NumberValue.Sqrt3By2;

        return new NumberValue(Math.Cos(angleValue.Angle.Number));
    }

    /// <summary>
    /// The 'tan' function.
    /// </summary>
    /// <param name="angleValue">The angle number.</param>
    /// <returns>The result of tangent function.</returns>
    public static NumberValue Tan(AngleValue angleValue)
    {
        var angle = angleValue.Normalize().Angle;

        // 0
        if (angle == 0)
            return NumberValue.Zero;

        // 30
        if (angle == Math.PI / 6)
            return NumberValue.Sqrt3By3;

        // 45
        if (angle == Math.PI / 4)
            return NumberValue.One;

        // 60
        if (angle == Math.PI / 3)
            return NumberValue.Sqrt3;

        // 90
        if (angle == Math.PI / 2)
            return NumberValue.PositiveInfinity;

        // 120
        if (angle == 2 * Math.PI / 3)
            return -NumberValue.Sqrt3;

        // 135
        if (angle == 3 * Math.PI / 4)
            return -NumberValue.One;

        // 150
        if (angle == 5 * Math.PI / 6)
            return -NumberValue.Sqrt3By3;

        // 180
        if (angle == Math.PI)
            return NumberValue.Zero;

        // 210
        if (angle == 7 * Math.PI / 6)
            return NumberValue.Sqrt3By3;

        // 225
        if (angle == 5 * Math.PI / 4)
            return NumberValue.One;

        // 240
        if (angle == 4 * Math.PI / 3)
            return NumberValue.Sqrt3;

        // 270
        if (angle == 3 * Math.PI / 2)
            return NumberValue.PositiveInfinity;

        // 300
        if (angle == 5 * Math.PI / 3)
            return -NumberValue.Sqrt3;

        // 315
        if (angle == 7 * Math.PI / 4)
            return -NumberValue.One;

        // 330
        if (angle == 11 * Math.PI / 6)
            return -NumberValue.Sqrt3By3;

        return new NumberValue(Math.Tan(angleValue.Angle.Number));
    }

    /// <summary>
    /// The 'cot' function.
    /// </summary>
    /// <param name="angleValue">The angle number.</param>
    /// <returns>The result of cotangent function.</returns>
    public static NumberValue Cot(AngleValue angleValue)
    {
        var angle = angleValue.Normalize().Angle;

        // 0
        if (angle == 0)
            return NumberValue.PositiveInfinity;

        // 30
        if (angle == Math.PI / 6)
            return NumberValue.Sqrt3;

        // 45
        if (angle == Math.PI / 4)
            return NumberValue.One;

        // 60
        if (angle == Math.PI / 3)
            return NumberValue.Sqrt3By3;

        // 90
        if (angle == Math.PI / 2)
            return NumberValue.Zero;

        // 120
        if (angle == 2 * Math.PI / 3)
            return -NumberValue.Sqrt3By3;

        // 135
        if (angle == 3 * Math.PI / 4)
            return -NumberValue.One;

        // 150
        if (angle == 5 * Math.PI / 6)
            return -NumberValue.Sqrt3;

        // 180
        if (angle == Math.PI)
            return NumberValue.PositiveInfinity;

        // 210
        if (angle == 7 * Math.PI / 6)
            return NumberValue.Sqrt3;

        // 225
        if (angle == 5 * Math.PI / 4)
            return NumberValue.One;

        // 240
        if (angle == 4 * Math.PI / 3)
            return NumberValue.Sqrt3By3;

        // 270
        if (angle == 3 * Math.PI / 2)
            return NumberValue.Zero;

        // 300
        if (angle == 5 * Math.PI / 3)
            return -NumberValue.Sqrt3;

        // 315
        if (angle == 7 * Math.PI / 4)
            return -NumberValue.One;

        // 330
        if (angle == 11 * Math.PI / 6)
            return -NumberValue.Sqrt3By3;

        return new NumberValue(Math.Cos(angleValue.Angle.Number) / Math.Sin(angleValue.Angle.Number));
    }

    /// <summary>
    /// The 'cot' function.
    /// </summary>
    /// <param name="angleValue">The angle number.</param>
    /// <returns>The result of secant function.</returns>
    public static NumberValue Sec(AngleValue angleValue)
    {
        var angle = angleValue.Normalize().Angle;

        // 0
        if (angle == 0)
            return NumberValue.One;

        // 30
        if (angle == Math.PI / 6)
            return NumberValue.Sqrt3By3By2;

        // 45
        if (angle == Math.PI / 4)
            return NumberValue.Sqrt2;

        // 60
        if (angle == Math.PI / 3)
            return NumberValue.Two;

        // 90
        if (angle == Math.PI / 2)
            return NumberValue.PositiveInfinity;

        // 120
        if (angle == 2 * Math.PI / 3)
            return -NumberValue.Two;

        // 135
        if (angle == 3 * Math.PI / 4)
            return -NumberValue.Sqrt2;

        // 150
        if (angle == 5 * Math.PI / 6)
            return -NumberValue.Sqrt3By3By2;

        // 180
        if (angle == Math.PI)
            return -NumberValue.One;

        // 210
        if (angle == 7 * Math.PI / 6)
            return -NumberValue.Sqrt3By3By2;

        // 225
        if (angle == 5 * Math.PI / 4)
            return -NumberValue.Sqrt2;

        // 240
        if (angle == 4 * Math.PI / 3)
            return -NumberValue.Two;

        // 270
        if (angle == 3 * Math.PI / 2)
            return NumberValue.PositiveInfinity;

        // 300
        if (angle == 5 * Math.PI / 3)
            return -NumberValue.Two;

        // 315
        if (angle == 7 * Math.PI / 4)
            return NumberValue.Sqrt2;

        // 330
        if (angle == 11 * Math.PI / 6)
            return NumberValue.Sqrt3By3By2;

        return new NumberValue(1 / Math.Cos(angleValue.Angle.Number));
    }

    /// <summary>
    /// The 'csc' function.
    /// </summary>
    /// <param name="angleValue">The angle number.</param>
    /// <returns>The result of cosecant function.</returns>
    public static NumberValue Csc(AngleValue angleValue)
    {
        var angle = angleValue.Normalize().Angle;

        // 0
        if (angle == 0)
            return NumberValue.PositiveInfinity;

        // 30
        if (angle == Math.PI / 6)
            return NumberValue.Two;

        // 45
        if (angle == Math.PI / 4)
            return NumberValue.Sqrt2;

        // 60
        if (angle == Math.PI / 3)
            return NumberValue.Sqrt3By3By2;

        // 90
        if (angle == Math.PI / 2)
            return NumberValue.One;

        // 120
        if (angle == 2 * Math.PI / 3)
            return NumberValue.Sqrt3By3By2;

        // 135
        if (angle == 3 * Math.PI / 4)
            return NumberValue.Sqrt2;

        // 150
        if (angle == 5 * Math.PI / 6)
            return NumberValue.Two;

        // 180
        if (angle == Math.PI)
            return NumberValue.PositiveInfinity;

        // 210
        if (angle == 7 * Math.PI / 6)
            return -NumberValue.Two;

        // 225
        if (angle == 5 * Math.PI / 4)
            return -NumberValue.Sqrt2;

        // 240
        if (angle == 4 * Math.PI / 3)
            return -NumberValue.Sqrt3By3By2;

        // 270
        if (angle == 3 * Math.PI / 2)
            return -NumberValue.One;

        // 300
        if (angle == 5 * Math.PI / 3)
            return NumberValue.Sqrt3By3By2;

        // 315
        if (angle == 7 * Math.PI / 4)
            return NumberValue.Sqrt2;

        // 330
        if (angle == 11 * Math.PI / 6)
            return NumberValue.Two;

        return new NumberValue(1 / Math.Sin(angleValue.Angle.Number));
    }

    /// <summary>
    /// Returns the angle whose sine is the specified number.
    /// </summary>
    /// <param name="number">A number representing a sine, where d must be greater than or equal to -1, but less than or equal to 1.</param>
    /// <returns>
    /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
    /// -or-
    /// <see cref="double.NaN"/> if <paramref name="number"/> &lt; -1 or <paramref name="number"/> &gt; 1 or <paramref name="number"/> equals <see cref="double.NaN"/>.
    /// </returns>
    public static AngleValue Asin(NumberValue number)
        => Radian(Math.Asin(number.Number));

    /// <summary>
    /// Returns the angle whose cosine is the specified number.
    /// </summary>
    /// <param name="number">A number representing a cosine, where d must be greater than or equal to -1, but less than or equal to 1.</param>
    /// <returns>
    /// An angle, θ, measured in radians, such that 0 ≤ θ ≤ π.
    /// -or-
    /// <see cref="double.NaN"/> if <paramref name="number"/> &lt; -1 or <paramref name="number"/> &gt; 1 or <paramref name="number"/> equals <see cref="double.NaN"/>.
    /// </returns>
    public static AngleValue Acos(NumberValue number)
        => Radian(Math.Acos(number.Number));

    /// <summary>
    /// Returns the angle whose tangent is the specified number.
    /// </summary>
    /// <param name="number">A number representing a tangent.</param>
    /// <returns>
    /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
    /// -or-
    /// <see cref="double.NaN"/> if <paramref name="number"/> equals <see cref="double.NaN"/>,
    /// -π/2 rounded to double precision (-1.5707963267949) if <paramref name="number"/> equals <see cref="double.NegativeInfinity"/>,
    /// or π/2 rounded to double precision (1.5707963267949) if <paramref name="number"/> equals <see cref="double.PositiveInfinity"/>.
    /// </returns>
    public static AngleValue Atan(NumberValue number)
        => Radian(Math.Atan(number.Number));

    /// <summary>
    /// Returns the angle whose cotangent is the specified number.
    /// </summary>
    /// <param name="number">A number representing a cotangent.</param>
    /// <returns>An angle, measured in radians.</returns>
    public static AngleValue Acot(NumberValue number)
        => Radian(Math.PI / 2 - Math.Atan(number.Number));

    /// <summary>
    /// Returns the angle whose secant is the specified number.
    /// </summary>
    /// <param name="number">A number representing a secant.</param>
    /// <returns>An angle, measured in radians.</returns>
    public static AngleValue Asec(NumberValue number)
        => Radian(Math.Acos(1 / number.Number));

    /// <summary>
    /// Returns the angle whose cosecant is the specified number.
    /// </summary>
    /// <param name="number">A number representing a hyperbolic cosecant.</param>
    /// <returns>An angle, measured in radians.</returns>
    public static AngleValue Acsc(NumberValue number)
        => Radian(Math.Asin(1 / number.Number));

    /// <summary>
    /// Returns the hyperbolic sine of the specified angle.
    /// </summary>
    /// <param name="angle">An angle.</param>
    /// <returns>The hyperbolic sine of <paramref name="angle"/>. If <paramref name="angle"/> is equal to <see cref="double.NegativeInfinity"/>, <see cref="double.PositiveInfinity"/>, or <see cref="double.NaN"/>.</returns>
    public static NumberValue Sinh(AngleValue angle)
        => new NumberValue(Math.Sinh(angle.ToRadian().Angle.Number));

    /// <summary>
    /// Returns the hyperbolic cosine of the specified angle.
    /// </summary>
    /// <param name="angle">An angle.</param>
    /// <returns>
    /// The hyperbolic cosine of <paramref name="angle"/>.
    /// If <paramref name="angle"/> is equal to <see cref="double.NegativeInfinity"/> or <see cref="double.PositiveInfinity"/>, <see cref="double.PositiveInfinity"/> is returned.
    /// If <paramref name="angle"/> is equal to <see cref="double.NaN"/>, <see cref="double.NaN"/> is returned.
    /// </returns>
    public static NumberValue Cosh(AngleValue angle)
        => new NumberValue(Math.Cosh(angle.ToRadian().Angle.Number));

    /// <summary>
    /// Returns the hyperbolic tangent of the specified angle.
    /// </summary>
    /// <param name="angle">An angle.</param>
    /// <returns>
    /// The hyperbolic tangent of <paramref name="angle"/>.
    /// If <paramref name="angle"/> is equal to <see cref="double.NegativeInfinity"/>, this method returns -1.
    /// If <paramref name="angle"/> is equal to <see cref="double.PositiveInfinity"/>, this method returns 1.
    /// If <paramref name="angle"/> is equal to <see cref="double.NaN"/>, this method returns <see cref="double.NaN"/>.
    /// </returns>
    public static NumberValue Tanh(AngleValue angle)
        => new NumberValue(Math.Tanh(angle.ToRadian().Angle.Number));

    /// <summary>
    /// Returns the hyperbolic cotangent of the specified angle.
    /// </summary>
    /// <param name="angle">An angle, measured in radians.</param>
    /// <returns>The hyperbolic cotangent of value.</returns>
    public static NumberValue Coth(AngleValue angle)
    {
        var d = angle.ToRadian().Angle.Number;

        return new NumberValue((Math.Exp(d) + Math.Exp(-d)) / (Math.Exp(d) - Math.Exp(-d)));
    }

    /// <summary>
    /// Returns the hyperbolic secant of the specified angle.
    /// </summary>
    /// <param name="angle">An angle.</param>
    /// <returns>The hyperbolic secant of value.</returns>
    public static NumberValue Sech(AngleValue angle)
    {
        var d = angle.ToRadian().Angle.Number;

        return new NumberValue(2 / (Math.Exp(d) + Math.Exp(-d)));
    }

    /// <summary>
    /// Returns the hyperbolic cosecant of the specified angle.
    /// </summary>
    /// <param name="angle">An angle.</param>
    /// <returns>The hyperbolic cosecant of value.</returns>
    public static NumberValue Csch(AngleValue angle)
    {
        var d = angle.ToRadian().Angle.Number;

        return new NumberValue(2 / (Math.Exp(d) - Math.Exp(-d)));
    }

    /// <summary>
    /// Returns the angle whose hyperbolic sine is the specified number.
    /// </summary>
    /// <param name="number">A number representing a hyperbolic sine, where <paramref name="number"/> must be greater than or equal to <see cref="double.NegativeInfinity"/>, but less than or equal to <see cref="double.PositiveInfinity"/>.</param>
    /// <returns>
    /// An angle, θ, measured in radians, such that -∞ &lt; θ ≤ -1, or 1 ≤ θ &lt; ∞.
    /// -or-
    /// <see cref="double.NaN"/> if <paramref name="number"/> equals <see cref="double.NaN"/>.
    /// </returns>
    public static AngleValue Asinh(NumberValue number)
        => Radian(Math.Asinh(number.Number));

    /// <summary>
    /// Returns the angle whose hyperbolic cosine is the specified number.
    /// </summary>
    /// <param name="number">A number representing a hyperbolic cosine, where <paramref name="number"/> must be greater than or equal to 1, but less than or equal to <see cref="double.PositiveInfinity"/>.</param>
    /// <returns>
    /// An angle, θ, measured in radians, such that 0 ≤ θ ≤ ∞.
    /// -or-
    /// <see cref="double.NaN"/> if <paramref name="number"/> &lt; 1 or <paramref name="number"/> equals <see cref="double.NaN"/>.
    /// </returns>
    public static AngleValue Acosh(NumberValue number)
        => Radian(Math.Acosh(number.Number));

    /// <summary>
    /// Returns the angle whose hyperbolic tangent is the specified number.
    /// </summary>
    /// <param name="number">A number representing a hyperbolic tangent, where <paramref name="number"/> must be greater than or equal to -1, but less than or equal to 1.</param>
    /// <returns>
    /// An angle, θ, measured in radians, such that -∞ &lt; θ &lt; -1, or 1 &lt; θ &lt; ∞.
    /// -or-
    /// <see cref="double.NaN"/> if <paramref name="number"/> &lt; -1 or <paramref name="number"/> &gt; 1 or <paramref name="number"/> equals <see cref="double.NaN"/>.
    /// </returns>
    public static AngleValue Atanh(NumberValue number)
        => Radian(Math.Atanh(number.Number));

    /// <summary>
    /// Returns the angle whose hyperbolic cotangent is the specified number.
    /// </summary>
    /// <param name="number">A number representing a hyperbolic cotangent.</param>
    /// <returns>An angle, measured in radians.</returns>
    public static AngleValue Acoth(NumberValue number)
        => Radian(Math.Log((number.Number + 1) / (number.Number - 1)) / 2);

    /// <summary>
    /// Returns the angle whose hyperbolic secant is the specified number.
    /// </summary>
    /// <param name="number">A number representing a hyperbolic secant.</param>
    /// <returns>An angle, measured in radians.</returns>
    public static AngleValue Asech(NumberValue number)
    {
        var z = 1 / number.Number;

        return Radian(Math.Log(z + Math.Sqrt(z + 1) * Math.Sqrt(z - 1)));
    }

    /// <summary>
    /// Returns the angle whose hyperbolic cosecant is the specified number.
    /// </summary>
    /// <param name="number">A number representing a hyperbolic cosecant.</param>
    /// <returns>An angle, measured in radians.</returns>
    public static AngleValue Acsch(NumberValue number)
        => Radian(Math.Log(1 / number.Number + Math.Sqrt(1 / number.Number * number.Number + 1)));

    /// <summary>
    /// Converts <see cref="AngleValue"/> to <see cref="Angle"/>.
    /// </summary>
    /// <returns>The angle number.</returns>
    public Angle AsExpression()
        => new Angle(this);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public NumberValue Angle { get; }

    /// <summary>
    /// Gets a unit.
    /// </summary>
    public AngleUnit Unit { get; }

    /// <summary>
    /// Gets an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    public double Sign => Angle.Sign;
}